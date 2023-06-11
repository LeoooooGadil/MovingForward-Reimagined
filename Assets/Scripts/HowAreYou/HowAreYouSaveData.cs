using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HowAreYouSaveData
{
	public Dictionary<string, Dictionary<string, HowAreYouItem>> responses = new();

	public HowAreYouSaveData(HowAreYouSave _save)
	{
        responses = new();
        responses = _save.responses;
	}
}
