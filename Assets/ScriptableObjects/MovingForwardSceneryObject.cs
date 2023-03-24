using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Sceneries Manifest", menuName = "Moving Forward/Scenery", order = 1)]
public class MovingForwardSceneryObject : ScriptableObject
{
    [SerializeField]
    public List<MovingForwardScenery> sceneryList;

    [System.Serializable]

    [IncludeInSettings(true)]
    public class MovingForwardScenery
    {
        [SerializeField]
        public string label;

        [SerializeField]
        public string sceneName;
        
        [SerializeField]
        public Texture2D thumbnail;
    }
}
