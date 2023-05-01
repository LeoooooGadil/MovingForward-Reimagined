using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTabsManager : MonoBehaviour
{
	public List<MenuTabItem> menuTabs = new List<MenuTabItem>();
    public List<GameObject> menuTabContents = new List<GameObject>();
	public int currentTab = 0;

	// Start is called before the first frame update
	void Start()
	{
        if(MenuManager.Instance)
        {
            currentTab = MenuManager.Instance.GetCurrentTab();
        }

        UpdateTabs();
        UpdateTabContents();
	}

	void UpdateTabs()
	{
        for (int i = 0; i < menuTabs.Count; i++)
        {
            if (i == currentTab)
            {
                menuTabs[i].isEnabled = true;
            }
            else
            {
                menuTabs[i].isEnabled = false;
            }
        }
	}

    void UpdateTabContents()
    {
        for (int i = 0; i < menuTabContents.Count; i++)
        {
            if (i == currentTab)
            {
                menuTabContents[i].SetActive(true);
            }
            else
            {
                menuTabContents[i].SetActive(false);
            }
        }
    }

    public void SetCurrentTab(string tabName)
    {
        for (int i = 0; i < menuTabs.Count; i++)
        {
            if (menuTabs[i].tabName == tabName)
            {
                currentTab = i;
                UpdateTabs();
                UpdateTabContents();
                return;
            }
        }
    }
}
