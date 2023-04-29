using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlocker : MonoBehaviour

{
	public static ScreenBlocker instance;
	public Canvas canvas;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if (LevelManager.instance == null) return;

		bool isTransitioning = LevelManager.instance.IsTransitioning;

		if (isTransitioning)
		{
			BlockScreen();
		}
		else
		{
			UnblockScreen();
		}
	}

	public void BlockScreen()
	{
		canvas.enabled = true;
	}

	public void UnblockScreen()
	{
		canvas.enabled = false;
	}
}
