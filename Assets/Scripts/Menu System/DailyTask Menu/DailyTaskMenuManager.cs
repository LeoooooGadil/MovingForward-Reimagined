using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskMenuManager : MonoBehaviour
{
	public List<Task> unfinishedTasks;
	public List<Task> finishedTasks;

	public GameObject taskPrefab;
	public Transform taskContainer;
	public Text ratioText;

	void Start()
	{
		LoadTasks();
	}

	void OnDisable()
	{
		CleanUpTasks();
	}

	void OnEnable()
	{
		LoadTasks();
		DisplayTasks();
	}

	void Update()
	{
		UpdateRatioText();
	}

	void UpdateRatioText()
	{

		if (DailyTaskManagerV2.instance == null)
		{
			Debug.LogError("DailyTaskManagerV2.instance is null");
			return;
		}


		List<Task> loadedFinishedTasks = DailyTaskManagerV2.instance.GetTasks()["finished"];
		List<Task> loadedUnfinishedTasks = DailyTaskManagerV2.instance.GetTasks()["unfinished"];

		if (loadedFinishedTasks == null || loadedUnfinishedTasks == null) return;

		ratioText.text = loadedUnfinishedTasks.Count + "/" + loadedFinishedTasks.Count;
	}

	void LoadTasks()
	{
		if (DailyTaskManagerV2.instance == null)
		{
			Debug.LogError("DailyTaskManagerV2.instance is null");
			return;
		}

		List<Task> loadedFinishedTasks = DailyTaskManagerV2.instance.GetTasks()["finished"];
		List<Task> loadedUnfinishedTasks = DailyTaskManagerV2.instance.GetTasks()["unfinished"];

		if (loadedFinishedTasks == null || loadedUnfinishedTasks == null) return;

		Debug.Log("Loaded " + loadedFinishedTasks.Count + " finished tasks");
		Debug.Log("Loaded " + loadedUnfinishedTasks.Count + " unfinished tasks");

		unfinishedTasks = loadedUnfinishedTasks;
		finishedTasks = loadedFinishedTasks;
	}

	void DisplayTasks()
	{
		if (unfinishedTasks == null || finishedTasks == null) return;

		StartCoroutine(DisplayTasksWithDelay());
	}

	IEnumerator DisplayTasksWithDelay()
	{
		int index = 0;
		foreach (Task task in unfinishedTasks)
		{
			GameObject taskObject = Instantiate(taskPrefab, taskContainer);
			taskObject.GetComponent<TaskItem>().index = index;
			taskObject.GetComponent<TaskItem>().task = task;
			taskObject.GetComponent<TaskItem>().dailyTaskMenuManager = this;
			index++;
			yield return new WaitForSeconds(0.15f);
		}

		foreach (Task task in finishedTasks)
		{
			GameObject taskObject = Instantiate(taskPrefab, taskContainer);
			taskObject.GetComponent<TaskItem>().index = index;
			taskObject.GetComponent<TaskItem>().task = task;
			taskObject.GetComponent<TaskItem>().dailyTaskMenuManager = this;
			index++;
			yield return new WaitForSeconds(0.15f);
		}
	}

	void CleanUpTasks()
	{
		foreach (Transform child in taskContainer)
		{
			Destroy(child.gameObject);
		}
	}

	public void CompleteTask(Task task)
	{
		if (task == null) return;
		DailyTaskManagerV2.instance.CompleteTask(task);
	}

	public Task ResetTask(int index, Task task)
	{
		if (task == null) return null;
		Task reloadedTask = DailyTaskManagerV2.instance.ResetTask(task);
		if (reloadedTask == null) return task;
		LoadTasks();
		return reloadedTask;
	}
}
