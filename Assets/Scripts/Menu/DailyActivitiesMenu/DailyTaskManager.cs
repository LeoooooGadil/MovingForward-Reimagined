using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
	public MovingForwardDailyTasksObject dailyTasksObject;

	public GameObject dailyTaskItemPrefab;
	public Transform dailyTaskContainer;

	public int taskCount = 5;
	public DailyTaskSave dailyTaskSave;

	public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> lowPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
	public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> mediumPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();
	public List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> highPriorityTasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();

	public int lowPercentage = 20;
	public int mediumPercentage = 40;

	private string saveFileName = "dailytask";

	void Awake()
	{
		DailyTaskSaveData loadedData = SaveSystem.Load(saveFileName) as DailyTaskSaveData;
		
		if (loadedData != null)
		{
			dailyTaskSave = new DailyTaskSave(loadedData);
		}
		else
		{
			dailyTaskSave = new DailyTaskSave();
		}
	}

	void Start() {
		if (dailyTaskSave.GetDailyTaskCount() == 0) {
			SeperateTasksBasedOnTheirPriority();
			GenerateDailyTasks();
			SaveDailyTasks();
		} else {
			LoadDailyTasksFromMemory();
		}
	}

	void SeperateTasksBasedOnTheirPriority()
	{
		foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in dailyTasksObject.Tasks)
		{
			if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.Low)
			{
				lowPriorityTasks.Add(task);
			}
			else if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.Medium)
			{
				mediumPriorityTasks.Add(task);
			}
			else if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.High)
			{
				highPriorityTasks.Add(task);
			}
		}
	}

	void GenerateDailyTasks()
	{
		// clear the container
		foreach (Transform child in dailyTaskContainer)
		{
			Destroy(child.gameObject);
		}

		// generate the tasks
		for (int i = 0; i < taskCount; i++)
		{
			// generate a random number between 0 and 100
			int randomNumber = Random.Range(0, 100);

			if (randomNumber < lowPercentage)
			{
				GenerateTask(lowPriorityTasks);
			}
			else if (randomNumber < mediumPercentage)
			{
				GenerateTask(mediumPriorityTasks);
			}
			else if (randomNumber < 100)
			{
				GenerateTask(highPriorityTasks);
			}
		}

	}

	void GenerateTask(List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> taskList)
	{
		// generate a random number between 0 and the length of the task list
		int randomNumber = Random.Range(0, taskList.Count);

		// get the task at the random index
		MovingForwardDailyTasksObject.MoivngForwardDailyTask task = taskList[randomNumber];

		// add the task to the daily tasks array
		dailyTaskSave.AddDailyTask(new DailyTask(task.name, task.points, task.priority));

		// instantiate the task item prefab
		GameObject taskItem = Instantiate(dailyTaskItemPrefab, dailyTaskContainer);

		// set the task item's text to the task's name
		DailyTaskItem dailyTaskItem = taskItem.GetComponent<DailyTaskItem>();
		dailyTaskItem.taskName = task.name;
		dailyTaskItem.taskPoints = task.points;
		dailyTaskItem.dailyTaskManager = this;
	}

	void LoadDailyTasksFromMemory()
	{
		// clear the container
		foreach (Transform child in dailyTaskContainer)
		{
			Destroy(child.gameObject);
		}

		// generate the tasks
		foreach (DailyTask task in dailyTaskSave.GetDailyTasks())
		{
			GenerateTask(task);
		}
	}

	void GenerateTask(DailyTask task)
	{
		// instantiate the task item prefab
		GameObject taskItem = Instantiate(dailyTaskItemPrefab, dailyTaskContainer);

		// set the task item's text to the task's name
		DailyTaskItem dailyTaskItem = taskItem.GetComponent<DailyTaskItem>();
		dailyTaskItem.dailyTaskManager = this;
		dailyTaskItem.taskName = task.name;
		dailyTaskItem.taskPoints = task.points;
		dailyTaskItem.isCompleted = task.isCompleted;
	}

	public void UpdateTask(string name, bool isCompleted)
	{
		dailyTaskSave.UpdateIsCompleted(name, isCompleted);
		SaveDailyTasks();
	}

	public void SaveDailyTasks()
	{
		DailyTaskSaveData saveData = new DailyTaskSaveData(dailyTaskSave);
		SaveSystem.Save(saveFileName, saveData);
	}
}
