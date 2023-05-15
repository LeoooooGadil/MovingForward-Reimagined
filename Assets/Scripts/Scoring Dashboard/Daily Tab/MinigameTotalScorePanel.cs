using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTotalScorePanel : MonoBehaviour
{
	public Text ScoreText;

	public float currentTimer = 0;

	void OnEnable()
	{
        ScoreText.text = "Loading...";
        UpdateData();
	}

	void Update()
	{
		if (currentTimer < 0.5)
		{
			currentTimer += Time.deltaTime;
		}
		else
		{
			currentTimer = 0;
			UpdateData();
		}
	}

	void UpdateData()
	{
		if (Aggregator.instance == null) return;

		Dictionary<string, WordleAggregate> loadedTodaysWordle = Aggregator.instance.GetTodaysWordleLogs();
		Dictionary<string, NumberLocationAggregate> loadedTodaysNumberLocation = Aggregator.instance.GetTodaysNumberLocationLogs();

		float score = 0;

		foreach (KeyValuePair<string, WordleAggregate> entry in loadedTodaysWordle)
		{
			score += entry.Value.totalPoints;
		}

		foreach (KeyValuePair<string, NumberLocationAggregate> entry in loadedTodaysNumberLocation)
		{
			score += entry.Value.totalPoints;
		}

		if (score == 0)
		{
			ScoreText.text = "No points";
		}
		else
		{
			ScoreText.text = score + " points";
		}
	}
}
