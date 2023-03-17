using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryManager : MonoBehaviour
{
	public static SceneryManager instance;

	public string[] sceneryList;
	int currentSceneryIndex = 0;
	public Scene currentScenery;

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
		UpdateScenery();
	}

	void UpdateScenery()
	{
		Scene currentScene = SceneManager.GetActiveScene();

		var scene = SceneManager.LoadSceneAsync(sceneryList[currentSceneryIndex], LoadSceneMode.Additive);
		scene.completed += (AsyncOperation op) =>
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneryList[currentSceneryIndex]));

			if (currentScenery.name != null)
				SceneManager.UnloadSceneAsync(currentScenery);

			currentScenery = SceneManager.GetSceneByName(sceneryList[currentSceneryIndex]);

			SceneManager.SetActiveScene(currentScene);
		};
	}
}
