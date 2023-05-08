using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregatorSave
{
	public List<string> keys = new List<string>();

	public Dictionary<string, DailyTaskAggregateV2> dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
	public Dictionary<string, NumberLocationAggregate> numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
	public Dictionary<string, WordleAggregate> wordleLogs = new Dictionary<string, WordleAggregate>();

	public AggregatorSave(AggregatorSaveData _aggregatorSaveData)
	{
		dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
		numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
		wordleLogs = new Dictionary<string, WordleAggregate>();

		foreach (KeyValuePair<string, DailyTaskAggregateV2> dailyTaskLog in _aggregatorSaveData.dailyTaskLogs)
		{
			dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
		}

		foreach (KeyValuePair<string, NumberLocationAggregate> numberLocationLog in _aggregatorSaveData.numberLocationLogs)
		{
            numberLocationLogs.Add(numberLocationLog.Key, numberLocationLog.Value);
		}

        foreach (KeyValuePair<string, WordleAggregate> wordleLog in _aggregatorSaveData.wordleLogs)
        {
            wordleLogs.Add(wordleLog.Key, wordleLog.Value);
        }
	}

	public AggregatorSave()
	{
		dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
		numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
        wordleLogs = new Dictionary<string, WordleAggregate>();
	}

	public void setDailyTaskLogs(Dictionary<string, DailyTaskAggregateV2> _dailyTaskLogs)
	{
		dailyTaskLogs = _dailyTaskLogs;
	}

	public void setNumberLocationLogs(Dictionary<string, NumberLocationAggregate> _numberLocationLogs)
	{
		numberLocationLogs = _numberLocationLogs;
	}

    public void setWordleLogs(Dictionary<string, WordleAggregate> _wordleLogs)
    {
        wordleLogs = _wordleLogs;
    }
}
