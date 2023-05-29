using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour, IPointerClickHandler
{
	public Text scoreText;
	public Text scoreIndicatorText;

	public float todayScore;
	public float totalScore;

	public bool isShowTodayScore = true;

	void Start()
	{
		LoadPlayerScore();
	}

	void Update()
	{
		UpdateScore();
	}

	void UpdateScore()
	{
		if (isShowTodayScore)
		{
			scoreText.text = NumberFormatter.FormatNumberWithThousandsSeparator(todayScore);
			scoreIndicatorText.text = "PTS";
		}
		else
		{
			scoreText.text = NumberFormatter.FormatNumberWithThousandsSeparator(totalScore);
			scoreIndicatorText.text = "PTS";
		}
	}

	private void LoadPlayerScore()
	{
		float _todayScore = DailyScoreCalculator.GetDailyScore();
		float _totalScore = DailyScoreCalculator.GetTotalScore();

		todayScore = _todayScore;
		totalScore = _totalScore;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManager.instance.PlaySFX("PopClick");
		isShowTodayScore = !isShowTodayScore;

		if(isShowTodayScore) OnScreenNotificationManager.instance.CreateNotification("Changed to Today's Score", OnScreenNotificationType.Info);
		else OnScreenNotificationManager.instance.CreateNotification("Changed to Total Score", OnScreenNotificationType.Info);
	}
}
