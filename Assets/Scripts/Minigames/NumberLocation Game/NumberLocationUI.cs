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

	public NumberLocationGame numberLocationGame;
	public NumberLocationDifficulty numberLocationDifficulty = new NumberLocationDifficulty();

	public NumberLocationDifficulty.Difficulty difficulty = NumberLocationDifficulty.Difficulty.Easy;
	public DialogAnimator dialogAnimator;

	public void Start()
	{
		dialogAnimator = GetComponent<DialogAnimator>();
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
		}
	}

	void setLookTimeText()
	{
		float lookTime = numberLocationDifficulty.GetDefaultHowLongToNumbersSee();
		LookTimeText.text = "Look Time: " + lookTime.ToString() + "s" + " (-" + numberLocationDifficulty.GetHowLongMinus().ToString() + "s)";
	}

	void setNumbersText()
	{
		int[] numbers = numberLocationDifficulty.getNumbers();
		int numbersCount = numbers.Length;
		NumbersText.text = "Numbers: " + numbersCount.ToString();
	}

	public void PlayButtonClicked()
	{
		StartCoroutine(exitAnimation());
	}

	IEnumerator exitAnimation()
	{
        AudioManager.instance.PlaySFX("PopClick");
        dialogAnimator.ExitDialog();
        yield return new WaitForSeconds(0.1f);
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
