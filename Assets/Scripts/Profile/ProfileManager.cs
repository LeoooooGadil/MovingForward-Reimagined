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
			profileManagerSave = new ProfileManagerSave();
		}
	}

	public string GetUserName()
	{
		return profileManagerSave.username;
	}

	public string GetStatus()
	{
        return "Depressed";
	}
}
