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
	}

	void Start()
	{
		LoadLifeCycle();
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

		foreach (KeyValuePair<string, LifeCycleItem> lifeCycleItem in lifeCycleSave.lifeCycleItems)
		{
			LifeCycleItem item = lifeCycleItem.Value;

			if (item.startTime < DateTime.Now && !item.Envoke)
			{
				Debug.Log("Life Cycle Item: " + item.name + " Envoked");
				item.Envoke = true;

				if (item.isRepeatable)
				{
					switch (item.repeatType)
					{
						case LifeCycleRepeatType.Daily:
							item.startTime = item.startTime.AddDays(1);
							break;
						case LifeCycleRepeatType.Weekly:
							item.startTime = item.startTime.AddDays(7);
							break;
						case LifeCycleRepeatType.Monthly:
							item.startTime = item.startTime.AddMonths(1);
							break;
						case LifeCycleRepeatType.Yearly:
							item.startTime = item.startTime.AddYears(1);
							break;
						case LifeCycleRepeatType.Custom:
							item.startTime = item.startTime.AddSeconds(item.customRepeatTime);
							break;
						default:
							break;
					}

					item.repeatCount++;
					if (item.repeatCount >= item.maxRepeatCount)
					{
						item.isRepeatable = false;
					}
				}

				lifeCycleSave.lifeCycleItems[item.name] = item;
			}
		}

		SaveLifeCycle();
	}

	void LoadLifeCycle()
	{
		LifeCycleSaveData lifeCycleSaveData = Resources.Load<LifeCycleSaveData>(savefilename);

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
		// check if the item already exists
		if (lifeCycleSave.lifeCycleItems.ContainsKey(lifeCycleItem.name))
		{
			Debug.LogError("Life Cycle Item: " + lifeCycleItem.name + " already exists");
			return;
		}

		lifeCycleSave.lifeCycleItems.Add(lifeCycleItem.name, lifeCycleItem);
	}

	public void RemoveLifeCycleItem(string name)
	{
		lifeCycleSave.lifeCycleItems.Remove(name);
	}

	public void RemoveLifeCycleItem(LifeCycleItem lifeCycleItem)
	{
		lifeCycleSave.lifeCycleItems.Remove(lifeCycleItem.name);
	}

	public LifeCycleItem GetLifeCycleItem(string name)
	{
		return lifeCycleSave.lifeCycleItems[name];
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
