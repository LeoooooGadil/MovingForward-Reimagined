using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance;

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

    public void ChangeScene(string sceneName, bool unloadCurrentScene = true)
    {
        StartCoroutine(LoadScene(sceneName, unloadCurrentScene));
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

    IEnumerator LoadScene(string sceneName, bool unloadCurrentScene = true)
    {
        var sceneMode = unloadCurrentScene ? LoadSceneMode.Single : LoadSceneMode.Additive;
        var sceneToBeLoaded = SceneManager.LoadSceneAsync(sceneName, sceneMode);
        sceneToBeLoaded.allowSceneActivation = false;

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
}
