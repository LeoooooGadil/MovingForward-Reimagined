using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoresAggregate : IAggregate
{
	public string taskName;
	public DailyChoreRoom room;
	public DailyChoreType type;
    public float points;
	public bool isMandatory;
	public int timestamp;

	public string GetCSVHeader()
	{
		return "taskName,room,type,isMandatory,timestamp";
	}

	public string GetCSVData()
	{
		return taskName + "," + room + "," + type + "," + points + "," + isMandatory + "," + timestamp;
	}
}

public class ChoreCompletedEvent
{
    public Chore chore;

    public ChoreCompletedEvent(Chore _chore)
    {
        chore = _chore;
    }

    public ChoresAggregate GetData()
    {
        ChoresAggregate choresAggregate = new ChoresAggregate();
        choresAggregate.taskName = chore.dailyChoreRoom + ":" + chore.dailyChoreType + " " + chore.choreName;
        choresAggregate.room = chore.dailyChoreRoom;
        choresAggregate.type = chore.dailyChoreType;
        choresAggregate.isMandatory = chore.isMandatory;
        choresAggregate.timestamp = TimeStamp.GetTimeStamp();

        return choresAggregate;
    }
}