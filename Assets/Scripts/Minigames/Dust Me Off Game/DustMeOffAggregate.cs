using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DustMeOffAggregate : IAggregate
{

	public string taskName;
	public int totalScore;
	public int timestamp;

	public string GetCSVData()
	{
		return "taskName,totalScore,timestamp";
	}

	public string GetCSVHeader()
	{
		return taskName + "," + totalScore + "," + timestamp;
	}
}

public class DustMeOffCompletedEvent
{
	public string taskName;
	public int totalScore;

	public DustMeOffCompletedEvent(string _taskName, int _totalScore)
	{
		taskName = _taskName;
		totalScore = _totalScore;
	}

	public DustMeOffAggregate GetData()
	{
        DustMeOffAggregate dustMeOffAggregate = new DustMeOffAggregate();
        dustMeOffAggregate.taskName = taskName;
        dustMeOffAggregate.totalScore = totalScore;
        dustMeOffAggregate.timestamp = TimeStamp.GetTimeStamp();

        return dustMeOffAggregate;
	}
}