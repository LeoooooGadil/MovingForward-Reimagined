using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AggregatorSaveData
{
	public List<string> keys = new List<string>();

	public Dictionary<string, DailyTaskAggregate> dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();

	public AggregatorSaveData(
		List<string> _keys,
		AggregatorSave _aggregatorSave
		)
	{
		keys = _keys;

		dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();

		foreach (KeyValuePair<string, DailyTaskAggregate> dailyTaskLog in _aggregatorSave.dailyTaskLogs)
		{
			dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
		}
	}
}
