using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JournalAggregate : IAggregate
{
	public string taskName;
	public string entryId;
	public int timestamp;

	public string GetCSVData()
	{
		return "taskName,entryId,timestamp";
	}

	public string GetCSVHeader()
	{
		return taskName + "," + entryId + "," + timestamp;
	}
}

public class JournalCompletedEvent
{
    public string taskName;
    public string entryId;

    public JournalCompletedEvent(string _taskName, string _entryId)
    {
        taskName = _taskName;
        entryId = _entryId;
    }

    public JournalAggregate GetData()
    {
        JournalAggregate journalAggregate = new JournalAggregate();
        journalAggregate.taskName = taskName;
        journalAggregate.entryId = entryId;
        journalAggregate.timestamp = TimeStamp.GetTimeStamp();

        return journalAggregate;
    }
}
