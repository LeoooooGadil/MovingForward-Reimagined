using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyScoreSave
{
    public float dailyScore;
    public List<string> keys;

    public DailyScoreSave()
    {
        dailyScore = 0;
        keys = new List<string>();
    }

    public DailyScoreSave(DailyScoreSaveData dailyScoreSaveData)
    {
        dailyScore = dailyScoreSaveData.dailyScore;
        keys = dailyScoreSaveData.keys;
    }
}
