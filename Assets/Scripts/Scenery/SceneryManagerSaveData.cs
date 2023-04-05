using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneryManagerSaveData
{
    public int currentSceneryIndex;
    public int previousSceneryIndex;

    public SceneryManagerSaveData(SceneryManagerSave _sceneryManagerSave)
    {
        currentSceneryIndex = _sceneryManagerSave.currentSceneryIndex;
        previousSceneryIndex = _sceneryManagerSave.previousSceneryIndex;
    }
}
