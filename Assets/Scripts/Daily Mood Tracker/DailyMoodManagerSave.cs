using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMoodManagerSave
{
	public Dictionary<string, Dictionary<string, CurrentMood>> dailyMoodDictionary;

	public DailyMoodManagerSave(DailyMoodManagerSaveData _dailyMoodManagerSaveData)
	{
		dailyMoodDictionary = new Dictionary<string, Dictionary<string, CurrentMood>>();
		dailyMoodDictionary = _dailyMoodManagerSaveData.dailyMoodDictionary;
	}

	public void AddMood(CurrentMood currentMood)
	{
		string todayString = DateTime.Today.ToString("dd/MM/yyyy"); 

		if (dailyMoodDictionary.ContainsKey(todayString))
		{
			if (dailyMoodDictionary[todayString].ContainsKey(currentMood.moodType.ToString()))
			{
				dailyMoodDictionary[todayString][currentMood.moodType.ToString()] = currentMood;
			}
			else
			{
				dailyMoodDictionary[todayString].Add(currentMood.moodType.ToString(), currentMood);
			}
		}
		else
		{
			Dictionary<string, CurrentMood> moodDictionary = new Dictionary<string, CurrentMood>();
			moodDictionary.Add(currentMood.moodType.ToString(), currentMood);
			dailyMoodDictionary.Add(todayString, moodDictionary);
		}
	}

	public DailyMoodManagerSave()
	{
        dailyMoodDictionary = new Dictionary<string, Dictionary<string, CurrentMood>>();
	}
}

[System.Serializable]
public class CurrentMood
{
	public MoodType moodType;
	public long timestamp;
}
