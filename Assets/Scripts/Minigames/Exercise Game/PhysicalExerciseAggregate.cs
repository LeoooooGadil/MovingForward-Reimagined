using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicalExerciseAggregate : IAggregate
{
	public string taskName;
	public int isFinished;
	public int timestamp;

	public string GetCSVData()
	{
		return "taskName,isFinished,timestamp";
	}

	public string GetCSVHeader()
	{
		return taskName + "," + isFinished + "," + timestamp;
	}
}

public class PhysicalExerciseCompletedEvent
{
	public string taskName;
	public int isFinished;

	public PhysicalExerciseCompletedEvent(string _taskName, bool _isFinished)
	{
		taskName = _taskName;
		isFinished = _isFinished ? 1 : 0;
	}

	public PhysicalExerciseAggregate GetData()
	{
		PhysicalExerciseAggregate phyiscalExerciseCompletedEvent = new PhysicalExerciseAggregate();
		phyiscalExerciseCompletedEvent.taskName = taskName;
		phyiscalExerciseCompletedEvent.isFinished = isFinished;
		phyiscalExerciseCompletedEvent.timestamp = TimeStamp.GetTimeStamp();

		return phyiscalExerciseCompletedEvent;
	}
}