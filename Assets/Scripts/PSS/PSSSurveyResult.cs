using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSSSurveyResult : MonoBehaviour
{
	public Text ScoreText;
	public Text ScoreInterpretationText;
	public Text ScoreDifferenceText;
	public int score = 0;

	void Start()
	{
		ScoreText.text = score.ToString();
		UpdateInterpretation();
        UpdateDifference();
	}

	void UpdateInterpretation()
	{
		// 0-13: Low stress
		// 14-26: Moderate stress
		// 27-40: High perceived stress

		if (score >= 0 && score <= 13)
		{
			ScoreInterpretationText.text = "Low stress";
		}
		else if (score >= 14 && score <= 26)
		{
			ScoreInterpretationText.text = "Moderate stress";
		}
		else if (score >= 27 && score <= 40)
		{
			ScoreInterpretationText.text = "High perceived stress";
		}
		else
		{
			ScoreInterpretationText.text = "Error";
		}
	}

	void UpdateDifference()
	{
        ScoreDifferenceText.text = "";
    }

	internal void SetResult(int score)
	{
		this.score = score;
        ScoreText.text = score.ToString();
        UpdateInterpretation();
        UpdateDifference();
	}
}
