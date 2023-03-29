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
		float minExperience = 0;
		float maxExperience = maxExperienceNeededToLevelUp;
		float currentValue = currentExperience;

		float experienceNormalized = ConvertExperienceToNormalized(minExperience, maxExperienceNeededToLevelUp, currentExperience);
		ExperienceBar.fillAmount = experienceNormalized;
	}

	// This method is to convert the current value of experience to a normalized value between 0 and 1
	// This is to be used with the SetExperienceBar method
	// The min and max values are the minimum and maximum values of the experience bar
    // return the normalized value of the experience
	float ConvertExperienceToNormalized(float min, float max, float experience)
	{
		float range = max - min;
		float adjustedExperience = experience - min;
		float experienceNormalized = adjustedExperience / range;
		return Mathf.Round(experienceNormalized * 100f) / 100f; // Rounds the value to 2 decimal places
	}
}
