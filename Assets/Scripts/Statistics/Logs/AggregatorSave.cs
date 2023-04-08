using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregatorSave
{
    public List<string> keys = new List<string>();

    public Dictionary<string, DailyTaskAggregate> dailyTaskLogs = new Dictionary<string, DailyTaskAggregate>();
    public Dictionary<string, NumberLocationAggregate> numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();

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
        numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
    }

    public void setDailyTaskLogs(Dictionary<string, DailyTaskAggregate> _dailyTaskLogs)
    {
        dailyTaskLogs = _dailyTaskLogs;
    }

    public void setNumberLocationLogs(Dictionary<string, NumberLocationAggregate> _numberLocationLogs)
    {
        numberLocationLogs = _numberLocationLogs;
    }
}
