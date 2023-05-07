using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreStorageSave
{
    public Dictionary<string, long> highScores;

    public HighScoreStorageSave(HighScoreStorageSaveData highScoreStorageSaveData)
    {
        highScores = highScoreStorageSaveData.highScores;
    }

    public HighScoreStorageSave()
    {
        highScores = new Dictionary<string, long>();
    }

    public void SaveHighScore(string key, long value)
    {
        if (highScores.ContainsKey(key))
        {
            if (highScores[key] < value)
            {
                highScores[key] = value;
            }
        }
        else
        {
            highScores.Add(key, value);
        }
    }

    public long GetHighScore(string key)
    {
        if (highScores.ContainsKey(key))
        {
            return highScores[key];
        }
        else
        {
            return 0;
        }
    }
}
