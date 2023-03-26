using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggregator : MonoBehaviour
{
	public static Aggregator instance;

	private List<string> keys = new List<string>();
	private string saveFileName = "aggregatorSaveData";

	Dictionary<string, DailyTaskAggregate> dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();

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

	public void Publish(DailyTaskCompletedEvent dailyTaskCompletedEvent)
	{
		DailyTaskAggregate dailyTaskAggregate = dailyTaskCompletedEvent.GetData();
		dailyTaskLogs.Add(generateKey(), dailyTaskAggregate);

		SaveAggregator();
	}

	public void SaveAggregator()
	{
		AggregatorSave aggregatorSave = new AggregatorSave();
		aggregatorSave.setDailyTaskLogs(dailyTaskLogs);

		AggregatorSaveData aggregatorSaveData = new AggregatorSaveData(keys, aggregatorSave);

		SaveSystem.Save(saveFileName, aggregatorSaveData);
	}

	public string generateKey()
	{
		while (true)
		{
			string key = KeyGenerator.GetKey();
			if (!keys.Contains(key))
			{
				keys.Add(key);
				Debug.Log("Key: " + key);
				return key;
			}
		}
	}
}
