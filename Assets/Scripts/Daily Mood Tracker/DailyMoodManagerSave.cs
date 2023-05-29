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
		
	}

	public DailyMoodManagerSave()
	{
        dailyMoodDictionary = new Dictionary<string, Dictionary<string, CurrentMood>>();
	}
}

public class CurrentMood
{
	public MoodType moodType;
	public long timestamp;
}
