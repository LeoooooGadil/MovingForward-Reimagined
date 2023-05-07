using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreStorageSaveData
{
    public Dictionary<string, long> highScores;

    public HighScoreStorageSaveData(HighScoreStorageSave highScoreStorageSave)
    {
        highScores = highScoreStorageSave.highScores;
    }
}
