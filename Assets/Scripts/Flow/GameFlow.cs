using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
	public static GameFlow instance;

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
        InitializeCore();
	}

	public void InitializeCore()
	{
		// check if core scene is loaded
		if (!SceneManager.GetSceneByName("Core").isLoaded)
		{
            // load core scene
            SceneManager.LoadScene("Core", LoadSceneMode.Additive);
		}
	}
}
