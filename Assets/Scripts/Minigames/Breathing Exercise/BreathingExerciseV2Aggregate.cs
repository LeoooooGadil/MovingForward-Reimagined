using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreathingExerciseV2Aggregate : IAggregate
{

	public string taskName;
	public int totalSet;
	public int timestamp;

	public string GetCSVData()
	{
		return "taskName,totalSet,timestamp";
	}

	public string GetCSVHeader()
	{
		return taskName + "," + totalSet + "," + timestamp;
	}
}

public class BreathingExerciseV2CompletedEvent
{
    public string taskName;
    public int totalSet;

    public BreathingExerciseV2CompletedEvent(string _taskName, int _totalSet)
    {
        taskName = _taskName;
        totalSet = _totalSet;
    }

    public BreathingExerciseV2Aggregate GetData()
    {
        BreathingExerciseV2Aggregate breathingExerciseV2Aggregate = new BreathingExerciseV2Aggregate();
        breathingExerciseV2Aggregate.taskName = taskName;
        breathingExerciseV2Aggregate.totalSet = totalSet;
        breathingExerciseV2Aggregate.timestamp = TimeStamp.GetTimeStamp();

        return breathingExerciseV2Aggregate;
    }
}
