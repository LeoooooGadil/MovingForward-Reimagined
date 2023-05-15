using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyScoreSave
{
	public Dictionary<string, DailyScoreItem> dailyScores = new Dictionary<string, DailyScoreItem>();

	public DailyScoreSave()
	{
		dailyScores = new Dictionary<string, DailyScoreItem>();
	}

	public DailyScoreSave(DailyScoreSaveData dailyScoreSaveData)
	{
		dailyScores = new Dictionary<string, DailyScoreItem>();

        foreach (KeyValuePair<string, DailyScoreItem> dailyScoreItem in dailyScoreSaveData.dailyScores)
        {
            dailyScores.Add(dailyScoreItem.Key, dailyScoreItem.Value);
        }
	}

    public void Add(DailyScoreItem dailyScoreItem)
    {
        string todayString = DateTime.Today.ToString("dd/MM/yyyy");
        
        if (!dailyScores.ContainsKey(todayString))
        {
            dailyScores.Add(todayString, dailyScoreItem);
        } else
        {
            dailyScores[todayString] = dailyScoreItem;
        }
    }

    public DailyScoreItem GetToday()
    {
        string todayString = DateTime.Today.ToString("dd/MM/yyyy");

        if (dailyScores.ContainsKey(todayString))
        {
            return dailyScores[todayString];
        }

        return new DailyScoreItem();
    }
}

[System.Serializable]
public class DailyScoreItem
{
    public float dailyScore;
    public List<string> dailyScoreStorageKeys = new List<string>();
    public string date;

    public DailyScoreItem(float _dailyScore, string _date, List<string> _dailyScoreStorageKeys)
    {
        dailyScore = _dailyScore;
        date = _date;
        dailyScoreStorageKeys = _dailyScoreStorageKeys;
    }

    public DailyScoreItem()
    {
        dailyScore = 0;
        date = DateTime.Today.ToString("dd/MM/yyyy");
        dailyScoreStorageKeys = new List<string>();
    }

    public DailyScoreItem Add(float score, string key)
    {
        dailyScore += score;
        dailyScoreStorageKeys.Add(key);

        return this;
    }

    public void AddDailyScoreStorageKey(string key)
    {
        dailyScoreStorageKeys.Add(key);
    }

    public void Reset()
    {
        dailyScore = 0;
    }

	internal float GetTotalScore()
	{
        return dailyScore;
	}
}
