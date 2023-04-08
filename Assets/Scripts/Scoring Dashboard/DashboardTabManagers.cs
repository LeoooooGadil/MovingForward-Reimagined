using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardTabManagers : MonoBehaviour
{
    public DashboardTab[] tabs;
    public GameObject[] tabContents;

    public int selectedTab = 0;

    void Start() {
        SelectTab(tabs[selectedTab]);
        UpdateTabContents();
    }

    public void SelectTab(DashboardTab tab) {
        selectedTab = tab.transform.GetSiblingIndex();
        foreach (DashboardTab t in tabs) {
            t.isSelected = false;
        }
        tab.isSelected = true;
        UpdateTabContents();
    }

    void UpdateTabContents() {
        for (int i = 0; i < tabContents.Length; i++) {
            if (i == selectedTab) {
                tabContents[i].SetActive(true);
            } else {
                tabContents[i].SetActive(false);
            }
        }
    }
}
