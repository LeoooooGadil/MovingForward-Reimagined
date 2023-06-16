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
				responses[todayString][item.timestamp.ToString()] = item;
			}
			else
			{
				responses[todayString].Add(item.timestamp.ToString(), item);
			}
		}
		else
		{
            Dictionary<string, HowAreYouItem> howAreYouDictionary = new Dictionary<string, HowAreYouItem>();
            howAreYouDictionary.Add(item.response.ToString(), item);
            responses.Add(todayString, howAreYouDictionary);
		}
	}

	public List<HowAreYouItem> AllResponses()
	{
		List<HowAreYouItem> howAreYouList = new List<HowAreYouItem>();

		foreach (KeyValuePair<string, Dictionary<string, HowAreYouItem>> mood in responses)
		{
			foreach (KeyValuePair<string, HowAreYouItem> item in mood.Value)
			{
				howAreYouList.Add(item.Value);
			}
		}

		return howAreYouList;
	}
}

[System.Serializable]
public class HowAreYouItem
{
	public HowAreYouResponse response;
	public long timestamp;

	public HowAreYouItem(HowAreYouResponse _response)
	{
		response = _response;
		timestamp = TimeStamp.GetTimeStamp();
	}
}

[System.Serializable]	
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
