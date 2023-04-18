using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LifeCycleRepeatType
{
    None,
    Daily,
    Weekly,
    Monthly,
    Yearly,
    Custom
}

[System.Serializable]
public class LifeCycleItem 
{
    public string name;
    public DateTime startTime;
    public bool Envoke;
    public bool isRepeatable;
    public LifeCycleRepeatType repeatType;
    public int customRepeatTime; // in seconds
    public int repeatCount;
    public int maxRepeatCount;
}

public class LifeCycleSave
{
    public Dictionary<string, LifeCycleItem> lifeCycleItems = new Dictionary<string, LifeCycleItem>();

    public LifeCycleSave(LifeCycleSaveData _lifeCycleSaveData)
    {
        lifeCycleItems = _lifeCycleSaveData.lifeCycleItems;
    }

    public LifeCycleSave()
    {
        lifeCycleItems = new Dictionary<string, LifeCycleItem>();
    }

    public Dictionary<string, LifeCycleItem> GetLifeCycleItems()
    {
        return lifeCycleItems;
    }
}
