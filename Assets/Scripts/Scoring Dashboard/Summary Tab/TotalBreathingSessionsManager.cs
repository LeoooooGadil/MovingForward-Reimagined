using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalBreathingSessionsManager : MonoBehaviour
{
    public Text ScoreText;
	public int todaysSession = 0;

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

		Dictionary<string, BreathingExerciseV2Aggregate> loadedtodaysSession = Aggregator.instance.GetBreathingExerciseLogs();

		if (loadedtodaysSession.Count == 0)
		{
			ScoreText.text = "No sessions";
		}
		else
		{
			ScoreText.text = loadedtodaysSession.Count + " session" + (loadedtodaysSession.Count == 1 ? "" : "s");
		}
	}
}
