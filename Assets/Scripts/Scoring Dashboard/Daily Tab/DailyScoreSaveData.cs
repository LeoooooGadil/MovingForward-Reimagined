using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyScoreSaveData
{
    public float dailyScore;
    public List<string> keys;

    public DailyScoreSaveData(DailyScoreSave dailyScoreSave)
    {
        dailyScore = dailyScoreSave.dailyScore;
        keys = dailyScoreSave.keys;
    }
}
