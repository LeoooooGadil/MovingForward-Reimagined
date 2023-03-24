using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu Tabs Manifest", menuName = "Moving Forward/MenuTabs", order = 1)]
public class MovingForwardMenuTabsObject : ScriptableObject
{
    [SerializeField]
    public List<MovingForwardMenuTab> Tabs;
    
    [System.Serializable]

    [IncludeInSettings(true)]
    public class MovingForwardMenuTab
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public GameObject ContentPrefab;
        [SerializeField]
        public Texture2D Icon;
        [SerializeField]
        public Color backgroundColor = new Color(0, 0, 0, 1);
    }
}
