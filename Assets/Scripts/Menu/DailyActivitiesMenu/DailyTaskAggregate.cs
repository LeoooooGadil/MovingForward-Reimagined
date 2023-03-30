using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyTaskAggregate : IAggregate
{
	public string taskName;
	public float points;
	public int timestamp;

	public string GetCSVHeader()
	{
		return "taskName,points,timestamp";
	}

	public string GetCSVData()
	{
		return taskName + "," + points + "," + timestamp;
	}
}

public class DailyTaskCompletedEvent
{
	public DailyTask dailyTask;

	public DailyTaskCompletedEvent(DailyTask _dailyTask)
	{
		dailyTask = _dailyTask;
	}

	public DailyTaskAggregate GetData()
	{
		DailyTaskAggregate dailyTaskAggregate = new DailyTaskAggregate();
		dailyTaskAggregate.taskName = dailyTask.name;
		dailyTaskAggregate.points = dailyTask.points;
		dailyTaskAggregate.timestamp = TimeStamp.GetTimeStamp();

		return dailyTaskAggregate;
	}
}
