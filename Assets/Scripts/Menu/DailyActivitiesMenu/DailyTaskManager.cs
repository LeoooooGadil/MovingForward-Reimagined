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

	void Start()
	{
		if (dailyTaskSave.GetDailyTaskCount() == 0)
		{
			SeperateTasksBasedOnTheirPriority();
			GenerateDailyTasks();
			SaveDailyTasks();
		}
		else
		{
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

		List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> tasks = new List<MovingForwardDailyTasksObject.MoivngForwardDailyTask>();

		for (int i = 0; i < taskCount; i++)
		{
			// generate a random number between 0 and 100
			int randomNumber = Random.Range(0, 100);

			if (randomNumber < lowPercentage)
			{
				int randomTask = Random.Range(0, lowPriorityTasks.Count);
				MovingForwardDailyTasksObject.MoivngForwardDailyTask task = lowPriorityTasks[randomTask];
				dailyTaskSave.AddDailyTask(new DailyTask(task.name, task.points, task.priority));
				tasks.Add(task);
			}
			else if (randomNumber < mediumPercentage)
			{
				int randomTask = Random.Range(0, mediumPriorityTasks.Count);
				MovingForwardDailyTasksObject.MoivngForwardDailyTask task = mediumPriorityTasks[randomTask];
				dailyTaskSave.AddDailyTask(new DailyTask(task.name, task.points, task.priority));
				tasks.Add(task);
			}
			else if (randomNumber < 100)
			{
				int randomTask = Random.Range(0, highPriorityTasks.Count);
				MovingForwardDailyTasksObject.MoivngForwardDailyTask task = highPriorityTasks[randomTask];
				dailyTaskSave.AddDailyTask(new DailyTask(task.name, task.points, task.priority));
				tasks.Add(task);
			}
		}

		StartCoroutine(PlaceGeneratedTaskInContainer(tasks));
	}

	IEnumerator PlaceGeneratedTaskInContainer(List<MovingForwardDailyTasksObject.MoivngForwardDailyTask> tasks)
	{
		// generate the tasks
		foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in tasks)
		{
			GenerateTask(task);

			yield return new WaitForSeconds(0.1f);
		}
	}

	void GenerateTask(MovingForwardDailyTasksObject.MoivngForwardDailyTask task)
	{
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

		StartCoroutine(PlaceLoadedTaskInContainer());
	}

	IEnumerator PlaceLoadedTaskInContainer()
	{
		// generate the tasks
		foreach (DailyTask task in dailyTaskSave.GetDailyTasks())
		{
			GenerateTask(task);

			yield return new WaitForSeconds(0.1f);
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

	public void ResetDailyTasks()
	{
		dailyTaskSave.ResetDailyTasks();
		SaveDailyTasks();
		GenerateDailyTasks();
	}
}
