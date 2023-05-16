using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeMeOutGame : MonoBehaviour
{
	public TakeMeOutSpawnArea spawnArea;
	public GameObject trashPrefab;
	public Button reshuffleButton;
	public GameObject pointsAfterDeathPrefab;
	public GameObject startingSceneGameObject;
	public GameObject topPanelGameObject;

	public Sprite trashCanIndicator;
	public Sprite pileCanIndicator;

	public Image indicatorImageLeft;
	public Image indicatorImageRight;
	public GameObject winLosePanelGameObject;

	public Text timerText;
	public Text scorePoints;

	public float TotalPoints = 0;
	public int TotalSeconds = 60;
	public float currentTimer = 1f;
	public int howManyTrashItemsPerSpawn = 5;

	private bool isGameRunning = false;
	private BoxCollider2D trashArea;
	private Vector3 offset = new Vector3(0, 3, 0);
	private List<TrashItem> spawnedTrashItems = new List<TrashItem>();

	public bool isSwitched = false;


	void Start()
	{
		reshuffleButton.onClick.AddListener(ReshuffleTrash);
		setIndicators();
	}

	public void StartTheGame()
	{
		StartCoroutine(StartGameWithStartingScene());
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

	internal void StartGame()
	{
		topPanelGameObject.SetActive(true);
		isGameRunning = true;
		TotalPoints = 0;
		TotalSeconds = 60;
		StartCoroutine(SpawnItems());
	}

	internal void StopTheGame()
	{
		StopAllCoroutines();
		isGameRunning = false;
	}

	public void ResetTheGame()
	{
		StopAllCoroutines();
		isGameRunning = false;

		foreach (var trashItem in spawnedTrashItems)
		{
			Destroy(trashItem.gameObject);
		}
	}

	void Update()
	{
		scorePoints.text = NumberFormatter.FormatNumberWithThousandsSeparator(TotalPoints);
		timerText.text = TotalSeconds.ToString("F0") + "s";

		if (!isGameRunning) return;

		currentTimer -= Time.deltaTime;

		if (currentTimer < 0)
		{
			if (TotalSeconds > 0)
			{
				TotalSeconds--;
			}

			if (TotalSeconds <= 0)
			{
				if (spawnedTrashItems.Count == 0 && isGameRunning)
				{
					isGameRunning = false;
					StopTheGame();
					UpdateChoreManager();
					UpdateStatistics();
					ShowWinLosePanel();
				}
			}
			else if(isGameRunning)
			{
				CheckIfTrashListEmpty();
			}

			currentTimer = 1f;
		}
	}

	void UpdateStatistics()
	{
		TakeMeOutCompletedEvent takeMeOutCompletedEvent = new TakeMeOutCompletedEvent(
			"Won the Take Out The Trash game",
			(int)TotalPoints
		);

		Aggregator.instance.Publish(takeMeOutCompletedEvent);
	}

	void ShowWinLosePanel()
	{
		StartCoroutine(ShowWinLosePanelWithDelay());
	}

	IEnumerator ShowWinLosePanelWithDelay()
	{
		yield return new WaitForSeconds(2f);
		TakeMeOutWinLosePanel winLosePanel = winLosePanelGameObject.GetComponent<TakeMeOutWinLosePanel>();
		winLosePanel.isWin = TotalPoints == 0 ? false : true;
		winLosePanel.score = (int)TotalPoints;
		winLosePanel.highscore = (int)HighScoreStorage.GetHighScore("TakeMeOutHighScore");
		winLosePanelGameObject.SetActive(true);
	}

	void UpdateChoreManager()
	{
		if (TotalPoints <= 0)
		{
			ChoresManager.instance.RemoveChore();
		}

		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore != null && chore.dailyChoreType == DailyChoreType.ThrowMeOut)
		{
			ChoresManager.instance.CompleteChore(chore);
		}
		else
		{
			chore = ChoresManager.instance.FindChore(DailyChoreRoom.None, DailyChoreType.ThrowMeOut);

			if (chore != null)
			{
				ChoresManager.instance.CompleteChore(chore);
			}
		}

	}

	void CheckIfTrashListEmpty()
	{
		if (spawnedTrashItems.Count != 0) return;
		StartCoroutine(SpawnItems());
	}

	public void DunkedTrash(TrashItem trashItem)
	{
		spawnedTrashItems.Remove(trashItem);
		Debug.Log("DunkedTrash: " + trashItem.name);

	}

	public void ReshuffleTrash()
	{
		foreach (var trashItem in spawnedTrashItems)
		{
			trashItem.transform.position = spawnArea.GetRandomPosition();
		}
	}

	public void SwitchTrash()
	{
		// a variable that is the percentage

		float percentage = 0.2f;

		if (UnityEngine.Random.value < percentage)
		{
			isSwitched = !isSwitched;
			setIndicators();
		}
	}

	void setIndicators()
	{
		if (!isSwitched)
		{
			indicatorImageLeft.sprite = trashCanIndicator;
			indicatorImageRight.sprite = pileCanIndicator;
		}
		else
		{
			indicatorImageLeft.sprite = pileCanIndicator;
			indicatorImageRight.sprite = trashCanIndicator;
		}
	}

	IEnumerator ReshuffleTrashCoroutine()
	{
		foreach (var trashItem in spawnedTrashItems)
		{
			trashItem.gameObject.SetActive(false);
			trashItem.transform.position = spawnArea.GetRandomPosition();
			trashItem.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void AddPoints(float points)
	{
		TotalPoints += points;
	}

	public void RemovePoints(float points)
	{
		TotalPoints -= points;
	}

	public void SpawnAtScore(Vector3 position, float points)
	{
		GameObject pointsAfterDeath = Instantiate(pointsAfterDeathPrefab, position, Quaternion.identity);
		pointsAfterDeath.GetComponent<PointsAfterDeath>().points = points;
	}

	IEnumerator SpawnItems()
	{
		if (!isGameRunning) yield break;

		Debug.Log("SpawnItems");

		for (int i = 0; i < howManyTrashItemsPerSpawn; i++)
		{
			TrashItem spawnedItem = spawnArea.Spawn(trashPrefab, gameObject);
			spawnedItem.game = this;
			spawnedItem.itemType = (ItemType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length);
			spawnedTrashItems.Add(spawnedItem);
			// randomize the trash item game objects name
			spawnedItem.name = spawnedItem.name + UnityEngine.Random.Range(0, 1000);

			yield return new WaitForSeconds(0.1f);
		}

	}
}








