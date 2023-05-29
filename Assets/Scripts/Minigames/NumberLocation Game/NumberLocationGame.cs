using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationGame : MonoBehaviour
{
	public GameObject MinigameUI;
	public GameObject MinigameWinLosePanel;
	public GameObject MinigameHintPanel;

	public NumberLocationLookTimeManager lookTimeManager;
	public NumberLocationRoundsManager roundsManager;
	public NumberLocationLivesManager livesManager;
	public TMP_Text difficultyText;

	private NumberLocationWinLosePanel numberLocationWinLosePanel;

	public List<NumberTablet> Tablets;
	public List<int> Number;
	NumberLocationDifficulty DifficultyManager = new NumberLocationDifficulty();
	public NumberLocationDifficulty.Difficulty difficulty;

	public int state = 0;

	// states 
	// 0 - not started
	// 1 - started not interactable
	// 2 - started interactable
	// 3 - win
	// 4 - lose
	// 5 - finished

	public int[] NotFoundNumbers;
	public List<NumberTablet> TabletContainingNumber = new List<NumberTablet>();
	public List<NumberTablet> TabletRemaining = new List<NumberTablet>();

	public float howLongToSeeAllNumbers = 3f;

	private int numberOfTimesLost = 0;
	private int numberOfTimesWon = 0;
	private int numberOfTimesPlayed = 0;

	private int livesLeft = 0;
	private int maxShowNumberHint = 3;

	public Image ContainerImage;

	public void Start()
	{
		state = 0;
		MinigameWinLosePanel.SetActive(false);
		numberLocationWinLosePanel = MinigameWinLosePanel.GetComponent<NumberLocationWinLosePanel>();
		ResetTablets();
	}

	void Update()
	{
		// set the difficulty text to the current difficulty in uppercase
		setDifficultyText();
		setLookTimeText();
		setWinText();
	}

	void setWinText()
	{
		roundsManager.round = numberOfTimesWon;
		roundsManager.maxRounds = DifficultyManager.howManyWinsToPass;
	}

	void setLookTimeText()
	{
		lookTimeManager.lookTime = howLongToSeeAllNumbers;
	}

	void setDifficultyText()
	{
		switch (difficulty)
		{
			case NumberLocationDifficulty.Difficulty.Easy:
				// #ffb142
				difficultyText.color = new Color(1f, 0.698f, 0.258f);
				difficultyText.text = "EASY";
				break;
			case NumberLocationDifficulty.Difficulty.Medium:
				// #ff793f
				difficultyText.color = new Color(1f, 0.474f, 0.247f);
				difficultyText.text = "MEDIUM";
				break;
			case NumberLocationDifficulty.Difficulty.Hard:
				// #ff5252
				difficultyText.color = new Color(1f, 0.322f, 0.322f);
				difficultyText.text = "HARD";
				break;
		}
	}

	void GenerateNumbers()
	{
		Number = new List<int>();
		int[] numbers = DifficultyManager.getNumbers();
		for (int i = 0; i < numbers.Length; i++)
		{
			Number.Add(numbers[i]);
		}

		NotFoundNumbers = new int[Number.Count];
		Number.CopyTo(NotFoundNumbers);

		for (int i = 0; i < Number.Count; i++)
		{
			int temp = Number[i];
			int randomIndex = Random.Range(i, Number.Count);
			Number[i] = Number[randomIndex];
			Number[randomIndex] = temp;
		}

		Debug.Log("Numbers to find: " + string.Join(", ", NotFoundNumbers));
		Debug.Log("Generated numbers: " + string.Join(", ", Number.ToArray()));
	}

	public void ResetGame()
	{
		state = 0;
		ResetTablets();
		numberOfTimesLost = 0;
		numberOfTimesWon = 0;
		numberOfTimesPlayed = 0;
		ContainerImage.fillAmount = 1f;
		StopAllCoroutines();
	}

	void ResetTablets()
	{
		foreach (var tablet in Tablets)
		{
			tablet.Reset();
			tablet.numberLocationGame = this;
		}

		TabletContainingNumber.Clear();
	}

	// this method causes stack overflow when the difficulty is medium or hard
	void PlaceNumbers()
	{
		for (int i = 0; i < Number.Count; i++)
		{
			NumberTablet tablet = FindAppropriateTablet();
			tablet.SetNumber(Number[i]);
			TabletContainingNumber.Add(tablet);
			TabletRemaining.Add(tablet);
		}

		if (numberOfTimesWon > 5 && Random.Range(0, 100) < 75)
		{
			PlaceNuisanceTablets();
		}
	}

	// this method is used to find a tablet that is not occupied and is not a neighbour of another occupied tablet (if the difficulty is easy)
	// we cant check the neighbors if the numbers needed to place is above 6 cause it will cause stack overflow (because of recursion)
	// now that easy and medium are the same, we can check the neighbors if the difficulty is medium.
	// your task is to review the code and make it so that it checks the neighbors if the difficulty is easy and medium.
	// right now it only checks the neighbors if the difficulty is easy.
	NumberTablet FindAppropriateTablet()
	{
		// get a random tablet
		int randomIndex = Random.Range(0, Tablets.Count);
		NumberTablet tablet = Tablets[randomIndex];

		// initialize the variables
		bool isLeftNeighbourOccupied = false;
		bool isRightNeighbourOccupied = false;

		// check if the tablet is occupied
		if (tablet.isOccupied)
		{
			return FindAppropriateTablet();
		}

		if (difficulty == NumberLocationDifficulty.Difficulty.Easy || difficulty == NumberLocationDifficulty.Difficulty.Medium)
		{
			// check if the tablet is a neighbour of another occupied tablet
			if (randomIndex - 1 >= 0)
			{
				isLeftNeighbourOccupied = Tablets[randomIndex - 1].isOccupied;
			}

			if (randomIndex + 1 < Tablets.Count)
			{
				isRightNeighbourOccupied = Tablets[randomIndex + 1].isOccupied;
			}

			if (isLeftNeighbourOccupied || isRightNeighbourOccupied)
			{
				return FindAppropriateTablet();
			}

			// if the tablet is not occupied and is not a neighbour of another occupied tablet, return it
			return tablet;
		}
		else
		{
			// if the difficulty is medium or hard, return the tablet
			return tablet;
		}
	}

	void PlaceNuisanceTablets()
	{
		// get the number of nuisance tablets to place
		int numberOfNuisanceTablets = DifficultyManager.GetNumberOfNuisanceTablets();

		// place the nuisance tablets
		for (int i = 0; i < numberOfNuisanceTablets; i++)
		{
			NumberTablet tablet = FindAppropriateTablet();
			tablet.SetNumber(0);
			TabletContainingNumber.Add(tablet);
			TabletRemaining.Add(tablet);
		}
	}

	public bool OnTabletClicked(NumberTablet tablet)
	{
		Debug.Log("Clicked on tablet " + tablet.number);
		// get the index of the tablet that was clicked
		int index = Tablets.IndexOf(tablet);

		// check if the number on the tablet is the first number in the list of numbers to find
		if (NotFoundNumbers[0] == tablet.number)
		{
			// if it is, remove the number from the list
			NotFoundNumbers = NotFoundNumbers[1..];
			TabletRemaining.Remove(tablet);
			// check if the list is empty
			if (NotFoundNumbers.Length == 0)
			{
				// if it is, the player won
				state = 3;
				AudioManager.instance.PlaySFX("WinSfx");
				StartCoroutine(WinGame());
				numberOfTimesWon++;
				return true;
			}
			else
			{
				// if it isn't, the player found a number
				AudioManager.instance.PlaySFX("PopClick");
				return true;
			}
		}
		else
		{
			// if it isn't, the player clicked the wrong number
			AudioManager.instance.PlaySFX("WrongSfx");
			numberOfTimesLost++;
			livesLeft--;
			// check if the player has any lives left if not, the player lost
			StartCoroutine(LoseGame());
			return false;
		}
	}

	void ShowAllNumbers()
	{
		foreach (var tablet in Tablets)
		{
			tablet.Cover.SetActive(false);
			tablet.isCovered = false;
		}
	}

	void HideAllNumbers()
	{
		foreach (var tablet in Tablets)
		{
			tablet.Cover.SetActive(true);
			tablet.isCovered = true;
		}
	}

	void HideAllOccupiedNumbers()
	{
		foreach (var tablet in Tablets)
		{
			if (tablet.isOccupied)
			{
				tablet.Cover.SetActive(true);
				tablet.isCovered = true;
			}
			else
			{
				tablet.isInteractable = false;
			}
		}
	}

	void HideAllRemainingNumbers()
	{
		foreach (var tablet in TabletRemaining)
		{
			if (tablet.isOccupied)
			{
				tablet.Cover.SetActive(true);
				tablet.isCovered = true;
			}
			else
			{
				tablet.isInteractable = false;
			}
		}
	}

	void UpdateStatistics()
	{
		// easy is 75
		// medium is 100
		// hard is 150

		float points = 0;
		switch (difficulty)
		{
			case NumberLocationDifficulty.Difficulty.Easy:
				points = 75;
				break;
			case NumberLocationDifficulty.Difficulty.Medium:
				points = 100;
				break;
			case NumberLocationDifficulty.Difficulty.Hard:
				points = 150;
				break;
		}

		ExperienceManager.instance.AddExperience(points);


		NumberLocationCompletedEvent numberLocationCompletedEvent = new NumberLocationCompletedEvent(
			"Won Number Location Minigame",
			livesLeft,
			difficulty,
			numberOfTimesWon,
			points
		);

		Aggregator.instance.Publish(numberLocationCompletedEvent);
	}

	void UpdateChoreManager()
	{
		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore != null && chore.dailyChoreType == DailyChoreType.NumberPlacement)
		{
			ChoresManager.instance.CompleteChore(chore);
		}
		else
		{
			chore = ChoresManager.instance.FindChore(DailyChoreRoom.None, DailyChoreType.NumberPlacement);

			if (chore != null)
			{
				ChoresManager.instance.CompleteChore(chore);
			}
		}

	}

	void CompensatePlayer()
	{
		ProfileManager.instance.AddMoney(25);
	}

	public void StartTheGame()
	{
		StartCoroutine(StartFlow());
	}

	void CreateNewLifeCycle()
	{
		LifeCycleItem lifeCycleItem = new LifeCycleItem();
		lifeCycleItem.name = "NumberLocation";
		lifeCycleItem.startTime = System.DateTime.Now + System.TimeSpan.FromHours(4f);
		lifeCycleItem.maxRepeatCount = -1;
		lifeCycleItem.repeatType = LifeCycleRepeatType.Custom;
		lifeCycleItem.customRepeatTime = System.TimeSpan.FromHours(4f).Seconds;

		LifeCycleManager.instance.AddLifeCycleItem(lifeCycleItem);

		NotificationManager.instance.SendNotification(
			"Moving Forward",
			"Number Location is ready to play again!",
			System.DateTime.Now + System.TimeSpan.FromHours(4f));
	}

	IEnumerator StartFlow()
	{
		livesLeft = DifficultyManager.GetDefaultLives();
		livesManager.lives = livesLeft;
		livesManager.maxLives = livesLeft;
		DifficultyManager.SetDifficulty(difficulty);
		howLongToSeeAllNumbers = DifficultyManager.GetDefaultHowLongToNumbersSee();
		yield return new WaitForSeconds(1);
		StartCoroutine(StartGame());
		yield return null;
	}

	IEnumerator AnimateBorder(float howLong)
	{
		float time = 0;
		while (time < howLong)
		{
			ContainerImage.fillAmount = Mathf.Lerp(1, 0, time / howLong);
			time += Time.deltaTime;
			// every 1 second pass, play a sound
			if (time % 1 < Time.deltaTime)
			{
				AudioManager.instance.PlaySFX("TimerTickSfx");
			}
			yield return null;
		}
		ContainerImage.fillAmount = 0;
	}

	IEnumerator StartGame()
	{
		DifficultyManager.SetDifficulty(difficulty);
		ContainerImage.fillAmount = 1;
		state = 1;
		ResetTablets();
		GenerateNumbers();
		PlaceNumbers();
		HideAllNumbers();
		ShowAllNumbers();
		MinigameHintPanel.SetActive(false);
		AudioManager.instance.PlaySFX("TimerTickSfx");
		StartCoroutine(AnimateBorder(howLongToSeeAllNumbers));
		yield return new WaitForSeconds(howLongToSeeAllNumbers);
		HideAllOccupiedNumbers();
		MinigameHintPanel.SetActive(true);
		state = 2;
		yield return null;
	}

	IEnumerator RestartGame()
	{
		state = 1;
		yield return new WaitForSeconds(1);
		ResetTablets();
		howLongToSeeAllNumbers = DifficultyManager.GetNewHowLongToNumbersSee(howLongToSeeAllNumbers);
		numberOfTimesPlayed++;
		StartCoroutine(StartGame());
	}

	IEnumerator LoseGame()
	{
		state = 1;
		MinigameHintPanel.SetActive(false);
		yield return new WaitForSeconds(2);
		if (livesLeft <= 0)
		{
			StartCoroutine(TotallyLoseGame());
			yield break;
		}
		ResetTablets();
		livesManager.lives = livesLeft;
		StartCoroutine(RestartGame());
	}

	IEnumerator WinGame()
	{
		MinigameHintPanel.SetActive(false);
		state = 1;
		foreach (var tablet in TabletContainingNumber)
		{
			tablet.WinGameAnimation();
		}
		yield return new WaitForSeconds(3);
		if (numberOfTimesWon >= DifficultyManager.howManyWinsToPass)
		{
			StartCoroutine(TotallyWinGame());
			yield break;
		}
		ResetTablets();
		StartCoroutine(RestartGame());
	}

	IEnumerator TotallyWinGame()
	{
		float points = 0;
		switch (difficulty)
		{
			case NumberLocationDifficulty.Difficulty.Easy:
				points = 75;
				break;
			case NumberLocationDifficulty.Difficulty.Medium:
				points = 100;
				break;
			case NumberLocationDifficulty.Difficulty.Hard:
				points = 150;
				break;
			case NumberLocationDifficulty.Difficulty.Impossible:
				points = 200;
				break;
		}

		state = 1;
		numberLocationWinLosePanel.isWin = true;
		numberLocationWinLosePanel.score = (int)points;
		numberLocationWinLosePanel.difficulty = difficulty;
		MinigameWinLosePanel.SetActive(true);
		UpdateChoreManager();
		yield return new WaitForSeconds(1);
		UpdateStatistics();
		CompensatePlayer();
		AffirmationManager.instance.ScheduleRandomAffirmation();

		TicketAccess.RemoveOneFromTicket("NumberLocation");
		int ticketCount = TicketAccess.GetTicketCount("NumberLocation");
		if (ticketCount == 0) CreateNewLifeCycle();

		yield return null;
	}

	IEnumerator TotallyLoseGame()
	{
		state = 1;
		numberLocationWinLosePanel.isWin = false;
		numberLocationWinLosePanel.score = 0;
		numberLocationWinLosePanel.difficulty = difficulty;
		MinigameWinLosePanel.SetActive(true);
		yield return null;
	}

	IEnumerator LookAgainHint()
	{
		MinigameHintPanel.SetActive(false);
		state = 1;
		ShowAllNumbers();
		AudioManager.instance.PlaySFX("PopClick");
		StartCoroutine(AnimateBorder(howLongToSeeAllNumbers));
		yield return new WaitForSeconds(howLongToSeeAllNumbers);
		HideAllRemainingNumbers();
		state = 2;
		MinigameHintPanel.SetActive(true);
	}

	IEnumerator ShowMeTheNextNumber()
	{
		maxShowNumberHint--;


		if (maxShowNumberHint >= 0)
		{
			OnScreenNotificationManager.instance.CreateNotification(maxShowNumberHint + " Reveal Left", OnScreenNotificationType.Info);
		}
		else
		{
			OnScreenNotificationManager.instance.CreateNotification("No Reveal Left", OnScreenNotificationType.Info);
		}

		MinigameHintPanel.SetActive(false);
		state = 1;
		int nextNumber = NotFoundNumbers[0];
		Debug.Log("ShowMeTheNextNumber: " + nextNumber);
		foreach (var item in TabletContainingNumber)
		{
			if (item.number == nextNumber)
			{
				item.OnClick();
				TabletRemaining.Remove(item);
				break;
			}
		}

		AudioManager.instance.PlaySFX("PopClick");
		yield return new WaitForSeconds(1);
		state = 2;
		MinigameHintPanel.SetActive(true);
	}

	internal bool ActivateHint(NumberLocationHints hintType)
	{
		Debug.Log("ActivateHint: " + hintType);

		switch (hintType)
		{
			case NumberLocationHints.None:
				return false;
			case NumberLocationHints.ShowMeTheNextNumber:
				if (maxShowNumberHint <= 0)
				{
					OnScreenNotificationManager.instance.CreateNotification("No more hints left!", OnScreenNotificationType.Error);
					return false;
				}

				StartCoroutine(ShowMeTheNextNumber());
				return true;
			case NumberLocationHints.LookAgain:
				StartCoroutine(LookAgainHint());
				return true;
			default:
				return false;
		}
	}
}

public enum NumberLocationHints { None, ShowMeTheNextNumber, LookAgain };

public class NumberLocationDifficulty
{
	public enum Difficulty { Easy, Medium, Hard, Impossible };
	public Difficulty difficulty;

	private int[] EasyNumbers = { 1, 2, 3, 4, 5 };
	private int[] MediumNumbers = { 1, 2, 3, 4, 5 };
	private int[] HardNumbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
	private int[] ImpossibleNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };

	public float howLongToSeeAllNumbersEasy = 3f;
	public float howLongToSeeAllNumbersMedium = 3f;
	public float howLongToSeeAllNumbersHard = 5f;
	public float howLongToSeeAllNumbersImpossible = 5f;

	public float howLongToSeeAllNumbersEasyMinus = 0.2f;
	public float howLongToSeeAllNumbersMediumMinus = 0.3f;
	public float howLongToSeeAllNumbersHardMinus = 0.4f;
	public float howLongToSeeAllNumbersImpossibleMinus = 0.5f;

	public int lives = 3;

	public int howManyWinsToPass = 10;

	public int[] getNumbers()
	{
		switch (difficulty)
		{
			case Difficulty.Easy:
				return EasyNumbers;
			case Difficulty.Medium:
				return MediumNumbers;
			case Difficulty.Hard:
				return HardNumbers;
			case Difficulty.Impossible:
				return ImpossibleNumbers;
			default:
				return EasyNumbers;
		}
	}

	// get the difficulty from the dropdown
	public void SetDifficulty(Difficulty value)
	{
		difficulty = value;
	}

	public float GetHowLongMinus()
	{
		switch (difficulty)
		{
			case Difficulty.Easy:
				return howLongToSeeAllNumbersEasyMinus;
			case Difficulty.Medium:
				return howLongToSeeAllNumbersMediumMinus;
			case Difficulty.Hard:
				return howLongToSeeAllNumbersHardMinus;
			case Difficulty.Impossible:
				return howLongToSeeAllNumbersImpossibleMinus;
			default:
				return howLongToSeeAllNumbersEasyMinus;
		}
	}

	public float GetNewHowLongToNumbersSee(float value)
	{
		if (value <= 0.5f)
			return 0.5f;

		switch (difficulty)
		{
			case Difficulty.Easy:
				return value - howLongToSeeAllNumbersEasyMinus;
			case Difficulty.Medium:
				return value - howLongToSeeAllNumbersMediumMinus;
			case Difficulty.Hard:
				return value - howLongToSeeAllNumbersHardMinus;
			case Difficulty.Impossible:
				return value - howLongToSeeAllNumbersImpossibleMinus;
			default:
				return value - howLongToSeeAllNumbersEasyMinus;
		}
	}

	public float GetDefaultHowLongToNumbersSee()
	{
		switch (difficulty)
		{
			case Difficulty.Easy:
				return howLongToSeeAllNumbersEasy;
			case Difficulty.Medium:
				return howLongToSeeAllNumbersMedium;
			case Difficulty.Hard:
				return howLongToSeeAllNumbersHard;
			case Difficulty.Impossible:
				return howLongToSeeAllNumbersImpossible;
			default:
				return howLongToSeeAllNumbersEasy;
		}
	}

	public int GetNumberOfNuisanceTablets()
	{
		switch (difficulty)
		{
			case Difficulty.Easy:
				return 0;
			case Difficulty.Medium:
				return 1;
			case Difficulty.Hard:
				return 2;
			case Difficulty.Impossible:
				return 0;
			default:
				return 0;
		}
	}

	public int GetDefaultLives()
	{
		return lives;
	}
}
