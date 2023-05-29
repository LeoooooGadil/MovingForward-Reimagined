using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyMoodManagerSaveData
{
    public Dictionary<string, Dictionary<string, CurrentMood>> dailyMoodDictionary;

    public DailyMoodManagerSaveData(DailyMoodManagerSave _dailyMoodManagerSave)
    {
        dailyMoodDictionary = new Dictionary<string, Dictionary<string, CurrentMood>>();
        dailyMoodDictionary = _dailyMoodManagerSave.dailyMoodDictionary;
    }
}
