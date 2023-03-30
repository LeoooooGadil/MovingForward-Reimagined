using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoringSaveData : MonoBehaviour
{
    private int score;

    public ScoringSaveData(ScoringSave scoringSave)
    {
        score = scoringSave.Score;
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
}
