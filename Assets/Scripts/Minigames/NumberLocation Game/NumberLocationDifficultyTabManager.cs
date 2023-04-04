using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLocationDifficultyTabManager : MonoBehaviour
{
	public NumberLocationDifficultyTab[] tabs;
    public int currentTab = 0;

    public NumberLocationUI numberLocationUI;

	void Start()
	{
        UpdateTabs();
	}

	void UpdateTabs()
	{
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == currentTab)
            {
                tabs[i].isActive = true;
            }
            else
            {
                tabs[i].isActive = false;
            }
        }
	}

    public void SetTab(int tab)
    {
        currentTab = tab;
        UpdateTabs();
        numberLocationUI.SetDifficulty((NumberLocationDifficulty.Difficulty)tab);
    }
}
