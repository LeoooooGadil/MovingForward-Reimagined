using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskSaveV2
{
	public List<Task> dailyTasks;
	public List<Task> completedTasks;
	public DateTime date;

	public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> lowPriorityTasks;
	public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> mediumPriorityTasks;
	public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> highPriorityTasks;

	public DailyTaskSaveV2(DailyTaskSaveDataV2 _dailyTaskSaveData)
	{
		dailyTasks = new List<Task>();
		completedTasks = new List<Task>();

		lowPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
		mediumPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
		highPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();

		foreach (Task task in _dailyTaskSaveData.dailyTasks)
		{
			dailyTasks.Add(task);
		}

		foreach (Task task in _dailyTaskSaveData.completedTasks)
		{
			completedTasks.Add(task);
		}

		date = _dailyTaskSaveData.date;

		foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in _dailyTaskSaveData.lowPriorityTasks)
		{
			lowPriorityTasks.Add(task);
		}

        foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in _dailyTaskSaveData.mediumPriorityTasks)
        {
            mediumPriorityTasks.Add(task);
        }

        foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in _dailyTaskSaveData.highPriorityTasks)
        {
            highPriorityTasks.Add(task);
        }
	}

	public DailyTaskSaveV2()
	{
		dailyTasks = new List<Task>();
		completedTasks = new List<Task>();

        lowPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
        mediumPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
        highPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();

		date = DateTime.Now;
	}

	public string GetDate()
	{
		return date.ToString("dd/MM/yyyy");
	}

	public void AddDailyTask(Task _dailyTask)
	{
		dailyTasks.Add(_dailyTask);
	}

	public void RemoveDailyTask(Task _dailyTask)
	{
		dailyTasks.Remove(_dailyTask);
	}

	public int GetDailyTaskCount()
	{
		return dailyTasks.Count;
	}

	internal void CompleteTask(Task task)
	{
		completedTasks.Add(task.SetCompleted(true));
		dailyTasks.Remove(task);
	}
}

[System.Serializable]
public class Task
{
	public string taskName;
	public float taskPoints;
	public float taskCompenstation;
	public MovingForwardDailyTasksObject.DailyTaskPriority taskPriority;
	public bool isCompleted;

	public Task(string _taskName, float _taskPoints, MovingForwardDailyTasksObject.DailyTaskPriority _taskPriority, float _taskCompenstation, bool _isCompleted)
	{
		taskName = _taskName;
		taskPoints = _taskPoints;
		taskPriority = _taskPriority;
		taskCompenstation = _taskCompenstation;
		isCompleted = _isCompleted;
	}

	public Task(MovingForwardDailyTasksObject.MoivngForwardDailyTask _task)
	{
		taskName = _task.name;
		taskPoints = _task.points;
		taskPriority = _task.priority;
		isCompleted = false;
	}

	public Task SetCompleted(bool value)
	{
		isCompleted = value;
		return this;
	}

	public bool IsCompleted()
	{
		return isCompleted;
	}

	public Task SetCompensation(float _compensation)
	{
		taskCompenstation = _compensation;
		return this;
	}

	public Task SetCompensation(Task _task)
	{
		taskCompenstation = _task.taskCompenstation;
		return this;
	}

	public float GetCompensation()
	{
		return taskCompenstation;
	}
}