using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregatorSave
{
    public Dictionary<int, DailyTaskAggregate> dailyTaskLogs = new Dictionary<int, DailyTaskAggregate>();

    public AggregatorSave(AggregatorSaveData _aggregatorSaveData)
    {
        dailyTaskLogs = new Dictionary<int, DailyTaskAggregate>();

        foreach (KeyValuePair<int, DailyTaskAggregate> dailyTaskLog in _aggregatorSaveData.dailyTaskLogs)
        {
            dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
        }
    }

    public AggregatorSave()
    {
        dailyTaskLogs = new Dictionary<int, DailyTaskAggregate>();
    }

    public void setDailyTaskLogs(Dictionary<int, DailyTaskAggregate> _dailyTaskLogs)
    {
        dailyTaskLogs = _dailyTaskLogs;
    }
}
