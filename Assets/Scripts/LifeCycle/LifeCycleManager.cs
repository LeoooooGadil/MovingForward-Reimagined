using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleManager : MonoBehaviour
{
	public static LifeCycleManager instance;
	LifeCycleSave lifeCycleSave;
	public int checkInterval = 0;

	float timer = 0;

	string savefilename = "LifeCycleSaveData";

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		LoadLifeCycle();
	}

	void Start()
	{
		timer = checkInterval;
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= checkInterval)
		{
			CheckLifeCycle();
			timer = 0;
		}
	}

	void CheckLifeCycle()
	{
		if (lifeCycleSave.lifeCycleItems.Count == 0) return;

		Debug.Log("Checking Life Cycle");

		// convert dictionary to list
		List<LifeCycleItem> _lifeCycleItems = new List<LifeCycleItem>();
		foreach (var item in lifeCycleSave.lifeCycleItems)
		{
			_lifeCycleItems.Add(item.Value);
		}

		// check each item
		for (int i = 0; i < _lifeCycleItems.Count; i++)
		{
			_lifeCycleItems[i] = CheckLifeCycle(_lifeCycleItems[i]);
		}

		SaveLifeCycle();
	}

	LifeCycleItem CheckLifeCycle(LifeCycleItem lifeCycleItem)
	{
		if (lifeCycleItem.startTime < DateTime.Now && !lifeCycleItem.Envoke)
		{
			Debug.Log("Life Cycle Item: " + lifeCycleItem.name + " Envoked");
			lifeCycleItem.Envoke = true;

			if (lifeCycleItem.isRepeatable)
			{
				switch (lifeCycleItem.repeatType)
				{
					case LifeCycleRepeatType.Daily:
						lifeCycleItem.startTime = lifeCycleItem.startTime.AddDays(1);
						break;
					case LifeCycleRepeatType.Weekly:
						lifeCycleItem.startTime = lifeCycleItem.startTime.AddDays(7);
						break;
					case LifeCycleRepeatType.Monthly:
						lifeCycleItem.startTime = lifeCycleItem.startTime.AddMonths(1);
						break;
					case LifeCycleRepeatType.Yearly:
						lifeCycleItem.startTime = lifeCycleItem.startTime.AddYears(1);
						break;
					case LifeCycleRepeatType.Custom:
						lifeCycleItem.startTime = lifeCycleItem.startTime.AddSeconds(lifeCycleItem.customRepeatTime);
						break;
					default:
						break;
				}

				lifeCycleItem.repeatCount++;
				if (lifeCycleItem.maxRepeatCount != -1 && lifeCycleItem.repeatCount >= lifeCycleItem.maxRepeatCount)
				{
					lifeCycleItem.isRepeatable = false;
				}
			}
		}

		// if the item is not repeatable and has been envoked, remove it from the list
		if (!lifeCycleItem.isRepeatable && lifeCycleItem.Envoke)
		{
			// remove the item from the list
			RemoveLifeCycleItem(lifeCycleItem);
		}

		return lifeCycleItem;
	}

	// a function that bypass the timer by making them envoked or removed if they are not repeatable
	public void BypassLifeCycleItem(string lifeCycleItem)
	{
		if (lifeCycleSave.lifeCycleItems.ContainsKey(lifeCycleItem))
		{
			LifeCycleItem item = lifeCycleSave.lifeCycleItems[lifeCycleItem];
			item.Envoke = true;
			RemoveLifeCycleItem(item);
			SaveLifeCycle();
			Debug.Log("Life Cycle Item: " + lifeCycleItem + " envoked");
		}
		else
		{
			Debug.Log("Life Cycle Item: " + lifeCycleItem + " does not exist");
		}
	}

	void LoadLifeCycle()
	{
		LifeCycleSaveData lifeCycleSaveData = SaveSystem.Load(savefilename) as LifeCycleSaveData;

		if (lifeCycleSaveData == null)
		{
			lifeCycleSave = new LifeCycleSave();
		}
		else
		{
			lifeCycleSave = new LifeCycleSave(lifeCycleSaveData);
		}
	}

	void SaveLifeCycle()
	{
		LifeCycleSaveData lifeCycleSaveData = new LifeCycleSaveData(lifeCycleSave);
		SaveSystem.Save(savefilename, lifeCycleSaveData);
	}

	public void AddLifeCycleItem(LifeCycleItem lifeCycleItem)
	{
		Debug.Log("Adding Life Cycle Item: " + lifeCycleItem.name);

		// check if the item already exists
		if (lifeCycleSave.lifeCycleItems.ContainsKey(lifeCycleItem.name))
		{
			Debug.Log("Life Cycle Item: " + lifeCycleItem.name + " already exists");
			return;
		}

		LifeCycleItem item = CheckLifeCycle(lifeCycleItem);
		lifeCycleSave.lifeCycleItems.Add(item.name, item);

		SaveLifeCycle();

		Debug.Log("Life Cycle Item: " + lifeCycleItem.name + " added");
	}

	public void RemoveLifeCycleItem(string name)
	{
		lifeCycleSave.lifeCycleItems.Remove(name);

		SaveLifeCycle();
	}

	public void RemoveLifeCycleItem(LifeCycleItem lifeCycleItem)
	{
		lifeCycleSave.lifeCycleItems.Remove(lifeCycleItem.name);

		SaveLifeCycle();
	}

	public LifeCycleItem GetLifeCycleItem(string name)
	{
		if (lifeCycleSave.lifeCycleItems.ContainsKey(name))
		{
			return lifeCycleSave.lifeCycleItems[name];
		}
		else
		{
			return null;
		}
	}

	public void EnvokeLifeCycleItem(string name)
	{
		if (lifeCycleSave.lifeCycleItems.ContainsKey(name))
		{
			LifeCycleItem item = lifeCycleSave.lifeCycleItems[name];
			item.Envoke = false;
			lifeCycleSave.lifeCycleItems[name] = item;
			SaveLifeCycle();
			Debug.Log("Life Cycle Item: " + name + " envoked");
		}
	}

	// a function that can be called from other scripts to get the time left until the item is envoked
	// return -1 if the item is not found
	// return the time left in text format
	// example: 12d 3h 5m 12s, 3h 5m 12s, 5m, 5m 12s, 12s
	public string GetTimeLeft(string name)
	{
		if (lifeCycleSave.lifeCycleItems.ContainsKey(name))
		{
			LifeCycleItem item = lifeCycleSave.lifeCycleItems[name];
			if (item.startTime > DateTime.Now)
			{
				TimeSpan timeLeft = item.startTime - DateTime.Now;
				string timeLeftText = "";

				if (timeLeft.Days > 0)
				{
					timeLeftText += timeLeft.Days + "d ";
				}

				if (timeLeft.Hours > 0)
				{
					timeLeftText += timeLeft.Hours + "h ";
				}

				if (timeLeft.Minutes > 0)
				{
					timeLeftText += timeLeft.Minutes + "m ";
				}

				if (timeLeft.Seconds > 0)
				{
					timeLeftText += timeLeft.Seconds + "s";
				}

				return timeLeftText;
			}
			else
			{
				return "0s";
			}
		}
		else
		{
			return "-1";
		}
	}
}
