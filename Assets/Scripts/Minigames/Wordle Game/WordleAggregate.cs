using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordleAggregate : IAggregate
{
	public string taskName;
	public int tries;
	public MovingForwardWordleWordsObject.Word word;
	public float totalPoints;
	public int timestamp;

	public string GetCSVHeader()
	{
		return "taskName,tries,word,totalPoints,timestamp";
	}

	public string GetCSVData()
	{
		return taskName + "," + tries + "," + word + "," + totalPoints + "," + timestamp;
	}
}

public class WordleCompletedEvent
{
    public string taskName;
    public int tries;
    public MovingForwardWordleWordsObject.Word word;
    public float totalPoints;

    public WordleCompletedEvent(string _taskName, int _tries, MovingForwardWordleWordsObject.Word _word, float _totalPoints)
    {
        taskName = _taskName;
        tries = _tries;
        word = _word;
        totalPoints = _totalPoints;
    }

    public WordleAggregate GetData()
    {
        WordleAggregate wordleAggregate = new WordleAggregate();
        wordleAggregate.taskName = taskName;
        wordleAggregate.tries = tries;
        wordleAggregate.word = word;
        wordleAggregate.totalPoints = totalPoints;
        wordleAggregate.timestamp = TimeStamp.GetTimeStamp();

        return wordleAggregate;
    }
}