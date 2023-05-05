using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMoodManager : MonoBehaviour
{
	public static DailyMoodManager instance;

	[HideInInspector]
	public DailyMoodManagerSave dailyMoodManagerSave;

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

	public void SaveDailyMoodManager()
	{
		DailyMoodManagerSaveData dailyMoodManagerSaveData = new DailyMoodManagerSaveData(dailyMoodManagerSave);

		SaveSystem.Save(saveFileName, dailyMoodManagerSaveData);
	}




}
