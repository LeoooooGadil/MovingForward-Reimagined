using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskItem : MonoBehaviour
{
	public Text taskNameText;
	public Text taskPointsText;
	public CheckboxHandler checkboxHandler;
	public DailyTaskManager dailyTaskManager;

	public string taskName;
	public int taskPoints;

	public bool isCompleted = false;

	void Start()
	{
		taskNameText.text = taskName;
		taskPointsText.text = taskPoints.ToString() + " pts";
		checkboxHandler.SetChecked(isCompleted);
	}

	void Update()
	{
		// put middle line through the task name if it is completed
		if (isCompleted)
		{
			taskNameText.text = StrikeThrough(taskName);
		}
		else
		{
			taskNameText.text = taskName;
		}
	}

	public string StrikeThrough(string s)
	{
		string strikethrough = "";
		foreach (char c in s)
		{
			strikethrough = strikethrough + c + '\u0336';
		}
		return strikethrough;
	}

	public bool IsCompleted()
	{
		return isCompleted;
	}

	public void SetCompleted(bool value)
	{
		isCompleted = value;
		dailyTaskManager.UpdateTask(taskName, value);
	}
}
