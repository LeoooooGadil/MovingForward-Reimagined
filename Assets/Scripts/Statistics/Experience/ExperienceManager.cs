using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
	private ExperienceSave experienceSave;
	private string saveFileName = "experience";

	// this method is called when the player gains experience
	// add a check to see if the player experience (experience) is greater than the experience to next level (experienceToNextLevelMultiplier)
	// call SaveExperience() to save the experience
	public void AddExperience(float xp)
	{
		throw new System.NotImplementedException();
	}

	// this method is called when the player levels up
	// increase the level (level) by 1
	// reset the the experience but keep the remainder (experience)
	// call SaveExperience() to save the experience
	public void AdvanceLevel()
	{
		throw new System.NotImplementedException();
	}

	public void CalcExpNeededToLevel()
	{
		throw new System.NotImplementedException();
	}


	public void SaveExperience()
	{
		ExperienceSaveData saveData = new ExperienceSaveData(experienceSave);
		SaveSystem.Save(saveFileName, saveData);
	}
}