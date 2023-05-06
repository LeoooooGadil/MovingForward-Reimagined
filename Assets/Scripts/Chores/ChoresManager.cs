using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoresManager : MonoBehaviour
{
	public static ChoresManager instance;
	public ChoresSave choresSave;
	public MovingForwardDailyChoresScriptableObject dailyChoresObject;
	public int dailyChoreCount = 3;

	private string saveFileName = "ChoresManager";

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
	}

	void Start()
	{
		CheckChore();
		CheckLifeCycle();
	}

	void CheckChore()
	{
		if (choresSave.chores.Count == 0 && choresSave.completedChores.Count == 0)
		{
			GenerateDailyChores();
		}
	}

	void LoadDailyTask()
	{
		ChoresSaveData loadedData = SaveSystem.Load(saveFileName) as ChoresSaveData;

		if (loadedData != null)
		{
			choresSave = new ChoresSave(loadedData);
		}
		else
		{
			choresSave = new ChoresSave();
		}
	}

	void CheckLifeCycle()
	{
		LifeCycleItem thisLifeCycle = LifeCycleManager.instance.GetLifeCycleItem("DailyChore");

		if (thisLifeCycle == null)
		{
			if (choresSave.GetDate() != System.DateTime.Now.ToString("dd/MM/yyyy"))
			{
				GenerateDailyChores();
			}

			CreateNewLifeCycle();
			return;
		}

		if (thisLifeCycle.Envoke)
		{
			LifeCycleManager.instance.EnvokeLifeCycleItem("DailyChore");
			GenerateDailyChores();
			SendNotification();
		}
	}

	void SendNotification()
	{
		System.DateTime tomorrow = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.AddDays(1).Day, 8, 0, 0);
		NotificationManager.instance.SendNotification("Daily Chores", "New chores are waiting for you!", tomorrow);
	}

	void CreateNewLifeCycle()
	{
		LifeCycleItem lifeCycleItem = new LifeCycleItem();
		lifeCycleItem.name = "DailyChore";
		lifeCycleItem.isRepeatable = true;

		lifeCycleItem.startTime = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, 8, 0, 0);
		lifeCycleItem.maxRepeatCount = -1;
		lifeCycleItem.repeatType = LifeCycleRepeatType.Daily;

		LifeCycleManager.instance.AddLifeCycleItem(lifeCycleItem);
	}

	void GenerateDailyChores()
	{
		choresSave.chores.Clear();
		choresSave.completedChores.Clear();

		List<Chore> dailyChores = new List<Chore>();

		if (dailyChoresObject.chores.Count < dailyChoreCount)
		{
			for (int i = 0; i < dailyChoresObject.chores.Count; i++)
			{
				Chore chore = GenerateOneChore(i);
				dailyChores.Add(chore);
			}

			choresSave.chores = dailyChores;
			choresSave.date = System.DateTime.Now.Date;
			SaveDailyChores();
			return;
		}

		for (int i = 0; i < dailyChoresObject.chores.Count; i++)
		{
			int randomChore = Random.Range(0, dailyChoresObject.chores.Count);
			Chore chore = GenerateOneChore(randomChore);
			dailyChores.Add(chore);
		}

		choresSave.chores = dailyChores;
		choresSave.date = System.DateTime.Now.Date;
		SaveDailyChores();
		return;
	}

	Chore GenerateOneChore(int _index = -1)
	{
		Chores chore = dailyChoresObject.chores[_index];
		float compensation = GetCompensation(chore);
		Chore pickedChore = new Chore(
			chore.name,
			compensation,
			chore.room,
			chore.type
		);
		return pickedChore;
	}

	private float GetCompensation(Chores _chore)
	{
		float compensation = 0;
		compensation = Random.Range(10, 30);
		return compensation;
	}

	void SaveDailyChores()
	{
		ChoresSaveData choresSaveData = new ChoresSaveData(choresSave);
		SaveSystem.Save(saveFileName, choresSaveData);
	}

	public void CompleteChore(Chore _chore)
	{
		choresSave.CompleteChore(_chore);
		ProfileManager.instance.AddMoney(_chore.choreComponensation);
		SaveDailyChores();
	}

	public Dictionary<string, List<Chore>> GetChores()
	{
		Dictionary<string, List<Chore>> chores = new Dictionary<string, List<Chore>>();
		chores.Add("unfinished", choresSave.chores);
		chores.Add("finished", choresSave.completedChores);
		return chores;
	}

	public Chore FindChore(DailyChoreRoom room, DailyChoreType type)
	{
		Chore chore = choresSave.FindChore(room, type);
		if (chore != null)
		{
			return chore;
		}
		else
		{
			Debug.Log("No chore found");
			return null;
		}
	}
}
