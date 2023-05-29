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

}
