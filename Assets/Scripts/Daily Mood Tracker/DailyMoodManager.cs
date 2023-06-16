using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMoodManager : MonoBehaviour
{
	public static DailyMoodManager instance;

	[HideInInspector]
	public DailyMoodManagerSave dailyMoodManagerSave;

	public MoodType currentMoodType = MoodType.Neutral;

	private string saveFileName = "dailyMoodManagerSave";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		loadSaveDailyMoodManager();
	}

	public void loadSaveDailyMoodManager()
	{
		DailyMoodManagerSaveData dailyMoodManagerSaveData = SaveSystem.Load(saveFileName) as DailyMoodManagerSaveData;

		if (dailyMoodManagerSaveData != null)
		{
			dailyMoodManagerSave = new DailyMoodManagerSave(dailyMoodManagerSaveData);
		}
		else
		{
			dailyMoodManagerSave = new DailyMoodManagerSave();
		}
	}

	public void SetCurrentMood(MoodType moodType)
	{
		currentMoodType = moodType;
	}

	public void SaveCurrentMood()
	{
		CurrentMood currentMood = new CurrentMood();
		currentMood.moodType = currentMoodType;
		currentMood.timestamp = TimeStamp.GetTimeStamp();

		dailyMoodManagerSave.AddMood(currentMood);

		SaveDailyMoodManager();
	}

	public void SaveDailyMoodManager()
	{
		DailyMoodManagerSaveData dailyMoodManagerSaveData = new DailyMoodManagerSaveData(dailyMoodManagerSave);

		SaveSystem.Save(saveFileName, dailyMoodManagerSaveData);
	}

	internal MoodType GetMood()
	{
		// get the last mood saved
		CurrentMood currentMood = dailyMoodManagerSave.GetLastMood();

		// if there is no mood saved, return neutral
		if (currentMood == null) return MoodType.Neutral;

		// if the last mood saved is older than 24 hours, return neutral
		if (TimeStamp.GetTimeStamp() - currentMood.timestamp > 86400) return MoodType.Neutral;

		// return the last mood saved
		return currentMood.moodType;
	}

	public List<CurrentMood> AllMoods()
	{
		return dailyMoodManagerSave.GetAllTheMood();
	}
}
