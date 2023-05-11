using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighScoreStorage
{
    public static HighScoreStorageSave highScoreStorageSave;

    private static string saveFileName = "HighScoreStorageSave";

    public static void SaveHighScore(string key, long value)
    {
        LoadHighScore();

        highScoreStorageSave.SaveHighScore(key, value);

        SaveHighScore();
    }

    public static long GetHighScore(string key)
    {
        LoadHighScore();
        return highScoreStorageSave.GetHighScore(key);
    }

    static void LoadHighScore()
    {
        HighScoreStorageSaveData highScoreStorageSaveData = SaveSystem.Load(saveFileName) as HighScoreStorageSaveData;

        if (highScoreStorageSaveData != null)
        {
            highScoreStorageSave = new HighScoreStorageSave(highScoreStorageSaveData);
        }
        else
        {
            highScoreStorageSave = new HighScoreStorageSave();
        }
    }

    public static void SaveHighScore()
    {
        HighScoreStorageSaveData highScoreStorageSaveData = new HighScoreStorageSaveData(highScoreStorageSave);
        SaveSystem.Save(saveFileName, highScoreStorageSaveData);
    }
}
