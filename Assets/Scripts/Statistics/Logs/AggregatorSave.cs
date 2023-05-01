using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregatorSave
{
    public List<string> keys = new List<string>();

    public Dictionary<string, DailyTaskAggregateV2> dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
    public Dictionary<string, NumberLocationAggregate> numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();

    public AggregatorSave(AggregatorSaveData _aggregatorSaveData)
    {
        dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();

        foreach (KeyValuePair<string, DailyTaskAggregateV2> dailyTaskLog in _aggregatorSaveData.dailyTaskLogs)
        {
            dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
        }
    }

    public AggregatorSave()
    {
        dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
        numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
    }

    public void setDailyTaskLogs(Dictionary<string, DailyTaskAggregateV2> _dailyTaskLogs)
    {
        dailyTaskLogs = _dailyTaskLogs;
    }

    public void setNumberLocationLogs(Dictionary<string, NumberLocationAggregate> _numberLocationLogs)
    {
        numberLocationLogs = _numberLocationLogs;
    }
}
