using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceSave
{
    public int level = 1;
    public float experience = 0;
    public float experienceToNextLevel = 100;
    private float experienceToNextLevelMultiplier = 1.5f;

    private int maxLevel = 999; 

    public ExperienceSave(ExperienceSaveData _experienceSaveData)
    {
        level = _experienceSaveData.level;
        experience = _experienceSaveData.experience;
        experienceToNextLevel = _experienceSaveData.experienceToNextLevel;
    }

    public ExperienceSave()
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public void SetLevel(int _level)
    {
        level = _level;
    }

    public void SetExperience(float _experience)
    {
        experience = _experience;
    }

    public void SetExperienceToNextLevel(float _experienceToNextLevel)
    {
        experienceToNextLevel = _experienceToNextLevel;
    }

    public void SetExperienceToNextLevelMultiplier(float _experienceToNextLevelMultiplier)
    {
        experienceToNextLevelMultiplier = _experienceToNextLevelMultiplier;
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetExperience()
    {
        return experience;
    }

    public float GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public float GetExperienceToNextLevelMultiplier()
    {
        return experienceToNextLevelMultiplier;
    }

    public ExperienceSaveData GetExperienceSaveData()
    {
        return new ExperienceSaveData(this);
    }
}
