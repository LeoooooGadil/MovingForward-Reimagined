using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationUI : MonoBehaviour
{
	public GameObject TopPanel;
	public GameObject BackDrop;

	public Button PlayButton;
	public Text DifficultyText;
	public Text LookTimeText;
	public Text NumbersText;
	public Text ExperienceText;

	public NumberLocationGame numberLocationGame;
	public NumberLocationDifficulty numberLocationDifficulty = new NumberLocationDifficulty();

	public NumberLocationDifficulty.Difficulty difficulty = NumberLocationDifficulty.Difficulty.Easy;
	public DialogAnimator dialogAnimator;
	public BackDropLifeCycle backDropLifeCycle;

	public void Start()
	{
		dialogAnimator = GetComponent<DialogAnimator>();
		backDropLifeCycle = BackDrop.GetComponent<BackDropLifeCycle>();
		PlayButton.onClick.AddListener(PlayButtonClicked);

	}

	void OnEnable()
	{
		TopPanel.SetActive(false);
	}

	void OnDisable()
	{
		TopPanel.SetActive(true);
	}

	void Update()
	{
		setDifficultyText();
		setLookTimeText();
		setNumbersText();
		setExperienceText();
	}

	void setDifficultyText()
	{
		switch (difficulty)
		{
			case NumberLocationDifficulty.Difficulty.Easy:
				// #ffb142
				DifficultyText.color = new Color(1f, 0.698f, 0.258f);
				DifficultyText.text = "EASY";
				break;
			case NumberLocationDifficulty.Difficulty.Medium:
				// #ff793f
				DifficultyText.color = new Color(1f, 0.474f, 0.247f);
				DifficultyText.text = "MEDIUM";
				break;
			case NumberLocationDifficulty.Difficulty.Hard:
				// #ff5252
				DifficultyText.color = new Color(1f, 0.322f, 0.322f);
				DifficultyText.text = "HARD";
				break;
			case NumberLocationDifficulty.Difficulty.Impossible:
				// #b33939
				DifficultyText.color = new Color(0.702f, 0.223f, 0.223f);
				DifficultyText.text = "IMPOSSIBLE";
				break;
		}
	}

	void setExperienceText()
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

		ExperienceText.text = points.ToString() + " XP";
	}

	public int GetDifficultyByInt()
	{
		switch (difficulty)
		{
			case NumberLocationDifficulty.Difficulty.Easy:
				return 0;
			case NumberLocationDifficulty.Difficulty.Medium:
				return 1;
			case NumberLocationDifficulty.Difficulty.Hard:
				return 2;
			case NumberLocationDifficulty.Difficulty.Impossible:
				return 3;
		}
		return 0;
	}

	void setLookTimeText()
	{
		float lookTime = numberLocationDifficulty.GetDefaultHowLongToNumbersSee();
		LookTimeText.text = lookTime.ToString() + "s" + " (-" + numberLocationDifficulty.GetHowLongMinus().ToString() + "s)";
	}

	void setNumbersText()
	{
		int[] numbers = numberLocationDifficulty.getNumbers();
		int numbersCount = numbers.Length;
		NumbersText.text = numbersCount.ToString() + " Numbers";
	}

	public void PlayButtonClicked()
	{
		StartCoroutine(exitAnimation());
	}

	IEnumerator exitAnimation()
	{
		TopPanel.SetActive(true);
		AudioManager.instance.PlaySFX("PopClick");
		dialogAnimator.ExitDialog();
		backDropLifeCycle.ExitAnimation();
		yield return new WaitForSeconds(0.2f);
		AudioManager.instance.PlaySFX("MinigameStartSfx");
		numberLocationGame.difficulty = difficulty;
		numberLocationGame.StartTheGame();
		gameObject.SetActive(false);
		BackDrop.SetActive(false);
	}

	public void SetDifficulty(NumberLocationDifficulty.Difficulty difficulty)
	{
		this.difficulty = difficulty;
		numberLocationDifficulty.SetDifficulty(difficulty);
	}
}
