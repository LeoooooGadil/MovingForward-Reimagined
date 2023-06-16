using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopUps
{
	public PopUpType Type;
	public GameObject PopUpPrefab;
}

public enum PopUpType
{
	None,
	WordleHint,
	DailyMoodTracker,
	Tutorial,
	Disclaimer,
	MoodTrackerList
}
