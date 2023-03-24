using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTabManager : MonoBehaviour
{
	public static MenuTabManager Instance;

	public MovingForwardMenuTabsObject MenuTabs;
	public GameObject MenuTabPrefab;
	public GameObject MenuContentContainer;
	public GameObject MenuContentContainerBackground;
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

    void Start()
    {
        // if MenuManager.instance is not null, then get the current tab from the MenuManager
        if (MenuManager.Instance != null)
        {
            currentTab = MenuManager.Instance.GetCurrentTab();
        }
        createTabs();
        setTabActive(currentTab);
    }

	void createTabs()
	{

		// Create the tabs as a child of this object
		for (int i = 0; i < MenuTabs.Tabs.Count; i++)
		{
			MovingForwardMenuTabsObject.MovingForwardMenuTab tab = MenuTabs.Tabs[i];

			GameObject menuTab = Instantiate(MenuTabPrefab, this.transform);
			MenuTab menuTabScript = menuTab.GetComponent<MenuTab>();
			menuTabScript.tabName = tab.name;
			menuTabScript.tabIndex = i;
			menuTabScript.tabIcon = tab.Icon;
		}

		// Force the layout to update
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
	}

	public void setTabActive(int tabIndex)
	{
		currentTab = tabIndex;
		updateTabs();
		updateTabContent();
	}

	void updateTabs()
	{
		for (int i = 0; i < this.transform.childCount; i++)
		{
			MenuTab menuTab = this.transform.GetChild(i).GetComponent<MenuTab>();

			if (menuTab.tabIndex == currentTab)
			{
				menuTab.SetTabActive();
			}
			else
			{
				menuTab.SetTabInactive();
			}
		}
	}

	void updateTabContent()
	{
		// Destroy the current content
		for (int i = 0; i < MenuContentContainer.transform.childCount; i++)
		{
			GameObject oldContent = MenuContentContainer.transform.GetChild(i).gameObject;
			Destroy(oldContent);
		}

		GameObject tabContent = MenuTabs.Tabs[currentTab].ContentPrefab;
		Color backgroundColor = MenuTabs.Tabs[currentTab].backgroundColor;
		MenuContentContainerBackground.GetComponent<Image>().color = backgroundColor;
		// instantiate the new content as a child of the MenuContentContainer object and make it so that the size of the content is the same as the container
        GameObject newContent = Instantiate(tabContent, MenuContentContainer.transform);
	}
}
