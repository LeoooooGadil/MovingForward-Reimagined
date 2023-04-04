using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryManager : MonoBehaviour
{
	public static SceneryManager instance;

	public MovingForwardSceneryObject sceneryObject;
	int currentSceneryIndex = 0;
	int previousSceneryIndex = 0;
	public Scene currentScenery;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		UpdateScenery();
	}

	void UpdateScenery()
	{
		var scene = SceneManager.LoadSceneAsync(sceneryObject.sceneryList[currentSceneryIndex].sceneName, LoadSceneMode.Additive);
		scene.completed += (AsyncOperation op) =>
		{	
			Scene _currentScenery = SceneManager.GetSceneByName(sceneryObject.sceneryList[currentSceneryIndex].sceneName);
			Scene previousScenery = SceneManager.GetSceneByName(sceneryObject.sceneryList[previousSceneryIndex].sceneName);

			SceneManager.SetActiveScene(_currentScenery);

			if (previousSceneryIndex != currentSceneryIndex)
			{
				SceneManager.UnloadSceneAsync(previousScenery);
			}

			SceneManager.SetActiveScene(_currentScenery);

			currentScenery = _currentScenery;
		};
	}

	public void SetScenery(int index)
	{
		previousSceneryIndex = currentSceneryIndex;
		currentSceneryIndex = index;	
		UpdateScenery();
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
}
