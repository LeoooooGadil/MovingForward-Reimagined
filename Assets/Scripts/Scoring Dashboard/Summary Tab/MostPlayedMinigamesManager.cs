using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostPlayedMinigamesManager : MonoBehaviour
{
	public GameObject PlayedMinigameItemPrefab;
	public Transform PlayedMinigameItemContainer;

	public Dictionary<string, NumberLocationAggregate> numberLocationLogs = new Dictionary<string, NumberLocationAggregate>();
	public Dictionary<string, WordleAggregate> wordleLogs = new Dictionary<string, WordleAggregate>();

	public float currentTimer = 0;

	public PlayedMinigame NumberLocationMinigame = new PlayedMinigame("Number Location");
	public PlayedMinigame WordleMinigame = new PlayedMinigame("Wordle");

	void Start()
	{
		LoadPlayedMinigames();
	}

	void OnEnable()
	{
        StartCoroutine(UpdateUI());
	}

	void OnDisable()
	{
		foreach (Transform child in PlayedMinigameItemContainer)
		{
			Destroy(child.gameObject);
		}
	}

	IEnumerator UpdateUI()
	{

        foreach (Transform child in PlayedMinigameItemContainer)
		{
			Destroy(child.gameObject);
		}

		// the playedMinigameItems[0] is the most played minigame
		// the playedMinigameItems[1] is the second most played minigame

		if (NumberLocationMinigame.minigameTimes > WordleMinigame.minigameTimes)
		{
			GeneratePlayedMinigameItems(1, NumberLocationMinigame.minigameName, NumberLocationMinigame.minigameTimes, NumberLocationMinigame.minigameScore);
			yield return new WaitForSeconds(0.1f);
			GeneratePlayedMinigameItems(2, WordleMinigame.minigameName, WordleMinigame.minigameTimes, WordleMinigame.minigameScore);
		}
		else
		{
			GeneratePlayedMinigameItems(1, WordleMinigame.minigameName, WordleMinigame.minigameTimes, WordleMinigame.minigameScore);
			yield return new WaitForSeconds(0.1f);
			GeneratePlayedMinigameItems(2, NumberLocationMinigame.minigameName, NumberLocationMinigame.minigameTimes, NumberLocationMinigame.minigameScore);
		}
	}

	void GeneratePlayedMinigameItems(int number, string name, int times, float score)
	{
		GameObject playedMinigameItem = Instantiate(PlayedMinigameItemPrefab, PlayedMinigameItemContainer);
		playedMinigameItem.GetComponent<PlayedMinigameItem>().SetMinigame(number, name, times, score);
	}

	void Update()
	{
		if (currentTimer < 0.5)
		{
			currentTimer += Time.deltaTime;
		}
		else
		{
			currentTimer = 0;
			LoadPlayedMinigames();
		}
	}

	void LoadPlayedMinigames()
	{
		if (Aggregator.instance == null) return;

		numberLocationLogs = Aggregator.instance.GetNumberLocationLogs();
		wordleLogs = Aggregator.instance.GetWordleLogs();

		// Number Location
		NumberLocationMinigame.minigameTimes = numberLocationLogs.Count;
		NumberLocationMinigame.minigameScore = 0;
		foreach (KeyValuePair<string, NumberLocationAggregate> entry in numberLocationLogs)
		{
			NumberLocationMinigame.minigameScore += entry.Value.totalPoints;
		}

		// Wordle
		WordleMinigame.minigameTimes = wordleLogs.Count;
		WordleMinigame.minigameScore = 0;
		foreach (KeyValuePair<string, WordleAggregate> entry in wordleLogs)
		{
			WordleMinigame.minigameScore += entry.Value.totalPoints;
		}
	}
}

public class PlayedMinigame
{
	public int minigameRow;
	public string minigameName;
	public int minigameTimes;
	public float minigameScore;

	public PlayedMinigame(string name)
	{
		minigameName = name;
		minigameRow = 0;
		minigameTimes = 0;
		minigameScore = 0;
	}

	public void SetMinigame(int number, string name, int times, float score)
	{
		minigameRow = number;
		minigameName = name;
		minigameTimes = times;
		minigameScore = score;
	}
}
