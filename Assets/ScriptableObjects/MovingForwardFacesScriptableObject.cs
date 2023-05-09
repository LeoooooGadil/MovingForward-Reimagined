using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Faces Manifest", menuName = "Moving Forward/Tutorials/Faces Manifest")]
public class MovingForwardFacesScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<Faces> Faces;
}

[System.Serializable]
[IncludeInSettings(true)]
public class Faces
{
    public SequenceMood name;
    public Sprite sprite;
}
