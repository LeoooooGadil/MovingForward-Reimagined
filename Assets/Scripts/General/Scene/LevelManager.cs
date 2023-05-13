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

	public bool IsTransitioning { get; set; }

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

	public void ChangeScene(string sceneName, bool unloadCurrentScene = true, SceneTransitionMode transitionMode = SceneTransitionMode.None, bool overrideDefaultTransition = true)
	{
		StartCoroutine(LoadScene(sceneName, unloadCurrentScene, transitionMode, overrideDefaultTransition));
	}

	public void RemoveScene(string sceneName)
	{
		StartCoroutine(UnloadScene(sceneName));
	}

	public bool isSceneLoaded(string sceneName)
	{
		return SceneManager.GetSceneByName(sceneName).isLoaded;
	}

	public bool isStatsMenuLoaded()
	{
		return SceneManager.GetSceneByName("Stats Menu").isLoaded;
	}

	public bool isMainMenuLoaded()
	{
		return SceneManager.GetSceneByName("Menu").isLoaded;
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

	IEnumerator LoadScene(string sceneName, bool unloadCurrentScene = true, SceneTransitionMode transitionMode = SceneTransitionMode.None, bool overrideDefaultTransition = true)
	{
		if (sceneToBeLoaded != null) yield break;

		LoadSceneMode sceneMode = unloadCurrentScene ? LoadSceneMode.Single : LoadSceneMode.Additive;

		sceneToBeLoaded = SceneManager.LoadSceneAsync(sceneName, sceneMode);
		sceneToBeLoaded.allowSceneActivation = false;

		if (unloadCurrentScene && !overrideDefaultTransition) transitionMode = SceneTransitionMode.Slide;

		IsTransitioning = true;

		if (transitionMode == SceneTransitionMode.None)
		{
			try
			{
				while (!sceneToBeLoaded.isDone)
				{
					if (sceneToBeLoaded.progress >= 0.9f)
					{
						sceneToBeLoaded.allowSceneActivation = true;
						sceneToBeLoaded = null;
						IsTransitioning = false;
					}
				}
			}
			catch (Exception)
			{
				// ignored null reference exception
			}
		}
		else
		{
			LevelTransition transition = levelTransitions.Find(x => x.transitionMode == transitionMode);

			if (transition != null)
			{

				transitionCanvas.enabled = true;
				ActiveTransition = transition.AnimationSO;
				AudioManager.instance.PlaySFX("WhooshSfx", 0.5f);
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
		if (sceneToBeLoaded != null)
			sceneToBeLoaded.allowSceneActivation = true;
		yield return new WaitForSeconds(0.1f);
	}

	private IEnumerator Enter()
	{
		AudioManager.instance.PlaySFX("WhooshSfx", 0.5f);
		yield return new WaitForSeconds(0.2f);
		yield return StartCoroutine(ActiveTransition.Enter(transitionCanvas));
		yield return new WaitForSeconds(0.1f);
		transitionCanvas.enabled = false;
		sceneToBeLoaded = null;
		ActiveTransition = null;
		IsTransitioning = false;
	}
}
