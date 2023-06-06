using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Daily Chores Manifest", menuName = "Moving Forward/Daily Chores", order = 1)]
public class MovingForwardDailyChoresScriptableObject : ScriptableObject
{
	[SerializeField]
	public List<Chores> chores;
}

[System.Serializable]
[IncludeInSettings(true)]
public class Chores
{
	[SerializeField]
	public string name;

    [SerializeField]
    public DailyChoreRoom room;

	[SerializeField]
	public DailyChoreType type;

	[SerializeField]
	public string sceneName;
	public bool isMandatory;
	[SerializeField]
	public int minScore;
}

[System.Serializable]
public enum DailyChoreType
{
	DustMeOff,
	ThrowMeOut,
	Wordle,
	NumberPlacement,
	JournalEntry,
	Breathe,
	Exercise,
}

[System.Serializable]
public enum DailyChoreRoom
{
    None,
    LivingRoom,
    Bedroom,
    Out,
	Kitchen,
}
