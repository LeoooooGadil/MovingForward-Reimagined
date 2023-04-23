using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTotalScorePanel : MonoBehaviour
{
	public Text ScoreText;

	private float totalScore;
    private float currentScore;

	void OnEnable()
	{
		GetTotalScore();
	}

	void Update()
	{
        if (currentScore < totalScore)
        {
            currentScore = Mathf.Lerp(currentScore, totalScore, Time.deltaTime * 5);
            ScoreText.text = currentScore.ToString("F0") + " pts";;
        }
	}

	void GetTotalScore()
	{
        currentScore = 0;
		totalScore = 0;

		List<DailyScoreStorageItem> dailyScoreStorageItems = DailyScoreStorage.GetDailyScoreStorageItems();

		foreach (DailyScoreStorageItem dailyScoreStorageItem in dailyScoreStorageItems)
		{
			if (dailyScoreStorageItem.dailyScoreStorageType == DailyScoreStorageType.Minigame)
			{
				totalScore += dailyScoreStorageItem.score;
			}
		}
	}
}
