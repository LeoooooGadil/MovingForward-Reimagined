using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskSave
{
    public List<DailyTask> dailyTasks;

    public DailyTaskSave(DailyTaskSaveData _dailyTaskSaveData)
    {
        dailyTasks = new List<DailyTask>();
        foreach (DailyTask dailyTask in _dailyTaskSaveData.dailyTasks)
        {
            dailyTasks.Add(dailyTask);
        }
    }

    public DailyTaskSave() {
        dailyTasks = new List<DailyTask>();
    }

    public void AddDailyTask(DailyTask _dailyTask) {
        dailyTasks.Add(_dailyTask);
    }

    public void RemoveDailyTask(DailyTask _dailyTask) {
        dailyTasks.Remove(_dailyTask);
    }

    public void UpdateIsCompleted(string name, bool isComplete) {
        foreach (DailyTask dailyTask in dailyTasks) {
            if (dailyTask.name == name) {
                dailyTask.SetCompleted(isComplete);
                Aggregator.instance.Publish(new DailyTaskCompletedEvent(dailyTask));
                // use the experience manager to add the experience to the player
                ExperienceManager.instance.AddExperience(dailyTask.points);
            }

        }
    }

    public List<DailyTask> GetDailyTasks() {
        return dailyTasks;
    }

    public int GetDailyTaskCount() {
        return dailyTasks.Count;
    }
}
