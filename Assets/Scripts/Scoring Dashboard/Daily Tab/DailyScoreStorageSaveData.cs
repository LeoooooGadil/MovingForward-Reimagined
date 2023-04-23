using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyScoreStorageSaveData
{
	public Dictionary<string, DailyScoreStorageItem> dailyScoreStorageItems = new Dictionary<string, DailyScoreStorageItem>();

    public DailyScoreStorageSaveData(DailyScoreStorageSave dailyScoreStorageSave)
    {
        foreach (KeyValuePair<string, DailyScoreStorageItem> dailyScoreStorageItem in dailyScoreStorageSave.dailyScoreStorageItems)
        {
            dailyScoreStorageItems.Add(dailyScoreStorageItem.Key, dailyScoreStorageItem.Value);
        }
    }
}
