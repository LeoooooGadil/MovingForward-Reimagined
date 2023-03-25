using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExperienceSaveData
{
    public int level;
    public float experience;
    public float experienceToNextLevel;

    public ExperienceSaveData(ExperienceSave _experienceSave)
    {
        level = _experienceSave.level;
        experience = _experienceSave.experience;
        experienceToNextLevel = _experienceSave.experienceToNextLevel;
    }
}
