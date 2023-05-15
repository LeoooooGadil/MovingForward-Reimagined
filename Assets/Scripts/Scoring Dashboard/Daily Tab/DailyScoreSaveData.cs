using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyScoreSaveData
{
    public Dictionary<string, DailyScoreItem> dailyScores = new Dictionary<string, DailyScoreItem>();

    public DailyScoreSaveData(DailyScoreSave dailyScoreSave)
    {
        dailyScores = new Dictionary<string, DailyScoreItem>();

        foreach (KeyValuePair<string, DailyScoreItem> dailyScoreItem in dailyScoreSave.dailyScores)
        {
            dailyScores.Add(dailyScoreItem.Key, dailyScoreItem.Value);
        }
    }
}
