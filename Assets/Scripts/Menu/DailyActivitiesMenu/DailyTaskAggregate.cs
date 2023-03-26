using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyTaskAggregate
{
	public string taskName;
	public float points;
	public int timestamp;
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
