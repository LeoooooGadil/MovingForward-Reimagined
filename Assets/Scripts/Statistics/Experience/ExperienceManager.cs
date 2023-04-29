using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
	public static ExperienceManager instance;
	private ExperienceSave experienceSave;
	private string saveFileName = "experience";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		LoadExperience();
	}

	void LoadExperience()
	{
		ExperienceSaveData experienceSaveData = SaveSystem.Load(saveFileName) as ExperienceSaveData;
		
		if (experienceSaveData != null)
			experienceSave = new ExperienceSave(experienceSaveData);
		else
			experienceSave = new ExperienceSave();
	}

	// this method is called when the player gains experience
	// add a check to see if the player experience (experience) is greater than the experience to next level (experienceToNextLevel)
	// call SaveExperience() to save the experience
	// reset the the experience but keep the remainder (experience)
	public void AddExperience(float xp)
	{
		if(experienceSave.GetLevel() >= experienceSave.GetMaxLevel())
			return;

		float exp = experienceSave.GetExperience();
		float expToNextLevel = experienceSave.GetExperienceToNextLevel();

		float newExp = exp + xp;

		if (newExp >= expToNextLevel)
		{
			newExp = newExp - expToNextLevel;
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
		if(experienceSave.GetLevel() >= experienceSave.GetMaxLevel())
			return;

		AudioManager.instance.PlaySFX("LevelUpSfx");
		int level = experienceSave.GetLevel();
		level++;
		experienceSave.SetLevel(level);
		CalcExpNeededToLevel();
		OnScreenNotificationManager.instance.CreateNotification("Level Up! You are now level " + level + "!");

		SaveExperience();
	}

	// this method is called when the player levels up
	// increase the experience to next level (experienceToNextLevelMultiplier) by the experience to next level (experienceToNextLevelMultiplier) times the experience to next level multiplier (experienceToNextLevelMultiplier)
	public void CalcExpNeededToLevel()
	{
		int level = experienceSave.GetLevel();
		float levelMultiplier = experienceSave.GetExperienceToNextLevelMultiplier();

		float newExpToNextLevel = (level * levelMultiplier * 100) * 0.6f;

		experienceSave.SetExperienceToNextLevel(newExpToNextLevel);
	}

	public void SaveExperience()
	{
		ExperienceSaveData saveData = new ExperienceSaveData(experienceSave);
		SaveSystem.Save(saveFileName, saveData);
	}

	public ExperienceSave GetExperienceSave()
	{
		// check if the experienceSave is null
		// if it is null, load the experience
		if (experienceSave == null)
			LoadExperience();

		return experienceSave;
	}
}