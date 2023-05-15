using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyScoreManager : MonoBehaviour
{
	public Text targetScoreText;
	public Text currentScoreText;
	public float dailyScore;
	private float currentScore;
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
		targetScoreText.text = "Target: " + NumberFormatter.FormatNumberWithThousandsSeparator(maxDailyScore);
        UpdateCurrentScoreText();
	}

	void OnDisable()
	{
		dailyScore = 0;
		currentScore = 0;
		progressBar.fillAmount = 0;
	}

	void Update()
	{
		UpdateProgressBar();
		UpdateCurrentScoreText();
	}

	void UpdateProgressBar()
	{
		float dailyScoreNormalied = ConvertScoreToNormalized(0, maxDailyScore, dailyScore);
		progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, dailyScoreNormalied, Time.deltaTime * 5);
	}

	void UpdateCurrentScoreText()
	{
		if(currentScore == dailyScore) return;

		if(dailyScore == 0)
		{
			currentScoreText.text = "";
			return;
		}

		if (currentScore < dailyScore)
        {
            currentScore = Mathf.Lerp(currentScore, dailyScore, Time.deltaTime * 5);
            currentScoreText.text = NumberFormatter.FormatNumberWithThousandsSeparator(currentScore) + " pts";
        }
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




