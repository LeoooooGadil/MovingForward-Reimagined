using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DailyScoreCalculator
{
    public static DailyScoreSave dailyScoreSave;
    public static string saveFileName = "dailyScoreSaveData";

    static void LoadDailyScore()
    {
        DailyScoreSaveData dailyScoreSaveData = SaveSystem.Load(saveFileName) as DailyScoreSaveData;

        if (dailyScoreSaveData == null)
        {
            dailyScoreSave = new DailyScoreSave();
            return;
        }

        dailyScoreSave = new DailyScoreSave(dailyScoreSaveData);
    }

    public static void PublishDailyTask(string key, DailyTaskAggregate dailyTaskAggregate)
    {
        LoadDailyScore();

        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(dailyTaskAggregate.timestamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        if (dateTime.Day == DateTime.Now.Day)
        {
            dailyScoreSave.dailyScore += dailyTaskAggregate.points;
            dailyScoreSave.keys.Add(key);

            SaveDailyScore();
        }
    }

    public static void PublishNumberLocation(string key, NumberLocationAggregate numberLocationAggregate)
    {
        LoadDailyScore();

        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(numberLocationAggregate.timestamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        if (dateTime.Day == DateTime.Now.Day)
        {
            dailyScoreSave.dailyScore += numberLocationAggregate.totalPoints;
            dailyScoreSave.keys.Add(key);

            SaveDailyScore();
        }
    }

    public static float GetDailyScore()
    {
        LoadDailyScore();
        return dailyScoreSave.dailyScore;
    }

    public static void SaveDailyScore()
    {
        DailyScoreSaveData dailyScoreSaveData = new DailyScoreSaveData(dailyScoreSave);
        SaveSystem.Save(saveFileName, dailyScoreSaveData);
    }
}
