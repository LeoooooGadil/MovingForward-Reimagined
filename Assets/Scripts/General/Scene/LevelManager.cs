using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance;
	public List<LevelTransition> levelTransitions = new();

	private Canvas transitionCanvas;
	private AsyncOperation sceneToBeLoaded;
	private MovingForwardAbstractSceneTransitionScriptableObject ActiveTransition;

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

		SceneManager.sceneLoaded += HandleSceneChange;
		transitionCanvas = GetComponentInChildren<Canvas>();
		transitionCanvas.enabled = false;
	}

	private void HandleSceneChange(Scene arg0, LoadSceneMode arg1)
	{
		if (ActiveTransition != null)
		{
			StartCoroutine(Enter());
		}
	}

	public void ChangeScene(string sceneName, bool unloadCurrentScene = true, SceneTransitionMode transitionMode = SceneTransitionMode.None)
	{
		StartCoroutine(LoadScene(sceneName, unloadCurrentScene, transitionMode));
	}

	public void RemoveScene(string sceneName)
	{
		StartCoroutine(UnloadScene(sceneName));
	}

	IEnumerator UnloadScene(string sceneName)
	{
		var sceneToBeUnloaded = SceneManager.GetSceneByName(sceneName);
		var sceneToBeUnloadedAsync = SceneManager.UnloadSceneAsync(sceneToBeUnloaded);

		while (!sceneToBeUnloadedAsync.isDone)
		{
			yield return null;
		}
	}

	IEnumerator LoadScene(string sceneName, bool unloadCurrentScene = true, SceneTransitionMode transitionMode = SceneTransitionMode.None)
	{
		LoadSceneMode sceneMode = unloadCurrentScene ? LoadSceneMode.Single : LoadSceneMode.Additive;

		sceneToBeLoaded = SceneManager.LoadSceneAsync(sceneName, sceneMode);

		if(unloadCurrentScene) transitionMode = SceneTransitionMode.Slide;

		sceneToBeLoaded.allowSceneActivation = false;

		if (transitionMode == SceneTransitionMode.None)
		{
			while (!sceneToBeLoaded.isDone)
			{
				if (sceneToBeLoaded.progress >= 0.9f)
				{
					sceneToBeLoaded.allowSceneActivation = true;

				}

				yield return null;
			}

			GameFlow.instance.InitializeCore();
		}
		else
		{
			LevelTransition transition = levelTransitions.Find(x => x.transitionMode == transitionMode);

			if (transition != null)
			{
				transitionCanvas.enabled = true;
				ActiveTransition = transition.AnimationSO;
				StartCoroutine(Exit());
			}
			else
			{
				Debug.Log("No transition found for " + transitionMode);
			}
		}

		yield return null;
	}

	private IEnumerator Exit()
	{
		yield return StartCoroutine(ActiveTransition.Exit(transitionCanvas));
		sceneToBeLoaded.allowSceneActivation = true;
		yield return new WaitForSeconds(0.5f);
		GameFlow.instance.InitializeCore();
	}

	private IEnumerator Enter()
	{
		yield return StartCoroutine(ActiveTransition.Enter(transitionCanvas));
		transitionCanvas.enabled = false;
		sceneToBeLoaded = null;
		ActiveTransition = null;
	}
}
