using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyTaskAggregateV2 : IAggregate
{
	public string taskName;
	public float points;
	public int timestamp;
	public float compensation;

	public string GetCSVHeader()
	{
		return "taskName,points,timestamp,compensation";
	}

	public string GetCSVData()
	{
		return taskName + "," + points + "," + "," + compensation + timestamp;
	}
}

public class TaskCompletedEvent
{
	public Task dailyTask;

	public TaskCompletedEvent(Task _task)
	{
		dailyTask = _task;
	}

	public DailyTaskAggregateV2 GetData()
	{
		DailyTaskAggregateV2 dailyTaskAggregateV2 = new DailyTaskAggregateV2();
		dailyTaskAggregateV2.taskName = dailyTask.taskName;
		dailyTaskAggregateV2.points = dailyTask.taskPoints;
		dailyTaskAggregateV2.compensation = dailyTask.taskCompenstation;
		dailyTaskAggregateV2.timestamp = TimeStamp.GetTimeStamp();

		return dailyTaskAggregateV2;
	}
}
