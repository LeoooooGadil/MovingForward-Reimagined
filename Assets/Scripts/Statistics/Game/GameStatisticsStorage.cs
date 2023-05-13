using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatisticsStorage
{
	public static GameStatisticsSave gameStatisticsSave;
	public static string saveFileName = "gameStatisticsStorage";

	static void LoadGameStatisticsStorage()
	{
		GameStatisticsSaveData gameStatisticsSaveData = SaveSystem.Load(saveFileName) as GameStatisticsSaveData;

		if (gameStatisticsSaveData == null)
		{
			gameStatisticsSave = new GameStatisticsSave();
			return;
		}

		gameStatisticsSave = new GameStatisticsSave(gameStatisticsSaveData);
	}

    public static bool isVeryFirstTimePlayed()
    {
        LoadGameStatisticsStorage();

        if (gameStatisticsSave.veryFirstTimePlayed == 0)
        {
            return true;
        }

        return false;
    }

	public static void VeryFirstTimePlayed(long timestamp)
	{
		LoadGameStatisticsStorage();

		gameStatisticsSave.veryFirstTimePlayed = timestamp;

		SaveGameStatisticsStorage();
	}

	public static void LastTimePlayed(long timestamp)
	{
        LoadGameStatisticsStorage();

        gameStatisticsSave.lastTimePlayed = timestamp;

        SaveGameStatisticsStorage();
    }

    public static void LastTimeStartedPlaying(long timestamp)
    {
        LoadGameStatisticsStorage();

        gameStatisticsSave.lastTimeStartedPlaying = timestamp;

        SaveGameStatisticsStorage();
    }

    public static void TotalTimesPlayedPerDay(string date, TimeSpan timeSpan)
    {
        LoadGameStatisticsStorage();

        gameStatisticsSave.totalTimesPlayedPerDay[date] = timeSpan;

        SaveGameStatisticsStorage();
    }

	public static void SaveGameStatisticsStorage()
	{
		GameStatisticsSaveData gameStatisticsSaveData = new GameStatisticsSaveData(gameStatisticsSave);
		SaveSystem.Save(saveFileName, gameStatisticsSaveData);
	}
}
