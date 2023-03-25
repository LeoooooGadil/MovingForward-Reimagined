using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyTask : MovingForwardDailyTasksObject.MoivngForwardDailyTask
{
	[SerializeField]
	public bool isCompleted = false;

	public DailyTask(string name, int points, MovingForwardDailyTasksObject.DailyTaskPriority priority)
	{
		this.name = name;
		this.points = points;
		this.priority = priority;
	}

	public void SetCompleted(bool value)
	{
		isCompleted = value;
	}

	public bool IsCompleted()
	{
		return isCompleted;
	}
}
