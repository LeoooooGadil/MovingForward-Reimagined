using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustMeOffGame : MonoBehaviour
{
	public GameObject furnitureItemPrefab;
	public GameObject pointsAfterDeathPrefab;
	public GameObject pointsAtScore;
	public List<SpawnArea> spawnAreas;
	public Text pointsText;
	public Text timerText;

	public GameObject winLosePanelGameObject;
	public GameObject startingSceneGameObject;
	public GameObject topPanelGameObject;

	public float spawnRate = 5f;

	public float TotalPoints = 0;
	public int TotalFurniture = 0;

	public int TotalSeconds = 60;
	public float currentTimer = 1f;

	private bool isGameRunning = true;

	private float spawnRateNegative = 0.05f;
	public GameObject scoreLocation;

	private List<FurnitureItem> spawnedFurnitureItems = new List<FurnitureItem>();

	void Update()
	{
		pointsText.text = TotalPoints.ToString("F0");
		timerText.text = TotalSeconds.ToString("F0") + "s";

		if (!isGameRunning)
		{
			return;
		}

		currentTimer -= Time.deltaTime;

		if (currentTimer < 0)
		{
			TotalSeconds--;

			if (TotalSeconds <= 0)
			{
				isGameRunning = false;
				StopTheGame();
				UpdateChoreManager();
				UpdateStatistics();
				ShowWinLosePanel();
			}
			currentTimer = 1f;
		}

	}

	void UpdateStatistics()
	{
		DustMeOffCompletedEvent dustMeOffCompletedEvent = new DustMeOffCompletedEvent(
			"Won Dust Me Off Game",
			(int)TotalPoints
		);

		Aggregator.instance.Publish(dustMeOffCompletedEvent);
	}

	void UpdateChoreManager()
	{
		if (TotalPoints <= 0)
		{
			ChoresManager.instance.RemoveChore();
		}

		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore.dailyChoreType == DailyChoreType.DustMeOff)
		{
			ChoresManager.instance.CompleteChore(chore);
		}
		else
		{
			chore = ChoresManager.instance.FindChore(DailyChoreRoom.None, DailyChoreType.DustMeOff);

			if (chore != null)
			{
				ChoresManager.instance.CompleteChore(chore);
			}
		}

	}

	void StopTheGame()
	{
		StopAllCoroutines();
		foreach (FurnitureItem furnitureItem in spawnedFurnitureItems)
		{
			furnitureItem.StopThisObject();
		}
		spawnedFurnitureItems.Clear();

		HighScoreStorage.SaveHighScore("DustMeOffHighScore", (long)TotalPoints);
	}

	public void ResetTheGame()
	{
		StopAllCoroutines();
		foreach (FurnitureItem furnitureItem in spawnedFurnitureItems)
		{
			Destroy(furnitureItem.gameObject);
		}
		spawnedFurnitureItems.Clear();
		topPanelGameObject.SetActive(false);
		isGameRunning = false;
		TotalFurniture = 0;
		TotalPoints = 0;
		TotalSeconds = 60;
	}

	void ShowWinLosePanel()
	{
		StartCoroutine(ShowWinLosePanelWithDelay());
	}

	IEnumerator ShowWinLosePanelWithDelay()
	{
		yield return new WaitForSeconds(2);
		DustMeOffWinLosePanel winLosePanel = winLosePanelGameObject.GetComponent<DustMeOffWinLosePanel>();
		winLosePanel.isWin = TotalPoints == 0 ? false : true;
		winLosePanel.score = (int)TotalPoints;
		winLosePanel.highscore = (int)HighScoreStorage.GetHighScore("DustMeOffHighScore");
		winLosePanelGameObject.SetActive(true);
	}

	public void CleanedFurniture(Vector3 position, float points)
	{
		Debug.Log("Cleaned Furniture");
		TotalPoints += points;
		TotalFurniture++;
		SpawnPointsAfterDeath(position, points);
		SpawnAtScore(points);
	}

	public void UncleanedFurniture(Vector3 position, float points)
	{
		Debug.Log("Uncleaned Furniture");
		TotalPoints -= points;
		SpawnPointsAfterDeath(position, -points);
		SpawnAtScore(-points);
	}

	public void SpawnPointsAfterDeath(Vector3 position, float points)
	{
		GameObject pointsAfterDeath = Instantiate(pointsAfterDeathPrefab, position, Quaternion.identity);
		pointsAfterDeath.GetComponent<PointsAfterDeath>().points = points;
	}

	public void SpawnAtScore(float points)
	{
		GameObject pointsAfterDeath = Instantiate(pointsAtScore, scoreLocation.transform.position, Quaternion.identity);
		pointsAfterDeath.GetComponent<PointsAfterDeath>().points = points;
	}

	public void AddFurnitureItem(FurnitureItem furnitureItem)
	{
		spawnedFurnitureItems.Add(furnitureItem);
	}

	public void RemoveFurnitureItem(FurnitureItem furnitureItem)
	{
		spawnedFurnitureItems.Remove(furnitureItem);
	}

	IEnumerator SpawnFurniture()
	{
		while (true)
		{
			Debug.Log("Spawning Furniture");
			int area = Random.Range(0, spawnAreas.Count);
			SpawnArea randomSpawnArea = spawnAreas[area];
			FurnitureItem spawnedFurnitureItem = randomSpawnArea.Spawn(furnitureItemPrefab, gameObject);
			spawnedFurnitureItem.dustMeOffGame = this;
			spawnedFurnitureItem.whereDidSpawn = area;
			yield return new WaitForSeconds(spawnRate);
		}
	}

	IEnumerator StartGameWithStartingScene()
	{
		startingSceneGameObject.SetActive(true);
		StartingScene startingScene = startingSceneGameObject.GetComponent<StartingScene>();
		startingScene.TotalSeconds = 3;
		startingScene.OnTimerEnd = StartGame;
		yield return new WaitForSeconds(3);
		startingSceneGameObject.SetActive(false);
		StartGame();
	}

	public void NegateSpawnRate()
	{
		// if the TotalSeconds is less than 30, then the spawn rate will be decreased by 0.1f
		if (TotalSeconds > 30)
		{
			spawnRate -= spawnRateNegative;
		}
		else
		{
			spawnRate -= spawnRateNegative * 2;
		}
	}

	internal void StartTheGame()
	{
		StartCoroutine(StartGameWithStartingScene());
	}

	internal void StartGame()
	{
		topPanelGameObject.SetActive(true);
		isGameRunning = true;
		TotalFurniture = 0;
		TotalPoints = 0;
		TotalSeconds = 60;
		StartCoroutine(SpawnFurniture());
	}

	internal void StopGame()
	{
		isGameRunning = false;
		StopTheGame();
		StopAllCoroutines();
	}
}
