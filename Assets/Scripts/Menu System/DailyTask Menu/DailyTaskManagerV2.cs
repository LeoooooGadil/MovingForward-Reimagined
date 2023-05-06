using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManagerV2 : MonoBehaviour
{
	public static DailyTaskManagerV2 instance;
	public DailyTaskSaveV2 dailyTaskSaveV2;
	public MovingForwardDailyTasksObject dailyTasksObject;
	public int taskCount = 5;

	public float lowPercentage = 0.2f;
	public float mediumPercentage = 0.4f;

	private string saveFileName = "dailytask";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject); // Destroy duplicate
		}

		LoadDailyTask();
		SeperateTasks();
	}

	void Start()
	{
		CheckTask();
		CheckLifeCycle();
	}

	void CheckTask()
	{
		if((dailyTaskSaveV2.dailyTasks.Count == 0 && dailyTaskSaveV2.completedTasks.Count == 0) || dailyTaskSaveV2.GetDate() != System.DateTime.Now.ToString("dd/MM/yyyy"))
		{
			GenerateDailyTasks();
		}
	}

	void LoadDailyTask()
	{
		DailyTaskSaveDataV2 loadedData = SaveSystem.Load(saveFileName) as DailyTaskSaveDataV2;

		if (loadedData != null)
		{
			dailyTaskSaveV2 = new DailyTaskSaveV2(loadedData);
		}
		else
		{
			dailyTaskSaveV2 = new DailyTaskSaveV2();
		}
	}

	void CheckLifeCycle()
	{
		LifeCycleItem thisLifeCycle = LifeCycleManager.instance.GetLifeCycleItem("DailyTask");

		if (thisLifeCycle == null)
		{
			if (dailyTaskSaveV2.GetDate() != System.DateTime.Now.ToString("dd/MM/yyyy"))
			{
				GenerateDailyTasks();
			}

			CreateNewLifeCycle();
			return;
		}

		if (thisLifeCycle.Envoke)
		{
			LifeCycleManager.instance.EnvokeLifeCycleItem("DailyTask");
			GenerateDailyTasks();
			SendNotification();
		}

	}

	void SendNotification()
	{
		System.DateTime tomorrow = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.AddDays(1).Day, 8, 0, 0);

		NotificationManager.instance.SendNotification("Moving Forward", "Good Morning!", tomorrow);

		NotificationManager.instance.SendNotification("Daily Tasks", "You have new tasks to complete", tomorrow);
	}

	void CreateNewLifeCycle()
	{
		LifeCycleItem lifeCycleItem = new LifeCycleItem();
		lifeCycleItem.name = "DailyTask";
		lifeCycleItem.isRepeatable = true;
		// startTime 8:00 AM today
		lifeCycleItem.startTime = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 8, 0, 0);
		lifeCycleItem.maxRepeatCount = -1;
		lifeCycleItem.repeatType = LifeCycleRepeatType.Daily;

		LifeCycleManager.instance.AddLifeCycleItem(lifeCycleItem);
	}

	void SeperateTasks()
	{
		dailyTaskSaveV2.lowPriorityTasks.Clear();
		dailyTaskSaveV2.mediumPriorityTasks.Clear();
		dailyTaskSaveV2.highPriorityTasks.Clear();

		foreach (MovingForwardDailyTasksObject.MoivngForwardDailyTask task in dailyTasksObject.Tasks)
		{
			if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.Low)
			{
				dailyTaskSaveV2.lowPriorityTasks.Add(task);
			}
			else if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.Medium)
			{
				dailyTaskSaveV2.mediumPriorityTasks.Add(task);
			}
			else if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.High)
			{
				dailyTaskSaveV2.highPriorityTasks.Add(task);
			}
		}

		SaveDailyTasks();
	}

	void GenerateDailyTasks()
	{
		dailyTaskSaveV2.dailyTasks.Clear();
		dailyTaskSaveV2.completedTasks.Clear();

		List<Task> newTasks = new List<Task>();

		for (int i = 0; i < taskCount; i++)
		{
			Task newTask = GenerateOneTask();
			newTasks.Add(newTask);
		}

		foreach (Task task in newTasks)
		{
			dailyTaskSaveV2.AddDailyTask(task);
		}

		dailyTaskSaveV2.date = System.DateTime.Now.Date;
		TicketAccess.RemoveTicket("DailyTask");

		Debug.Log("Daily Tasks Generated");

		SaveDailyTasks();
	}
	Task GenerateOneTask()
	{
		int random = Random.Range(0, 100);

		if (random < lowPercentage * 100)
		{
			int randomTask = Random.Range(0, dailyTaskSaveV2.lowPriorityTasks.Count);
			MovingForwardDailyTasksObject.MoivngForwardDailyTask task = dailyTaskSaveV2.lowPriorityTasks[randomTask];
			float compensation = GetCompensation(task);
			return new Task(task).SetCompensation(compensation);
		}
		else if (random < mediumPercentage * 100)
		{
			int randomTask = Random.Range(0, dailyTaskSaveV2.mediumPriorityTasks.Count);
			MovingForwardDailyTasksObject.MoivngForwardDailyTask task = dailyTaskSaveV2.mediumPriorityTasks[randomTask];
			float compensation = GetCompensation(task);
			return new Task(task).SetCompensation(compensation);
		}
		else
		{
			int randomTask = Random.Range(0, dailyTaskSaveV2.highPriorityTasks.Count);
			MovingForwardDailyTasksObject.MoivngForwardDailyTask task = dailyTaskSaveV2.highPriorityTasks[randomTask];
			float compensation = GetCompensation(task);
			return new Task(task).SetCompensation(compensation);
		}
	}

	private float GetCompensation(MovingForwardDailyTasksObject.MoivngForwardDailyTask task)
	{
		// calculate compensation by tasks priority and task points. max compensation is 30 coins.
		// the higher the priority the higher the compensation
		// points will also determine the compensation
		// make better formula here

		float compensation = 0;

		if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.Low)
		{
			compensation = 5 + (task.points / 10);
		}
		else if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.Medium)
		{
			compensation = 10 + (task.points / 10);
		}
		else if (task.priority == MovingForwardDailyTasksObject.DailyTaskPriority.High)
		{
			compensation = 15 + (task.points / 10);
		}

		return compensation;
	}

	void SaveDailyTasks()
	{
		DailyTaskSaveDataV2 dailyTaskSaveDataV2 = new DailyTaskSaveDataV2(dailyTaskSaveV2);
		SaveSystem.Save(saveFileName, dailyTaskSaveDataV2);
	}

	public Dictionary<string, List<Task>> GetTasks()
	{
		Dictionary<string, List<Task>> allTasks = new Dictionary<string, List<Task>>();
		allTasks.Add("unfinished", dailyTaskSaveV2.dailyTasks);
		allTasks.Add("finished", dailyTaskSaveV2.completedTasks);
		return allTasks;
	}

	public void CompleteTask(Task task)
	{
		dailyTaskSaveV2.CompleteTask(task);
		Aggregator.instance.Publish(new TaskCompletedEvent(task));
		ExperienceManager.instance.AddExperience(task.taskPoints);
		ProfileManager.instance.AddMoney(task.taskCompenstation);
		SaveDailyTasks();
	}

	internal Task ResetTask(Task task)
	{
		dailyTaskSaveV2.RemoveDailyTask(task);

		// generate new task and make sure it's not the same as the old one or any other task in the dailyTask and completedTask lists
		Task newTask = GenerateOneTask();

		while (dailyTaskSaveV2.dailyTasks.Contains(newTask) || dailyTaskSaveV2.completedTasks.Contains(newTask))
		{
			newTask = GenerateOneTask();
		}

		dailyTaskSaveV2.AddDailyTask(newTask);
		SaveDailyTasks();
		return newTask;
	}
}
