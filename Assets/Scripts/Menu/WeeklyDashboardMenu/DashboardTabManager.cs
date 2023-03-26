using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardTabManager : MonoBehaviour
{
    public MovingForwardWeeklyDashboardObject dashboardTabs;

	public GameObject dashboardTabPrefab;
    public Transform dashboardContentContainer;
	private int activeTab = 0;

	void Start()
	{
		CreateTab();
		UpdateTabs();
        UpdateContent();
	}

	void CreateTab()
	{
		for (int i = 0; i < dashboardTabs.Tabs.Count; i++)
		{
            MovingForwardWeeklyDashboardObject.DashboardTab tab = dashboardTabs.Tabs[i];

			GameObject newTab = Instantiate(dashboardTabPrefab, transform);
			DashboardTab dashboardTab = newTab.GetComponent<DashboardTab>();
			dashboardTab.tabName = tab.name;
			dashboardTab.dashboardTabManager = this;
			dashboardTab.tabID = i;
			dashboardTab.isActive = (i == activeTab);
		}
	}

	void UpdateTabs()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			DashboardTab dashboardTab = transform.GetChild(i).GetComponent<DashboardTab>();
			dashboardTab.SetActive(dashboardTab.tabID == activeTab);
		}
	}

    void UpdateContent() {
        GameObject content = dashboardTabs.Tabs[activeTab].content;

        // Destroy all the children of the content container
        for (int i = 0; i < dashboardContentContainer.childCount; i++)
        {
            Destroy(dashboardContentContainer.GetChild(i).gameObject);
        }

        GameObject newContent = Instantiate(content, dashboardContentContainer);
    }

	public void SetActiveTab(int index)
	{
		activeTab = index;
		UpdateTabs();
        UpdateContent();
	}
}
