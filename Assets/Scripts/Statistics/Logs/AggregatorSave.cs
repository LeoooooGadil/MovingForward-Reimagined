using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggregatorSave
{
	public List<string> keys = new List<string>();

	public Dictionary<string, DailyTaskAggregateV2> dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
	public Dictionary<string, NumberLocationAggregate> numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
	public Dictionary<string, WordleAggregate> wordleLogs = new Dictionary<string, WordleAggregate>();
	public Dictionary<string, DustMeOffAggregate> dustMeOffLogs = new Dictionary<string, DustMeOffAggregate>();
	public Dictionary<string, TakeMeOutAggregate> takeMeOutLogs = new Dictionary<string, TakeMeOutAggregate>();
	public Dictionary<string, BreathingExerciseV2Aggregate> breathingExerciseLogs = new Dictionary<string, BreathingExerciseV2Aggregate>();
	public Dictionary<string, ChoresAggregate> choresLogs = new Dictionary<string, ChoresAggregate>();
	public Dictionary<string, JournalAggregate> journalLogs = new Dictionary<string, JournalAggregate>();
	public Dictionary<string, PhysicalExerciseAggregate> PELogs = new Dictionary<string, PhysicalExerciseAggregate>();

	public AggregatorSave(AggregatorSaveData _aggregatorSaveData)
	{
		dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
		numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
		wordleLogs = new Dictionary<string, WordleAggregate>();
		dustMeOffLogs = new Dictionary<string, DustMeOffAggregate>();
		takeMeOutLogs = new Dictionary<string, TakeMeOutAggregate>();
		breathingExerciseLogs = new Dictionary<string, BreathingExerciseV2Aggregate>();
		choresLogs = new Dictionary<string, ChoresAggregate>();
		journalLogs = new Dictionary<string, JournalAggregate>();
		PELogs = new Dictionary<string, PhysicalExerciseAggregate>();

		foreach (KeyValuePair<string, DailyTaskAggregateV2> dailyTaskLog in _aggregatorSaveData.dailyTaskLogs)
		{
			dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
		}

		foreach (KeyValuePair<string, NumberLocationAggregate> numberLocationLog in _aggregatorSaveData.numberLocationLogs)
		{
			numberLocationLogs.Add(numberLocationLog.Key, numberLocationLog.Value);
		}

		foreach (KeyValuePair<string, WordleAggregate> wordleLog in _aggregatorSaveData.wordleLogs)
		{
			wordleLogs.Add(wordleLog.Key, wordleLog.Value);
		}

		foreach (KeyValuePair<string, DustMeOffAggregate> dustMeOffLog in _aggregatorSaveData.dustMeOffLogs)
		{
			dustMeOffLogs.Add(dustMeOffLog.Key, dustMeOffLog.Value);
		}

		foreach (KeyValuePair<string, TakeMeOutAggregate> takeMeOutLog in _aggregatorSaveData.takeMeOutLogs)
		{
			takeMeOutLogs.Add(takeMeOutLog.Key, takeMeOutLog.Value);
		}

		foreach (KeyValuePair<string, BreathingExerciseV2Aggregate> breathingExerciseLog in _aggregatorSaveData.breathingExerciseLogs)
		{
			breathingExerciseLogs.Add(breathingExerciseLog.Key, breathingExerciseLog.Value);
		}

		foreach (KeyValuePair<string, ChoresAggregate> choresLog in _aggregatorSaveData.choresLogs)
		{
			choresLogs.Add(choresLog.Key, choresLog.Value);
		}

		foreach (KeyValuePair<string, JournalAggregate> journalLog in _aggregatorSaveData.journalLogs)
		{
			journalLogs.Add(journalLog.Key, journalLog.Value);
		}

		foreach (KeyValuePair<string, PhysicalExerciseAggregate> PELog in _aggregatorSaveData.PELogs)
		{
			PELogs.Add(PELog.Key, PELog.Value);
		}
	}

	public AggregatorSave()
	{
		dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
		numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
		wordleLogs = new Dictionary<string, WordleAggregate>();
		dustMeOffLogs = new Dictionary<string, DustMeOffAggregate>();
		takeMeOutLogs = new Dictionary<string, TakeMeOutAggregate>();
		breathingExerciseLogs = new Dictionary<string, BreathingExerciseV2Aggregate>();
		choresLogs = new Dictionary<string, ChoresAggregate>();
		journalLogs = new Dictionary<string, JournalAggregate>();
		PELogs = new Dictionary<string, PhysicalExerciseAggregate>();
	}

	public void setDailyTaskLogs(Dictionary<string, DailyTaskAggregateV2> _dailyTaskLogs)
	{
		dailyTaskLogs = _dailyTaskLogs;
	}

	public void setNumberLocationLogs(Dictionary<string, NumberLocationAggregate> _numberLocationLogs)
	{
		numberLocationLogs = _numberLocationLogs;
	}

	public void setWordleLogs(Dictionary<string, WordleAggregate> _wordleLogs)
	{
		wordleLogs = _wordleLogs;
	}

	public void setDustMeOffLogs(Dictionary<string, DustMeOffAggregate> _dustMeOffLogs)
	{
		dustMeOffLogs = _dustMeOffLogs;
	}

	public void setTakeMeOutLogs(Dictionary<string, TakeMeOutAggregate> _takeMeOutLogs)
	{
		takeMeOutLogs = _takeMeOutLogs;
	}

	public void setBreathingExerciseLogs(Dictionary<string, BreathingExerciseV2Aggregate> _breathingExerciseLogs)
	{
		breathingExerciseLogs = _breathingExerciseLogs;
	}

	public void setChoresLogs(Dictionary<string, ChoresAggregate> _choresLogs)
	{
		choresLogs = _choresLogs;
	}

	public void setJournalLogs(Dictionary<string, JournalAggregate> _journalLogs)
	{
		journalLogs = _journalLogs;
	}

	public void setPELogs(Dictionary<string, PhysicalExerciseAggregate> _PELogs)
	{
		PELogs = _PELogs;
	}
}
