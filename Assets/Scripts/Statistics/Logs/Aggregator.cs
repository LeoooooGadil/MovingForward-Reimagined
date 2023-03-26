using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggregator : MonoBehaviour
{
	public static Aggregator instance;
	private string saveFileName = "aggregatorSaveData";

	Dictionary<int, DailyTaskAggregate> dailyTaskLogs = new Dictionary<int, DailyTaskAggregate>();

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		LoadAggregator();
	}

	public void LoadAggregator()
	{
		AggregatorSaveData aggregatorSaveData = SaveSystem.Load(saveFileName) as AggregatorSaveData;
		AggregatorSave aggregatorSave = new AggregatorSave(aggregatorSaveData);

        dailyTaskLogs = aggregatorSave.dailyTaskLogs;
	}

	public void AddDailyTaskLog(DailyTaskAggregate dailyTaskLog)
	{
		dailyTaskLogs.Add(dailyTaskLog.timestamp, dailyTaskLog);
	}

	public void SaveAggregator()
	{
        AggregatorSave aggregatorSave = new AggregatorSave();
        aggregatorSave.setDailyTaskLogs(dailyTaskLogs);

        AggregatorSaveData aggregatorSaveData = new AggregatorSaveData(aggregatorSave);

        SaveSystem.Save(saveFileName, aggregatorSaveData);
	}
}
