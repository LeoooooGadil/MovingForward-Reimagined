using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyTaskSaveData
{
    public List<DailyTask> dailyTasks;

    public DailyTaskSaveData(DailyTaskSave _dailyTaskSave)
    {
        dailyTasks = new List<DailyTask>();
        foreach (DailyTask dailyTask in _dailyTaskSave.dailyTasks)
        {
            dailyTasks.Add(dailyTask);
        }
    }
}
