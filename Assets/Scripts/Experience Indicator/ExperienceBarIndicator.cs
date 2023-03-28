using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarIndicator : MonoBehaviour
{
	public Image ExperienceBar;

	private float experienceNormalized;

	void Update()
	{
        ExperienceSave experienceSave = ExperienceManager.instance.GetExperienceSave();
		float maxExperienceNeededToLevelUp = experienceSave.GetExperienceToNextLevel();
		float currentExperience = experienceSave.GetExperience();

        SetExperienceBar(maxExperienceNeededToLevelUp, currentExperience);
	}

	public void SetExperienceBar(float maxExperienceNeededToLevelUp, float currentExperience)
	{
		float experienceNormalized = ConvertExperienceToNormalized(0, maxExperienceNeededToLevelUp, currentExperience);
		ExperienceBar.fillAmount = experienceNormalized;
	}

	// This method is to convert the current value of experience to a normalized value between 0 and 1
	// This is to be used with the SetExperienceBar method
	// The min and max values are the minimum and maximum values of the experience bar
    // return the normalized value of the experience
	float ConvertExperienceToNormalized(float min, float max, float experience)
	{
        throw new System.NotImplementedException();
	}
}
