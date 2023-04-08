using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DailyScoreStorage
{
    public static DailyScoreStorageSave dailyScoreStorageSave;
    public static string saveFileName = "dailyScoreStorage";

    static void LoadDailyScoreStorage()
    {
        DailyScoreStorageSaveData dailyScoreStorageSaveData = SaveSystem.Load(saveFileName) as DailyScoreStorageSaveData;

        if (dailyScoreStorageSaveData == null)
        {
            dailyScoreStorageSave = new DailyScoreStorageSave();
            return;
        }

        dailyScoreStorageSave = new DailyScoreStorageSave(dailyScoreStorageSaveData);
    }

    public static void Publish(string key, string name, float score, long timestamp, DailyScoreStorageType dailyScoreStorageType)
    {
        LoadDailyScoreStorage();

        DailyScoreStorageItem dailyScoreStorageItem = new DailyScoreStorageItem();
        dailyScoreStorageItem.key = key;
        dailyScoreStorageItem.name = name;
        dailyScoreStorageItem.score = score;
        dailyScoreStorageItem.timestamp = timestamp;
        dailyScoreStorageItem.dailyScoreStorageType = dailyScoreStorageType;

        dailyScoreStorageSave.AddDailyScoreStorageItem(key, dailyScoreStorageItem);

        SaveDailyScoreStorage();
    }

    public static void SaveDailyScoreStorage()
    {
        DailyScoreStorageSaveData dailyScoreStorageSaveData = new DailyScoreStorageSaveData(dailyScoreStorageSave);
        SaveSystem.Save(saveFileName, dailyScoreStorageSaveData);
    }
}
