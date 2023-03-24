using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance;

	private int currentTab = 0;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void OpenMenu(int tabIndex = 0)
	{
		LevelManager.instance.ChangeScene("Menu", false);
        currentTab = tabIndex;
	}

	public void CloseMenu()
	{
		LevelManager.instance.RemoveScene("Menu");
        currentTab = 0;
	}

    public int GetCurrentTab()
    {
        return currentTab;
    }

}
