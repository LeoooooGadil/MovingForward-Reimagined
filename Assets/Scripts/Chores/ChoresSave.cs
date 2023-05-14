using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoresSave
{
	public List<Chore> chores;
	public List<Chore> completedChores;
	public DateTime date;

	public ChoresSave(ChoresSaveData _choresSaveData)
	{
		chores = new List<Chore>();
		completedChores = new List<Chore>();

		foreach (Chore chore in _choresSaveData.chores)
		{
			chores.Add(chore);
		}

		foreach (Chore chore in _choresSaveData.completedChores)
		{
			completedChores.Add(chore);
		}

		date = _choresSaveData.date;
	}

	public ChoresSave()
	{
		chores = new List<Chore>();
        completedChores = new List<Chore>();
		date = System.DateTime.Now;
	}

	public string GetDate()
	{
		return date.ToString("dd/MM/yyyy");
	}

	public void AddDailyChore(Chore _dailyChore)
	{
		chores.Add(_dailyChore);
	}

	public void RemoveDailyChore(Chore _dailyChore)
	{
		chores.Remove(_dailyChore);
	}

	public void AddCompletedChore(Chore _completedChore)
	{
		completedChores.Add(_completedChore);
	}

	public int GetDailyChoreCount()
	{
		return chores.Count;
	}

	internal void CompleteChore(Chore chore)
	{
		completedChores.Add(chore.SetCompleted(true));
		chores.Remove(chore);
	}

	internal Chore FindChore(DailyChoreRoom room, DailyChoreType type)
	{
		foreach (Chore chore in chores)
		{
			if (chore.dailyChoreRoom == room && chore.dailyChoreType == type)
			{
				return chore;
			}
		}

		return null;
	}

	internal Chore FindChore(string name)
	{
		foreach (Chore chore in chores)
		{
			if (chore.choreName == name)
			{
				return chore;
			}
		}

		return null;
	}
}

[System.Serializable]
public class Chore
{
	public string choreName;
	public float choreComponensation;
	public float minScore;
	public DailyChoreRoom dailyChoreRoom;
	public DailyChoreType dailyChoreType;
	public bool isMandatory;
	public bool isCompleted;

	public Chore(string _choreName, float _chorePoints, float _choreComponensation, float _minScore, DailyChoreRoom _dailyChoreRoom, DailyChoreType _dailyChoreType, bool _isMandatory, bool _isCompleted)
	{
		choreName = _choreName;
		choreComponensation = _choreComponensation;
		minScore = _minScore;
		dailyChoreRoom = _dailyChoreRoom;
		dailyChoreType = _dailyChoreType;
		isMandatory = _isMandatory;
		isCompleted = _isCompleted;
	}

	public Chore(Chores _chores)
	{
		choreName = _chores.name;
		dailyChoreRoom = _chores.room;
		dailyChoreType = _chores.type;
		minScore = _chores.minScore;
		isMandatory = _chores.isMandatory;
		isCompleted = false;
	}

	public Chore(string _choreName, float _choreComponensation, int _minScore, DailyChoreRoom _dailyChoreRoom, DailyChoreType _dailyChoreType, bool _isMandatory)
	{
		choreName = _choreName;
		choreComponensation = _choreComponensation;
		minScore = _minScore;
		dailyChoreRoom = _dailyChoreRoom;
		dailyChoreType = _dailyChoreType;
		isMandatory = _isMandatory;
		isCompleted = false;
	}

	public Chore SetCompleted(bool value)
	{
		isCompleted = value;
		return this;
	}

	public bool IsCompleted()
	{
		return isCompleted;
	}

	public Chore SetCompensation(float _compensation)
	{
		choreComponensation = _compensation;
		return this;
	}

	public Chore SetCompensation(Chore _chore)
	{
		choreComponensation = _chore.choreComponensation;
		return this;
	}
}
