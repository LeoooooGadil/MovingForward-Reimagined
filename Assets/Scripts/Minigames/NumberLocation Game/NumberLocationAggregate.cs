using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumberLocationAggregate : IAggregate
{
	public string taskName;
	public int livesLeft;
	public NumberLocationDifficulty.Difficulty difficulty;
	public int wonRounds;
	public float totalPoints;
	public int timestamp;

	public string GetCSVHeader()
	{
		return "taskName,livesLeft,difficulty,wonRounds,totalPoints,timestamp";
	}

	public string GetCSVData()
	{
		return taskName + "," + livesLeft + "," + GetDifficulty() + "," + wonRounds + "," + totalPoints + "," + timestamp;
	}

	public string GetDifficulty()
	{
        return difficulty.ToString();
	}
}

public class NumberLocationCompletedEvent
{
    public string taskName;
	public int livesLeft;
	public NumberLocationDifficulty.Difficulty difficulty;
	public int wonRounds;
	public float totalPoints;

    public NumberLocationCompletedEvent(string _taskName, int _livesLeft, NumberLocationDifficulty.Difficulty _difficulty, int _wonRounds, float _totalPoints)
    {
        taskName = _taskName;
        livesLeft = _livesLeft;
        difficulty = _difficulty;
        wonRounds = _wonRounds;
        totalPoints = _totalPoints;
    }

    public NumberLocationAggregate GetData()
    {
        NumberLocationAggregate numberLocationAggregate = new NumberLocationAggregate();
        numberLocationAggregate.taskName = taskName;
        numberLocationAggregate.livesLeft = livesLeft;
        numberLocationAggregate.difficulty = difficulty;
        numberLocationAggregate.wonRounds = wonRounds;
        numberLocationAggregate.totalPoints = totalPoints;
        numberLocationAggregate.timestamp = TimeStamp.GetTimeStamp();

        return numberLocationAggregate;
    }
}
