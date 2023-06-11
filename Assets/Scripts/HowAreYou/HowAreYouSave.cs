using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowAreYouSave
{
	public Dictionary<string, Dictionary<string, HowAreYouItem>> responses = new();

	public HowAreYouSave()
	{
		responses = new();
	}

	public HowAreYouSave(HowAreYouSaveData _saveData)
	{
		responses = new();
		responses = _saveData.responses;
	}

	public void AddAResponse(HowAreYouItem item)
	{
		string todayString = DateTime.Today.ToString("dd/MM/yyyy");

		if (responses.ContainsKey(todayString))
		{
			if (responses[todayString].ContainsKey(item.response.ToString()))
			{
				responses[todayString][item.response.ToString()] = item;
			}
			else
			{
				responses[todayString].Add(item.response.ToString(), item);
			}
		}
		else
		{
            Dictionary<string, HowAreYouItem> howAreYouDictionary = new Dictionary<string, HowAreYouItem>();
            howAreYouDictionary.Add(item.response.ToString(), item);
            responses.Add(todayString, howAreYouDictionary);
		}
	}
}

public class HowAreYouItem
{
	public HowAreYouResponse response;
	public long timestamp;
}

public enum HowAreYouResponse
{
	Great,
	VeryGood,
	Good,
	Okay,
	NotGood,
	Bad,
	Awful
}
