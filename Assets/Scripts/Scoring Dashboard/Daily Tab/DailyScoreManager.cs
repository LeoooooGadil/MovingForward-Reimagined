using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyScoreManager : MonoBehaviour
{
	public Text targetScoreText;
	public Text currentScoreText;
	public float dailyScore;
	private float maxDailyScore = 2500;
	public Image progressBar;

	void Start()
	{
		dailyScore = DailyScoreCalculator.GetDailyScore();
        UpdateCurrentScoreText();
	}

	void OnEnable()
	{
		dailyScore = DailyScoreCalculator.GetDailyScore();
		targetScoreText.text = "Target: " + maxDailyScore;
        UpdateCurrentScoreText();
	}

	void OnDisable()
	{
		dailyScore = 0;
		progressBar.fillAmount = 0;
	}

	void Update()
	{
		UpdateProgressBar();
		
	}

	void UpdateProgressBar()
	{
		float dailyScoreNormalied = ConvertScoreToNormalized(0, maxDailyScore, dailyScore);
		progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, dailyScoreNormalied, Time.deltaTime * 5);
	}

	void UpdateCurrentScoreText()
	{
		if(dailyScore == 0)
		{
			currentScoreText.text = "";
			return;
		}

		currentScoreText.text = dailyScore + "pts";
	}

	float ConvertScoreToNormalized(float min, float max, float score)
	{
		float fromMin = 0;
		float fromMax = max;
		float toMin = 0;
		float toMax = 1;

		float experienceNormalized = (score - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;

		return Mathf.Round(experienceNormalized * 100) / 100;
	}
}




