using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
	public static ExperienceManager instance;
	private ExperienceSave experienceSave;
	private string saveFileName = "experience";

	void Awake() {
		if (instance != null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// this method is called when the player gains experience
	// add a check to see if the player experience (experience) is greater than the experience to next level (experienceToNextLevel)
	// call SaveExperience() to save the experience
	// reset the the experience but keep the remainder (experience)
	public void AddExperience(float xp)
	{
		float exp = experienceSave.GetExperience();
		float expToNextLevel = experienceSave.GetExperienceToNextLevel();

		float newExp = exp + xp;

		if (newExp >= expToNextLevel)
		{
			newExp = expToNextLevel - newExp;
			AdvanceLevel();
			experienceSave.SetExperience(newExp);
		}
		else
			experienceSave.SetExperience(newExp);

		SaveExperience();
	}

	// this method is called when the player levels up
	// increase the level (level) by 1
	// call SaveExperience() to save the experience
	public void AdvanceLevel()
	{
		int level = experienceSave.GetLevel();
		level++;
		experienceSave.SetLevel(level);
		CalcExpNeededToLevel();

		SaveExperience();
	}

	// this method is called when the player levels up
	// increase the experience to next level (experienceToNextLevelMultiplier) by the experience to next level (experienceToNextLevelMultiplier) times the experience to next level multiplier (experienceToNextLevelMultiplier)
	// call SaveExperience() to save the experience
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