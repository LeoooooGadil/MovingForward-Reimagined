using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TakeMeOutAggregate : IAggregate
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

public class TakeMeOutCompletedEvent
{
    public string taskName;
    public int totalScore;

    public TakeMeOutCompletedEvent(string _taskName, int _totalScore)
    {
        taskName = _taskName;
        totalScore = _totalScore;
    }

    public TakeMeOutAggregate GetData()
    {
        TakeMeOutAggregate takeMeOutAggregate = new TakeMeOutAggregate();
        takeMeOutAggregate.taskName = taskName;
        takeMeOutAggregate.totalScore = totalScore;
        takeMeOutAggregate.timestamp = TimeStamp.GetTimeStamp();

        return takeMeOutAggregate;
    }
}
