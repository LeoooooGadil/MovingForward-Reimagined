using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregatorSave
{
    private List<string> keys = new List<string>();

    public Dictionary<string, DailyTaskAggregate> dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();

    public AggregatorSave(AggregatorSaveData _aggregatorSaveData)
    {
        dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();

        foreach (KeyValuePair<string, DailyTaskAggregate> dailyTaskLog in _aggregatorSaveData.dailyTaskLogs)
        {
            dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
        }
    }

    public AggregatorSave()
    {
        dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();
    }

    public void setDailyTaskLogs(Dictionary<string, DailyTaskAggregate> _dailyTaskLogs)
    {
        dailyTaskLogs = _dailyTaskLogs;
    }
}
