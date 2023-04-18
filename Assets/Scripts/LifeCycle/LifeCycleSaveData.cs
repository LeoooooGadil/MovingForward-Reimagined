using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LifeCycleSaveData
{
    public Dictionary<string, LifeCycleItem> lifeCycleItems;

    public LifeCycleSaveData()
    {
        lifeCycleItems = new Dictionary<string, LifeCycleItem>();
    }

    public LifeCycleSaveData(LifeCycleSave _lifeCycleSave)
    {
        lifeCycleItems = _lifeCycleSave.lifeCycleItems;
    }
}
