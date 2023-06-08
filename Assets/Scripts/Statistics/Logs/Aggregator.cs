using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Aggregator : MonoBehaviour
{
	public static Aggregator instance;

	private List<string> keys = new List<string>();
	private string saveFileName = "aggregatorSaveData";

	private Dictionary<string, DailyTaskAggregateV2> dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
	public Dictionary<string, NumberLocationAggregate> numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
	public Dictionary<string, WordleAggregate> wordleLogs = new Dictionary<string, WordleAggregate>();
	public Dictionary<string, DustMeOffAggregate> dustMeOffLogs = new Dictionary<string, DustMeOffAggregate>();
	public Dictionary<string, TakeMeOutAggregate> takeMeOutLogs = new Dictionary<string, TakeMeOutAggregate>();
	public Dictionary<string, BreathingExerciseV2Aggregate> breathingExerciseLogs = new Dictionary<string, BreathingExerciseV2Aggregate>();
	public Dictionary<string, ChoresAggregate> choresLogs = new Dictionary<string, ChoresAggregate>();
	public Dictionary<string, JournalAggregate> journalLogs = new Dictionary<string, JournalAggregate>();
	public Dictionary<string, PhysicalExerciseAggregate> PELogs = new Dictionary<string, PhysicalExerciseAggregate>();

	void Awake()
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
		LoadAggregator();
	}

	public void LoadAggregator()
	{
		AggregatorSaveData aggregatorSaveData = SaveSystem.Load(saveFileName) as AggregatorSaveData;

		if (aggregatorSaveData == null)
		{
			return;
		}

		AggregatorSave aggregatorSave = new AggregatorSave(aggregatorSaveData);

		keys = aggregatorSaveData.keys;

		dailyTaskLogs = aggregatorSave.dailyTaskLogs;
		numberLocationLogs = aggregatorSave.numberLocationLogs;
		wordleLogs = aggregatorSave.wordleLogs;
		dustMeOffLogs = aggregatorSave.dustMeOffLogs;
		takeMeOutLogs = aggregatorSave.takeMeOutLogs;
		breathingExerciseLogs = aggregatorSave.breathingExerciseLogs;
		choresLogs = aggregatorSave.choresLogs;
		journalLogs = aggregatorSave.journalLogs;
		PELogs = aggregatorSave.PELogs;
	}

	public void Publish(DailyTaskCompletedEvent dailyTaskCompletedEvent)
	{
		string key = generateKey();
		DailyTaskAggregate dailyTaskAggregate = dailyTaskCompletedEvent.GetData();
		// dailyTaskLogs.Add(key, dailyTaskAggregate);
		DailyScoreCalculator.PublishDailyTask(key, dailyTaskAggregate);

		SaveAggregator();
	}

	public void Publish(ChoreCompletedEvent choreCompletedEvent)
	{
		string key = generateKey();
		ChoresAggregate choreAggregate = choreCompletedEvent.GetData();
		// dailyTaskLogs.Add(key, dailyTaskAggregate);
		DailyScoreCalculator.PublishDailyChore(key, choreAggregate);

		SaveAggregator();
	}

	public void Publish(TaskCompletedEvent taskCompletedEvent)
	{
		string key = generateKey();
		DailyTaskAggregateV2 dailyTaskAggregateV2 = taskCompletedEvent.GetData();
		dailyTaskLogs.Add(key, dailyTaskAggregateV2);
		DailyScoreCalculator.PublishDailyTaskV2(key, dailyTaskAggregateV2);

		SaveAggregator();
	}

	public void Publish(NumberLocationCompletedEvent numberLocationCompletedEvent)
	{
		string key = generateKey();
		NumberLocationAggregate numberLocationAggregate = numberLocationCompletedEvent.GetData();
		numberLocationLogs.Add(key, numberLocationAggregate);
		DailyScoreCalculator.PublishNumberLocation(key, numberLocationAggregate);

		SaveAggregator();
	}

	public void Publish(WordleCompletedEvent wordleCompletedEvent)
	{
		string key = generateKey();
		WordleAggregate wordleAggregate = wordleCompletedEvent.GetData();
		wordleLogs.Add(key, wordleAggregate);
		DailyScoreCalculator.PublishWordle(key, wordleAggregate);

		SaveAggregator();
	}

	public void Publish(DustMeOffCompletedEvent dustMeOffCompleted)
	{
		string key = generateKey();
		DustMeOffAggregate dustMeOffAggregate = dustMeOffCompleted.GetData();
		dustMeOffLogs.Add(key, dustMeOffAggregate);
		DailyScoreCalculator.PublishDustMeOff(key, dustMeOffAggregate);

		SaveAggregator();

	}

	public void Publish(TakeMeOutCompletedEvent takeMeOutCompleted)
	{
		string key = generateKey();
		TakeMeOutAggregate takeMeOutAggregate = takeMeOutCompleted.GetData();
		takeMeOutLogs.Add(key, takeMeOutAggregate);
		DailyScoreCalculator.PublishTakeMeOut(key, takeMeOutAggregate);

		SaveAggregator();
	}

	public void Publish(BreathingExerciseV2CompletedEvent breathingExerciseV2CompletedEvent)
	{
		string key = generateKey();
		BreathingExerciseV2Aggregate breathingExerciseV2Aggregate = breathingExerciseV2CompletedEvent.GetData();
		breathingExerciseLogs.Add(key, breathingExerciseV2Aggregate);
		DailyScoreCalculator.PublishBreathingExerciseV2(key, breathingExerciseV2Aggregate);

		SaveAggregator();
	}

	public void Publish(JournalCompletedEvent journalCompletedEvent)
	{
		string key = generateKey();
		JournalAggregate journalAggregate = journalCompletedEvent.GetData();
		journalLogs.Add(key, journalAggregate);
		DailyScoreCalculator.PublishJournal(key, journalAggregate);

		SaveAggregator();
	}

	public void Publish(PhysicalExerciseCompletedEvent phyiscalExerciseCompletedEvent)
	{
		string key = generateKey();
		PhysicalExerciseAggregate physicalExerciseAggregate = phyiscalExerciseCompletedEvent.GetData();

	}

	public void SaveAggregator()
	{
		AggregatorSave aggregatorSave = new AggregatorSave();
		aggregatorSave.setDailyTaskLogs(dailyTaskLogs);
		aggregatorSave.setNumberLocationLogs(numberLocationLogs);
		aggregatorSave.setWordleLogs(wordleLogs);
		aggregatorSave.setDustMeOffLogs(dustMeOffLogs);
		aggregatorSave.setTakeMeOutLogs(takeMeOutLogs);
		aggregatorSave.setBreathingExerciseLogs(breathingExerciseLogs);
		aggregatorSave.setChoresLogs(choresLogs);
		aggregatorSave.setJournalLogs(journalLogs);
		aggregatorSave.setPELogs(PELogs);

		AggregatorSaveData aggregatorSaveData = new AggregatorSaveData(keys, aggregatorSave);

		SaveSystem.Save(saveFileName, aggregatorSaveData);
	}

	public void SaveCSV<T>(Dictionary<string, T> data, string fileName) where T : IAggregate
	{
		string path = Application.persistentDataPath + "/" + fileName + ".csv";
		string csv = string.Empty;

		// add header
		foreach (KeyValuePair<string, T> entry in data)
		{
			csv += entry.Value.GetCSVHeader() + "\n";
			break;
		}

		foreach (KeyValuePair<string, T> entry in data)
		{
			csv += entry.Value.GetCSVData() + "," + entry.Key + "\n";
		}

		StreamWriter writer = new StreamWriter(path, false);
		writer.Write(csv);
		writer.Flush();
		writer.Close();
		// Debug.Log("Saved CSV: " + path);
	}

	public Dictionary<string, DailyTaskAggregateV2> GetTodaysDailyTaskLogs()
	{
		Dictionary<string, DailyTaskAggregateV2> todaysDailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();

		foreach (KeyValuePair<string, DailyTaskAggregateV2> entry in dailyTaskLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysDailyTaskLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysDailyTaskLogs;
	}

	public Dictionary<string, NumberLocationAggregate> GetTodaysNumberLocationLogs()
	{
		Dictionary<string, NumberLocationAggregate> todaysNumberLocationLogs = new Dictionary<string, NumberLocationAggregate>();

		foreach (KeyValuePair<string, NumberLocationAggregate> entry in numberLocationLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysNumberLocationLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysNumberLocationLogs;
	}

	public Dictionary<string, WordleAggregate> GetTodaysWordleLogs()
	{
		Dictionary<string, WordleAggregate> todaysWordleLogs = new Dictionary<string, WordleAggregate>();

		foreach (KeyValuePair<string, WordleAggregate> entry in wordleLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysWordleLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysWordleLogs;
	}

	public Dictionary<string, DustMeOffAggregate> GetTodaysDustMeOffLogs()
	{
		Dictionary<string, DustMeOffAggregate> todaysDustMeOffLogs = new Dictionary<string, DustMeOffAggregate>();

		foreach (KeyValuePair<string, DustMeOffAggregate> entry in dustMeOffLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysDustMeOffLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysDustMeOffLogs;
	}

	public Dictionary<string, TakeMeOutAggregate> GetTodaysTakeMeOutLogs()
	{
		Dictionary<string, TakeMeOutAggregate> todaysTakeMeOutLogs = new Dictionary<string, TakeMeOutAggregate>();

		foreach (KeyValuePair<string, TakeMeOutAggregate> entry in takeMeOutLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysTakeMeOutLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysTakeMeOutLogs;
	}

	public Dictionary<string, BreathingExerciseV2Aggregate> GetTodaysBreathingExerciseLogs()
	{
		Dictionary<string, BreathingExerciseV2Aggregate> todaysBreathingExerciseLogs = new Dictionary<string, BreathingExerciseV2Aggregate>();

		foreach (KeyValuePair<string, BreathingExerciseV2Aggregate> entry in breathingExerciseLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysBreathingExerciseLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysBreathingExerciseLogs;
	}

	public Dictionary<string, ChoresAggregate> GetTodaysChoresLogs()
	{
		Dictionary<string, ChoresAggregate> todaysChoresLogs = new Dictionary<string, ChoresAggregate>();

		foreach (KeyValuePair<string, ChoresAggregate> entry in choresLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysChoresLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysChoresLogs;
	}

	public Dictionary<string, JournalAggregate> GetTodaysJournalLogs()
	{
		Dictionary<string, JournalAggregate> todaysJournalLogs = new Dictionary<string, JournalAggregate>();


		foreach (KeyValuePair<string, JournalAggregate> entry in journalLogs)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(entry.Value.timestamp);
			DateTime dateTime = dateTimeOffset.LocalDateTime;

			if (dateTime.Day == DateTime.Now.Day)
			{
				todaysJournalLogs.Add(entry.Key, entry.Value);
			}
		}

		return todaysJournalLogs;
	}



	// get all logs

	public Dictionary<string, DailyTaskAggregateV2> GetDailyTaskLogs()
	{
		return dailyTaskLogs;
	}

	public Dictionary<string, NumberLocationAggregate> GetNumberLocationLogs()
	{
		return numberLocationLogs;
	}

	public Dictionary<string, WordleAggregate> GetWordleLogs()
	{
		return wordleLogs;
	}

	public Dictionary<string, DustMeOffAggregate> GetDustMeOffLogs()
	{
		return dustMeOffLogs;
	}

	public Dictionary<string, TakeMeOutAggregate> GetTakeMeOutLogs()
	{
		return takeMeOutLogs;
	}

	public Dictionary<string, BreathingExerciseV2Aggregate> GetBreathingExerciseLogs()
	{
		return breathingExerciseLogs;
	}

	public Dictionary<string, ChoresAggregate> GetChoresLogs()
	{
		return choresLogs;
	}

	public Dictionary<string, JournalAggregate> GetJournalLogs()
	{
		return journalLogs;
	}

	public List<string> GetKeys()
	{
		return keys;
	}



	public void Clear()
	{
		dailyTaskLogs.Clear();
		numberLocationLogs.Clear();
		wordleLogs.Clear();
		dustMeOffLogs.Clear();
		takeMeOutLogs.Clear();
		breathingExerciseLogs.Clear();
		PELogs.Clear();
		keys.Clear();

		AudioManager.instance.PlaySFX("PaperCrumbleSfx");

		SaveAggregator();
	}

	public string generateKey()
	{
		while (true)
		{
			string key = KeyGenerator.GetKey();
			if (!keys.Contains(key))
			{
				keys.Add(key);
				return key;
			}
		}
	}
}

public interface IAggregate
{
	string GetCSVHeader();
	string GetCSVData();
}
