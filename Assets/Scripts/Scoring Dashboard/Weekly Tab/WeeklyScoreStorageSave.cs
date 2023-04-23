using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyScoreStorageSave
{
    public Dictionary<string, WeeklyScoreStorageItem> weeklyScoreStorageItems = new Dictionary<string, WeeklyScoreStorageItem>();

    public WeeklyScoreStorageSave(WeeklyScoreStorageSaveData weeklyScoreStorageSaveData)
    {
        foreach (KeyValuePair<string, WeeklyScoreStorageItem> weeklyScoreStorageItem in weeklyScoreStorageSaveData.weeklyScoreStorageItems)
        {
            weeklyScoreStorageItems.Add(weeklyScoreStorageItem.Key, weeklyScoreStorageItem.Value);
        }
    }

    public WeeklyScoreStorageSave()
    {
        weeklyScoreStorageItems = new Dictionary<string, WeeklyScoreStorageItem>();
    }

    public void AddWeeklyScoreStorageItem(string key, WeeklyScoreStorageItem weeklyScoreStorageItem)
    {
        weeklyScoreStorageItems.Add(key, weeklyScoreStorageItem);
    }
}

// a class that contains the 7 DailyScoreStorageItem class per week
[System.Serializable]
public class WeeklyScoreStorageItem
{
	public string key;
    public long timestamp;
    public float collectiveScore;
    public List<DailyScoreStorageItem> dailyScoreStorageItems = new List<DailyScoreStorageItem>();

    public void AddDailyScoreStorageItem(DailyScoreStorageItem dailyScoreStorageItem)
    {
        dailyScoreStorageItems.Add(dailyScoreStorageItem);
    }

    public void UpdateDailyScoreStorageItem(DailyScoreStorageItem dailyScoreStorageItem)
    {
        for (int i = 0; i < dailyScoreStorageItems.Count; i++)
        {
            if (dailyScoreStorageItems[i].key == dailyScoreStorageItem.key)
            {
                dailyScoreStorageItems[i] = dailyScoreStorageItem;
            }
        }
    }
}
