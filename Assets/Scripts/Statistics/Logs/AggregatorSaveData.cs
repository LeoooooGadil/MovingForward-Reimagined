using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AggregatorSaveData
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

	public AggregatorSaveData(List<string> _keys, AggregatorSave _aggregatorSave)
	{
		keys = _keys;

		dailyTaskLogs = new Dictionary<string, DailyTaskAggregateV2>();
		numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
		wordleLogs = new Dictionary<string, WordleAggregate>();
		dustMeOffLogs = new Dictionary<string, DustMeOffAggregate>();
		takeMeOutLogs = new Dictionary<string, TakeMeOutAggregate>();
		breathingExerciseLogs = new Dictionary<string, BreathingExerciseV2Aggregate>();
		choresLogs = new Dictionary<string, ChoresAggregate>();
		PELogs = new Dictionary<string, PhysicalExerciseAggregate>();

		foreach (KeyValuePair<string, DailyTaskAggregateV2> dailyTaskLog in _aggregatorSave.dailyTaskLogs)
		{
			dailyTaskLogs.Add(dailyTaskLog.Key, dailyTaskLog.Value);
		}

		foreach (KeyValuePair<string, NumberLocationAggregate> numberLocationLog in _aggregatorSave.numberLocationLogs)
		{
			numberLocationLogs.Add(numberLocationLog.Key, numberLocationLog.Value);
		}

		foreach (KeyValuePair<string, WordleAggregate> wordleLog in _aggregatorSave.wordleLogs)
		{
			wordleLogs.Add(wordleLog.Key, wordleLog.Value);
		}

		foreach (KeyValuePair<string, DustMeOffAggregate> dustMeOffLog in _aggregatorSave.dustMeOffLogs)
		{
			dustMeOffLogs.Add(dustMeOffLog.Key, dustMeOffLog.Value);
		}

		foreach (KeyValuePair<string, TakeMeOutAggregate> takeMeOutLog in _aggregatorSave.takeMeOutLogs)
		{
			takeMeOutLogs.Add(takeMeOutLog.Key, takeMeOutLog.Value);
		}

		foreach (KeyValuePair<string, BreathingExerciseV2Aggregate> breathingExerciseLog in _aggregatorSave.breathingExerciseLogs)
		{
			breathingExerciseLogs.Add(breathingExerciseLog.Key, breathingExerciseLog.Value);
		}

		foreach (KeyValuePair<string, ChoresAggregate> choresLog in _aggregatorSave.choresLogs)
		{
			choresLogs.Add(choresLog.Key, choresLog.Value);
		}

		foreach (KeyValuePair<string, JournalAggregate> journalLog in _aggregatorSave.journalLogs)
		{
			journalLogs.Add(journalLog.Key, journalLog.Value);
		}

		foreach (KeyValuePair<string, PhysicalExerciseAggregate> PELog in _aggregatorSave.PELogs)
		{
			PELogs.Add(PELog.Key, PELog.Value);
		}
	}
}
