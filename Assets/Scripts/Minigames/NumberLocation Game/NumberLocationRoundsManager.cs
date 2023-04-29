using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationRoundsManager : MonoBehaviour
{
    public Text roundText;

    public int round = 1;
    public int maxRounds = 5;

    void Update()
    {
        roundText.text = "Round " + round + "/" + maxRounds;
    }
}
