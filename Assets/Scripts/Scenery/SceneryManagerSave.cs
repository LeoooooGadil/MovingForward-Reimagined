using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryManagerSave
{
    public int currentSceneryIndex = 0;
    public int previousSceneryIndex = 0;

    public SceneryManagerSave(SceneryManagerSaveData _sceneryManagerSaveData)
    {
        currentSceneryIndex = _sceneryManagerSaveData.currentSceneryIndex;
        previousSceneryIndex = _sceneryManagerSaveData.previousSceneryIndex;
    }

    public SceneryManagerSave()
    {
        currentSceneryIndex = 0;
        previousSceneryIndex = 0;
    }

    public void SetScenery(int index)
    {
        previousSceneryIndex = currentSceneryIndex;
        currentSceneryIndex = index;
    }
}
