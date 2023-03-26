using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AggregatorSaveData
{
	public Dictionary<int, DailyTaskAggregate> dailyTaskLogs = new Dictionary<int, DailyTaskAggregate>();

	public AggregatorSaveData(
		AggregatorSave _aggregatorSave
		)
	{
		dailyTaskLogs = new Dictionary<int, DailyTaskAggregate>();

		foreach (KeyValuePair<int, DailyTaskAggregate> dailyTaskLog in _aggregatorSave.dailyTaskLogs)
		{
			dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
		}
	}
}
