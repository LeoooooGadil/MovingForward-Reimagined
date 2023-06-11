using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoresManager : MonoBehaviour
{
	public static ChoresManager instance;
	public ChoresSave choresSave;
	public MovingForwardDailyChoresScriptableObject dailyChoresObject;
	public int dailyChoreCount = 6;

	private string saveFileName = "ChoresManager";

	public Chore activeChore;
	private bool isFirstLaunch = true;

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
			Debug.Log("No chores found, generating new chores");
			GenerateDailyChores();
		}
	}

	void LoadDailyTask()
	{
		ChoresSaveData loadedData = SaveSystem.Load(saveFileName) as ChoresSaveData;

		if (loadedData != null)
		{
			choresSave = new ChoresSave(loadedData);
			isFirstLaunch = false;
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
			Debug.Log("lifecycle not found. Generating New Tasks");
			GenerateDailyChores();
			CreateNewLifeCycle();
			SendNotification();
			return;
		}

		if (thisLifeCycle.Envoke)
		{
			Debug.Log("Lifecycle Finished Timer. Generating New Tasks");
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

		lifeCycleItem.startTime = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day + 1, 8, 0, 0);
		lifeCycleItem.maxRepeatCount = -1;
		lifeCycleItem.repeatType = LifeCycleRepeatType.Daily;

		LifeCycleManager.instance.AddLifeCycleItem(lifeCycleItem);
	}

	List<Chore> GenerateMandatoryChores()
	{
		// get the half oc the daily chore count and round it up
		int half = Mathf.CeilToInt(dailyChoreCount / 2);

		List<Chore> dailyChores = new List<Chore>();
		List<Chores> mandatoryChores = new List<Chores>();

		for (int i = 0; i < dailyChoresObject.chores.Count; i++)
		{
			if (dailyChoresObject.chores[i].isMandatory == true)
				mandatoryChores.Add(dailyChoresObject.chores[i]);
		}

		if (isFirstLaunch)
		{
			Debug.Log("First Launch");

			foreach (Chores _chore in mandatoryChores)
			{
				if (_chore.room == DailyChoreRoom.LivingRoom && _chore.type == DailyChoreType.DustMeOff)
				{
					int TheTutorialChore = mandatoryChores.IndexOf(_chore);
					Chore chore = GenerateOneChore(TheTutorialChore, mandatoryChores);
					dailyChores.Add(chore);
					mandatoryChores.Remove(_chore);
					half--;
					break;
				}
			}
		}

		while (half > 0)
		{
			int randomChore = Random.Range(0, mandatoryChores.Count);

			while (CheckIfChoreIsAlreadyInTheList(mandatoryChores[randomChore], dailyChores) && CheckIfChoreIsYesterdayChore(mandatoryChores[randomChore]))
			{
				randomChore = Random.Range(0, mandatoryChores.Count);
			}

			Chore chore = GenerateOneChore(randomChore, mandatoryChores);
			dailyChores.Add(chore);
			mandatoryChores.RemoveAt(randomChore);
			half--;
		}

		choresSave.completedChores.Clear();
		choresSave.chores.Clear();

		return dailyChores;
	}

	void GenerateDailyChores()
	{
		
		TicketAccess.ResetAllTickts();

		int choreCount = dailyChoreCount;

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

		List<Chore> mandatoryChores = GenerateMandatoryChores();

		foreach (Chore chore in mandatoryChores)
		{
			dailyChores.Add(chore);
			choreCount--;
		}

		while (choreCount > 0)
		{
			int randomChore = Random.Range(0, dailyChoresObject.chores.Count);

			while (dailyChoresObject.chores[randomChore].isMandatory == true || CheckIfChoreIsAlreadyInTheList(dailyChoresObject.chores[randomChore], dailyChores))
			{
				randomChore = Random.Range(0, dailyChoresObject.chores.Count);
			}

			Chore chore = GenerateOneChore(randomChore);
			dailyChores.Add(chore);
			choreCount--;
		}

		choresSave.chores = dailyChores;
		choresSave.date = System.DateTime.Now.Date;
		SaveDailyChores();
		return;
	}

	// 
	public bool CheckIfChoreIsAlreadyInTheList(Chores chores, List<Chore> alreadyChores)
	{
		foreach (Chore chore in alreadyChores)
		{
			if (chore.choreName == chores.name)
			{
				return true;
			}
		}

		return false;
	}

	public bool CheckIfChoreIsYesterdayChore(Chores _chore)
	{
		foreach (Chore chore in choresSave.chores)
		{
			if (chore.choreName == _chore.name)
			{
				return true;
			}
		}

		return false;
	}

	Chore GenerateOneChore(int _index = -1)
	{
		Chores chore = dailyChoresObject.chores[_index];
		float compensation = GetCompensation(chore);
		Chore pickedChore = new Chore(
			chore.name,
			compensation,
			chore.minScore,
			chore.room,
			chore.type,
			chore.isMandatory
		);
		return pickedChore;
	}

	Chore GenerateOneChore(int _index = -1, List<Chores> _chore = null)
	{
		Chores chore = _chore[_index];
		float compensation = GetCompensation(chore);
		Chore pickedChore = new Chore(
			chore.name,
			compensation,
			chore.minScore,
			chore.room,
			chore.type,
			chore.isMandatory
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
		OnScreenNotificationManager.instance.CreateNotification("Chore completed!", OnScreenNotificationType.Sucess);
		OnScreenNotificationManager.instance.CreateNotification("You earned " + _chore.choreComponensation + " coins!", OnScreenNotificationType.Info);
		ProfileManager.instance.AddMoney(_chore.choreComponensation);
		Aggregator.instance.Publish(new ChoreCompletedEvent(_chore));
		choresSave.CompleteChore(_chore);
		AudioManager.instance.PlaySFX("ChoreCompleteSfx");
		activeChore = null;
		SaveDailyChores();
	}

	public Chore GetActiveChore()
	{
		// check if there is an active chore
		if (activeChore == null)
		{
			// if there is no active chore, return null
			return null;
		}

		// if there is an active chore, check if it is completed
		return activeChore;
	}

	public void RemoveChore()
	{
		if (activeChore == null) return;

		activeChore = null;
	}

	public void CompleteChore(DailyChoreType _type)
	{
		if (activeChore == null)
		{
			Debug.Log("No active chore");
			return;
		}

		if (activeChore.dailyChoreType == _type)
		{
			CompleteChore(activeChore);
		}
		else
		{
			Debug.Log("Wrong chore type");
		}
	}

	public Dictionary<string, List<Chore>> GetChores()
	{
		Dictionary<string, List<Chore>> chores = new Dictionary<string, List<Chore>>();
		chores.Add("unfinished", choresSave.chores);
		chores.Add("finished", choresSave.completedChores);
		return chores;
	}

	public List<Chore> GetUnfinishedChores()
	{
		return choresSave.chores;
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

	public void PlayChore(Chore chore)
	{
		StartCoroutine(PlayTheChore(chore));
	}

	public void ChangeScene(DailyChoreRoom room)
	{
		SceneryManager.instance.SetScenery(room);
	}

	public void LoadLevel(DailyChoreType type)
	{
		string sceneName = "";

		// find the scene name from the dailyChoresObject
		for (int i = 0; i < dailyChoresObject.chores.Count; i++)
		{
			if (dailyChoresObject.chores[i].type == type)
			{
				sceneName = dailyChoresObject.chores[i].sceneName;
				break;
			}
		}


		if (sceneName != "")
			LevelManager.instance.ChangeScene(sceneName, true, SceneTransitionMode.Slide, false);
		else
			OnScreenNotificationManager.instance.CreateNotification("No scene found", OnScreenNotificationType.Error);
		Debug.Log("No scene found");
	}

	IEnumerator PlayTheChore(Chore chore)
	{
		MenuManager.Instance.CloseMenu();
		yield return new WaitForSeconds(0.5f);
		ChangeScene(chore.dailyChoreRoom);
		yield return new WaitForSeconds(0.5f);
		LoadLevel(chore.dailyChoreType);
		activeChore = chore;
	}
}
