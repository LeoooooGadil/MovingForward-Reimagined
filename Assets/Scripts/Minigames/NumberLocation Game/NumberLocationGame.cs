using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationGame : MonoBehaviour
{
	public GameObject MinigameUI;

	public TMP_Text numberOfTriesText;
	public TMP_Text numberOfWinsText;
	public TMP_Text numberOfLossesText;


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

	public float howLongToSeeAllNumbers = 3f;

	private int numberOfTimesLost = 0;
	private int numberOfTimesWon = 0;
	private int numberOfTimesPlayed = 0;

	public Image ContainerImage;

	public void Start()
	{
		state = 0;
		ResetTablets();
	}

	void Update()
	{

#if UNITY_EDITOR || UNITY_STANDALONE
		if (Input.GetKeyDown(KeyCode.Space) && state == 0)
		{
			DifficultyManager.SetDifficulty(difficulty);
			howLongToSeeAllNumbers = DifficultyManager.GetDefaultHowLongToNumbersSee();
			StartCoroutine(StartGame());
		}
#endif

		numberOfTriesText.text = howLongToSeeAllNumbers.ToString("F2");
		numberOfWinsText.text = numberOfTimesWon + " Wins";
		numberOfLossesText.text = numberOfTimesLost + " Loss";
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
		}
	}

	NumberTablet FindAppropriateTablet()
	{
		int randomIndex = Random.Range(0, Tablets.Count);
		NumberTablet tablet = Tablets[randomIndex];

		bool isLeftNeighbourOccupied = false;
		bool isRightNeighbourOccupied = false;

		if (tablet.isOccupied)
		{
			return FindAppropriateTablet();
		}

		if (difficulty == NumberLocationDifficulty.Difficulty.Easy)
		{
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

			return tablet;
		}
		else
		{
			return tablet;
		}
	}

	public bool OnTabletClicked(NumberTablet tablet)
	{
		// get the index of the tablet that was clicked
		int index = Tablets.IndexOf(tablet);

		// check if the number on the tablet is the first number in the list of numbers to find
		if (NotFoundNumbers[0] == tablet.number)
		{
			// if it is, remove the number from the list
			NotFoundNumbers = NotFoundNumbers[1..];
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

	public void StartTheGame()
	{
		StartCoroutine(StartFlow());
	}

	IEnumerator StartFlow()
	{
		yield return new WaitForSeconds(1);
		DifficultyManager.SetDifficulty(difficulty);
		howLongToSeeAllNumbers = DifficultyManager.GetDefaultHowLongToNumbersSee();
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
		AudioManager.instance.PlaySFX("PopClick");
		HideAllNumbers();
		ShowAllNumbers();
		AudioManager.instance.PlaySFX("PopClick");
		StartCoroutine(AnimateBorder(howLongToSeeAllNumbers));
		for (int i = 0; i < howLongToSeeAllNumbers; i++)
		{
			yield return new WaitForSeconds(0.5f);
			AudioManager.instance.PlaySFX("PloukSfx");
			yield return new WaitForSeconds(0.5f);
		}
		AudioManager.instance.PlaySFX("PopClick");
		HideAllOccupiedNumbers();
		numberOfTimesPlayed++;
		state = 2;

		howLongToSeeAllNumbers = DifficultyManager.GetNewHowLongToNumbersSee(howLongToSeeAllNumbers);
	}

	IEnumerator RestartGame()
	{
		state = 1;
		yield return new WaitForSeconds(1);
		ResetTablets();
		StartCoroutine(StartGame());
	}

	IEnumerator LoseGame()
	{
		state = 1;
		yield return new WaitForSeconds(2);
		ResetTablets();
		StartCoroutine(RestartGame());
	}

	IEnumerator WinGame()
	{
		state = 1;
		foreach (var tablet in TabletContainingNumber)
		{
			tablet.WinGameAnimation();
		}
		yield return new WaitForSeconds(3);
		ResetTablets();
		StartCoroutine(RestartGame());
	}
}

public class NumberLocationDifficulty
{
	public enum Difficulty { Easy, Medium, Hard };
	public Difficulty difficulty;

	private int[] EasyNumbers = { 1, 2, 3, 4, 5 };
	private int[] MediumNumbers = { 1, 2, 3, 4, 5, 6, 7, 8 };
	private int[] HardNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

	public float howLongToSeeAllNumbersEasy = 3f;
	public float howLongToSeeAllNumbersMedium = 5f;
	public float howLongToSeeAllNumbersHard = 7f;

	public float howLongToSeeAllNumbersEasyMinus = 0.1f;
	public float howLongToSeeAllNumbersMediumMinus = 0.2f;
	public float howLongToSeeAllNumbersHardMinus = 0.3f;

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
			default:
				return howLongToSeeAllNumbersEasy;
		}
	}
}
