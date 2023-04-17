using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryManager : MonoBehaviour
{
	public static SceneryManager instance;

	public MovingForwardSceneryObject sceneryObject;
	public SceneryManagerSave sceneryManagerSave;
	public Scene currentScenery;

	private string saveFileName = "SceneryManager";

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		LoadSceneryManager();
		UpdateScenery();
	}

	void LoadSceneryManager()
	{
		SceneryManagerSaveData sceneryManagerSaveData = SaveSystem.Load(saveFileName) as SceneryManagerSaveData;

		if (sceneryManagerSaveData != null)
		{
			sceneryManagerSave = new SceneryManagerSave(sceneryManagerSaveData);
		}
		else
		{
			sceneryManagerSave = new SceneryManagerSave();
		}
	}

	void UpdateScenery()
	{
		int startTime = System.DateTime.Now.Millisecond;
		var scene = SceneManager.LoadSceneAsync(sceneryObject.sceneryList[sceneryManagerSave.currentSceneryIndex].sceneName, LoadSceneMode.Additive);
		scene.completed += (AsyncOperation op) =>
		{
			Scene _currentScenery = SceneManager.GetSceneByName(sceneryObject.sceneryList[sceneryManagerSave.currentSceneryIndex].sceneName);
			Scene previousScenery = SceneManager.GetSceneByName(sceneryObject.sceneryList[sceneryManagerSave.previousSceneryIndex].sceneName);

			SceneManager.SetActiveScene(_currentScenery);

			if (sceneryManagerSave.previousSceneryIndex != sceneryManagerSave.currentSceneryIndex)
			{
				// check if the previous scenery is loaded
				if (previousScenery.isLoaded)
				{
					// unload the previous scenery
					SceneManager.UnloadSceneAsync(previousScenery);
				}
			}

			SceneManager.SetActiveScene(_currentScenery);

			currentScenery = _currentScenery;
			int endTime = System.DateTime.Now.Millisecond;
			Debug.Log("Time to load scenery: " + (endTime - startTime) + "ms");
		};
	}

	public void SetScenery(int index)
	{
		sceneryManagerSave.SetScenery(index);
		UpdateScenery();
		SaveSceneryManager();
	}

	public void SetScenery(string sceneName)
	{
		for (int i = 0; i < sceneryObject.sceneryList.Count; i++)
		{
			if (sceneryObject.sceneryList[i].sceneName == sceneName)
			{
				SetScenery(i);
				return;
			}
		}
	}

	void SaveSceneryManager()
	{
		SceneryManagerSaveData sceneryManagerSaveData = new SceneryManagerSaveData(sceneryManagerSave);
		SaveSystem.Save(saveFileName, sceneryManagerSaveData);
	}
}
