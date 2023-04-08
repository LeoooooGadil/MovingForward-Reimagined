using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyScoreStorageSave
{
    public Dictionary<string, DailyScoreStorageItem> dailyScoreStorageItems = new Dictionary<string, DailyScoreStorageItem>();

    public DailyScoreStorageSave(DailyScoreStorageSaveData dailyScoreStorageSaveData)
    {
        foreach (KeyValuePair<string, DailyScoreStorageItem> dailyScoreStorageItem in dailyScoreStorageSaveData.dailyScoreStorageItems)
        {
            dailyScoreStorageItems.Add(dailyScoreStorageItem.Key, dailyScoreStorageItem.Value);
        }
    }

    public DailyScoreStorageSave()
    {
        dailyScoreStorageItems = new Dictionary<string, DailyScoreStorageItem>();
    }

    public void AddDailyScoreStorageItem(string key, DailyScoreStorageItem dailyScoreStorageItem)
    {
        dailyScoreStorageItems.Add(key, dailyScoreStorageItem);
    }
}
