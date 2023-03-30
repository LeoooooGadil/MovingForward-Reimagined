using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSave : MonoBehaviour
{
    private int score = 50; // the score is between 0 and 100.
    // 50 is the default score because under 50 is bad and over 50 is good

    public ScoringSave(ScoringSaveData data)
    {
        score = data.Score;
    }

    public ScoringSave()
    {
        score = 50;
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
}
