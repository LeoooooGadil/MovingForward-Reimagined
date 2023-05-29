using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalJournalEntriesManager : MonoBehaviour
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

		Dictionary<string, JournalAggregate> loadedTodaysJournalEntries = Aggregator.instance.GetJournalLogs();

		if (loadedTodaysJournalEntries.Count == 0)
		{
			ScoreText.text = "No entries";
		}
		else
		{
			ScoreText.text = loadedTodaysJournalEntries.Count + " entr" + (loadedTodaysJournalEntries.Count == 1 ? "y" : "ies");
		}
	}
}
