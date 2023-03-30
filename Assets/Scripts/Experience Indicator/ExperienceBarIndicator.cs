using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarIndicator : MonoBehaviour
{
	public Image ExperienceBar;
	public Text ExperienceText;

	private float experienceNormalized = 0;

	void Update()
	{
		// Get the experience save data from the ExperienceManager singleton class
		ExperienceSave experienceSave = ExperienceManager.instance.GetExperienceSave();

		float maxExperienceNeededToLevelUp = experienceSave.GetExperienceToNextLevel();
		float currentExperience = experienceSave.GetExperience();

		SetExperienceBar(maxExperienceNeededToLevelUp, currentExperience);
		SetExperienceText(currentExperience);
	}

	public void SetExperienceText(float currentExperience)
	{
		// lerp the text to the current experience and remove the decimal places
		ExperienceText.text = Mathf.Lerp(float.Parse(ExperienceText.text), currentExperience, Time.deltaTime * 5).ToString("F0");
		
	}

	public void SetExperienceBar(float maxExperienceNeededToLevelUp, float currentExperience)
	{
		float minExperience = 0;
		float maxExperience = maxExperienceNeededToLevelUp;
		float currentValue = currentExperience;

		float experienceNormalized = ConvertExperienceToNormalized(minExperience, maxExperienceNeededToLevelUp, currentExperience);
		ExperienceBar.fillAmount = Mathf.Lerp(ExperienceBar.fillAmount, experienceNormalized, Time.deltaTime * 8);
	}
		

	// This method is to convert the current value of experience to a normalized value between 0 and 1
	// This is to be used with the SetExperienceBar method
	// The min and max values are the minimum and maximum values of the experience bar
    // return the normalized value of the experience
	float ConvertExperienceToNormalized(float min, float max, float experience)
	{
		float fromMin = 0;
		float fromMax = max;
		float toMin = 0;
		float toMax = 1;

		float experienceNormalized = (experience - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;

		return Mathf.Round(experienceNormalized * 100) / 100;
	}
}
