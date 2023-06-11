using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowAreYouManager : MonoBehaviour
{
	public static HowAreYouManager instance;

	public bool isHowAreYouOpen = false;

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

	public void OpenHowAreYouForm()
	{
		LevelManager.instance.ChangeScene("How Are You", false);
		isHowAreYouOpen = true;
	}

	public void CloseHowAreYouForm()
	{
        if(isHowAreYouOpen == false) return;

        LevelManager.instance.RemoveScene("How Are You");
        isHowAreYouOpen = false;
	}
}
