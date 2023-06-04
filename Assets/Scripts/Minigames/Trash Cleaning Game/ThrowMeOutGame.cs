using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowMeOutGame : MonoBehaviour
{
	public GameObject Backdrop;
	public GameObject itemPrefab;
	public Animator trashCan;
	public Transform itemSpawnPoint;
	public Button isTrashButton;
	public Button isNotTrashButton;
	public Text scoreText;
	public Text timerText;
	public ThrowMeOutLivesManager livesManager;
	public ThrowMeOutItem currentItem;
	public int lastSpriteIndex = 0;
	public int lastClickedButton = 0;
	public int score = 0;
	public float TotalSeconds = 30f;
	public float currentTimer = 1f;
	public bool isGameRunning = false;
	public BackgroundColorDisplay backgroundColorDisplay;
	public GameObject startingSceneGameObject;
	public GameObject topPanelGameObject;
	public GameObject contentPanelGameObject;
	public GameObject winLosePanelGameObject;

	void Start()
	{
		// spawn first item

		isTrashButton.onClick.AddListener(IsTrashButtonClicked);
		isNotTrashButton.onClick.AddListener(IsNotTrashButtonClicked);
	}

	void Update()
	{
		scoreText.text = NumberFormatter.FormatNumberWithThousandsSeparator(score);
		timerText.text = TotalSeconds.ToString("F0") + "s";

		if (!isGameRunning) return;

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
				// AffirmationManager.instance.ScheduleRandomAffirmation();
			}
			else
			{
				currentTimer = 1f;
			}
		}
	}

	void ShowWinLosePanel()
	{
		StartCoroutine(ShowWinLosePanelWithDelay());
	}

	IEnumerator ShowWinLosePanelWithDelay()
	{
		yield return new WaitForSeconds(2);
		TakeMeOutWinLosePanel winLosePanel = winLosePanelGameObject.GetComponent<TakeMeOutWinLosePanel>();
		winLosePanel.isWin = score == 0 || livesManager.lives == 0 ? false : true;
		winLosePanel.score = (int)score;
		winLosePanel.highscore = (int)HighScoreStorage.GetHighScore("CleanItUpHighScore");
		winLosePanelGameObject.SetActive(true);
	}

	void UpdateStatistics()
	{
		if (score == 0 || livesManager.lives == 0)
		{
			return;
		}

		TakeMeOutCompletedEvent takeMeOutCompletedEvent = new TakeMeOutCompletedEvent(
			"Won Clean It Up Game",
			(int)score
		);

		Aggregator.instance.Publish(takeMeOutCompletedEvent);
	}

	void UpdateChoreManager()
	{
		if (score == 0 || livesManager.lives == 0)
		{
			return;
		}

		if (score <= 0 && livesManager.lives != 0)
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

	IEnumerator SpawnAnItem()
	{
		if (currentItem != null)
		{
			if (lastClickedButton == 0)
			{
				currentItem.NoThrow();
			}
			else if (lastClickedButton == 1)
			{
				currentItem.DestroyItem();
			}
		}

		currentItem = null;
		GameObject item = Instantiate(itemPrefab, itemSpawnPoint);
		item.transform.SetSiblingIndex(0);
		currentItem = item.GetComponent<ThrowMeOutItem>();
		currentItem.game = this;
		currentItem.isTrash = Random.Range(0, 2) == 0;
		AudioManager.instance.PlaySFX("ButtonClick");
		yield return null;
	}

	void IsTrashButtonClicked()
	{
		if(!isGameRunning) return;

		lastClickedButton = 1;

		if (currentItem == null) return;

		CheckItem();

		currentItem.DestroyItem();
		StartCoroutine(delaySpawn());
	}

	void IsNotTrashButtonClicked()
	{
		if(!isGameRunning) return;

		lastClickedButton = 0;

		if (currentItem == null) return;

		CheckItem();

		Debug.Log("IsNotTrashButtonClicked");
		currentItem.NoThrow();
		StartCoroutine(delaySpawn());
	}

	void CheckItem()
	{
		if (currentItem == null) return;

		if (currentItem.isTrash)
		{
			if (lastClickedButton == 0)
			{
				livesManager.lives--;
				AudioManager.instance.PlaySFX("WrongSfx");
				StartCoroutine(showFail());
			}
			else
			{
				score++;
				AudioManager.instance.PlaySFX("WinSfx");
				StartCoroutine(showSuccess());
				trashCan.SetTrigger("right");
			}
		}
		else
		{
			if (lastClickedButton == 1)
			{
				livesManager.lives--;
				AudioManager.instance.PlaySFX("WrongSfx");
				StartCoroutine(showFail());
				trashCan.SetTrigger("wrong");
			}
			else
			{
				score++;
				AudioManager.instance.PlaySFX("WinSfx");
				StartCoroutine(showSuccess());
			}
		}

		if(livesManager.lives == 0)
		{
			TotalSeconds = 1;
		}
	}

	public void StartTheGame()
	{
		StartCoroutine(StartGameWithStartScene());
	}

	internal void StartGame()
	{
		Backdrop.SetActive(false);
		livesManager.lives = 5;
		topPanelGameObject.SetActive(true);
		contentPanelGameObject.SetActive(true);
		isTrashButton.onClick.AddListener(IsTrashButtonClicked);
		isNotTrashButton.onClick.AddListener(IsNotTrashButtonClicked);
		isGameRunning = true;
		score = 0;
		TotalSeconds = 30;
		StartCoroutine(SpawnAnItem());
	}

	void StopTheGame()
	{
		isGameRunning = false;
		currentItem.DestroyItem();
		currentItem = null;

		HighScoreStorage.SaveHighScore("CleanItUpHighScore", (long)score);
	}

	IEnumerator StartGameWithStartScene()
	{
		startingSceneGameObject.SetActive(true);
		StartingScene startingScene = startingSceneGameObject.GetComponent<StartingScene>();
		startingScene.TotalSeconds = 3;
		startingScene.OnTimerEnd = () => StartGame();
		yield return new WaitForSeconds(3);
		startingSceneGameObject.SetActive(false);
		StartGame();
	}

	IEnumerator showSuccess()
	{
		backgroundColorDisplay.SetWinColor();
		yield return new WaitForSeconds(0.3f);
		backgroundColorDisplay.SetDefaultColor();
	}

	IEnumerator showFail()
	{
		backgroundColorDisplay.SetLoseColor();
		yield return new WaitForSeconds(0.3f);
		backgroundColorDisplay.SetDefaultColor();
	}

	IEnumerator delaySpawn()
	{
		yield return new WaitForSeconds(0.3f);
		StartCoroutine(SpawnAnItem());
	}
}
