using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyTaskSaveDataV2
{
    public List<Task> dailyTasks;
    public List<Task> completedTasks;
    public DateTime date;

    public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> lowPriorityTasks;
    public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> mediumPriorityTasks;
    public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> highPriorityTasks;

    public DailyTaskSaveDataV2(DailyTaskSaveV2 _dailyTaskSave)
    {
        dailyTasks = new List<Task>();
        completedTasks = new List<Task>();

        lowPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
        mediumPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
        highPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();

        foreach (Task task in _dailyTaskSave.dailyTasks)
        {
            dailyTasks.Add(task);
        }

        foreach (Task task in _dailyTaskSave.completedTasks)
        {
            completedTasks.Add(task);
        }

        date = _dailyTaskSave.date;

        foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in _dailyTaskSave.lowPriorityTasks)
        {
            lowPriorityTasks.Add(task);
        }

        foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in _dailyTaskSave.mediumPriorityTasks)
        {
            mediumPriorityTasks.Add(task);
        }

        foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in _dailyTaskSave.highPriorityTasks)
        {
            highPriorityTasks.Add(task);
        }
    }
}
