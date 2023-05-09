using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Sequence", menuName = "Moving Forward/Tutorials/Sequence")]
public class MovingForwardTutorialSequenceScriptableObject : ScriptableObject
{
    [SerializeField]
    public List<Sequence> Sequences;
}

[System.Serializable]
[IncludeInSettings(true)]
public class Sequence
{
    public string Title;
	public string Text;
	public SequencePosition Position;
    public SequenceMood Mood;
    public GameObject[] HighlightObject;
}

[System.Serializable]
public enum SequencePosition
{
	Top,
	Middle,
	Bottom,
}

[System.Serializable]
public enum SequenceMood
{
    Happy,
    Smile,
    Smirk,
    Surprised,
    Uhh,
    Annoyed,
    Angry
}
