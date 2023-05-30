using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
	public static ProfileManager instance;

	ProfileManagerSave profileManagerSave;

	private string saveFileName = "profileManagerSave";

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
	}

	void Start()
	{
		LoadProfile();
	}

	void LoadProfile()
	{
		ProfileManagerSaveData profileManagerSaveData = SaveSystem.Load(saveFileName) as ProfileManagerSaveData;

		if (profileManagerSaveData != null)
		{
			profileManagerSave = new ProfileManagerSave(profileManagerSaveData);
		}
		else
		{
			profileManagerSave = null;
		}
	}

	public bool CheckIfNoPlayer()
	{
		LoadProfile();

		if(profileManagerSave == null) return true;
		else return false;
	}

	public string GetUserName()
	{
		LoadProfile();

		if (profileManagerSave == null) return null;

		return profileManagerSave.username;
	}

	public string GetAge()
	{
		LoadProfile();

		if (profileManagerSave == null) return null;

		return profileManagerSave.age;
	}

	public string GetStatus()
	{
		if (DailyMoodManager.instance == null) return "";

		if (DailyMoodManager.instance.dailyMoodManagerSave == null) return "";

		return DailyMoodManager.instance.GetMood().ToString();
	}

	public float GetMoney()
	{
		LoadProfile();

		if (profileManagerSave == null) return 0;

		return profileManagerSave.money;
	}

	public void AddMoney(float money)
	{
		LoadProfile();

		OnScreenNotificationManager.instance.CreateNotification("+₱" + money.ToString("F0"), OnScreenNotificationType.Sucess);
		profileManagerSave.setMoney(profileManagerSave.money + money);

		SaveProfile();
	}

	public void NegateMoney(float money)
	{
		LoadProfile();

		OnScreenNotificationManager.instance.CreateNotification("-₱" + money.ToString("F0"), OnScreenNotificationType.Warning);
		profileManagerSave.setMoney(profileManagerSave.money - money);

		SaveProfile();
	}

	public void SaveProfile()
	{
		ProfileManagerSaveData profileManagerSaveData = new ProfileManagerSaveData(this.profileManagerSave);
		SaveSystem.Save(saveFileName, profileManagerSaveData);
	}
}
