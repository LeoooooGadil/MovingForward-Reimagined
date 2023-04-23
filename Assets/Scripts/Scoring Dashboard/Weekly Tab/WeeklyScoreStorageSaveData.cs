using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeeklyScoreStorageSaveData
{
    public Dictionary<string, WeeklyScoreStorageItem> weeklyScoreStorageItems = new Dictionary<string, WeeklyScoreStorageItem>();

    public WeeklyScoreStorageSaveData(WeeklyScoreStorageSave weeklyScoreStorageSave)
    {
        foreach (KeyValuePair<string, WeeklyScoreStorageItem> weeklyScoreStorageItem in weeklyScoreStorageSave.weeklyScoreStorageItems)
        {
            weeklyScoreStorageItems.Add(weeklyScoreStorageItem.Key, weeklyScoreStorageItem.Value);
        }
    }
}
