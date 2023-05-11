using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial Sequence", menuName = "Moving Forward/Tutorials/Sequence")]
public class MovingForwardTutorialSequenceScriptableObject : ScriptableObject
{
    [SerializeField]
    public string SequenceName;
    public bool GoToNextPhase = false;
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
    public string[] HighlightObject;
    public bool disableBackdrop = false;
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
