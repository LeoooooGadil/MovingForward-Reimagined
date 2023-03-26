using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weekly Dashboard Tabs Manifest", menuName = "Moving Forward/Weekly Tabs", order = 1)]
public class MovingForwardWeeklyDashboardObject : ScriptableObject
{
    [SerializeField]
    public List<DashboardTab> Tabs = new List<DashboardTab>();

    [System.Serializable]
    
    [IncludeInSettings(true)]
    public class DashboardTab
    {
        [SerializeField]
        public string name;

        [SerializeField]
        public GameObject content;
    }
}
