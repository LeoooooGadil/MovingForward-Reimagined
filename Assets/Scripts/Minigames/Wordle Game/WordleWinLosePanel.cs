using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordleWinLosePanel : MonoBehaviour
{
    public Sprite WinSprite;
	public Sprite LoseSprite;

	public Color32 WinTextColor;
	public Color32 LoseTextColor;

	public Text TitleText;
	public Text DifficultyText;
	public Text ScoreText;
	public Image CenterPiece;
    public GameObject BackDrop;

    public MovingForwardWordleWordsObject.Word wordToGuess;
	public bool isWin = false;
	public int score = 0;

	void OnEnable()
	{
        BackDrop.SetActive(true);

		if (isWin)
		{
			CenterPiece.sprite = WinSprite;
			TitleText.text = "YOU WIN";
			TitleText.color = WinTextColor;
			DifficultyText.text = wordToGuess.word + " - " + wordToGuess.definition;
			ScoreText.text = "+" + score.ToString() + " pts";
			AudioManager.instance.PlaySFX("MinigameWinSfx");
		}
		else
		{
			CenterPiece.sprite = LoseSprite;
			TitleText.text = "YOU LOSE";
			TitleText.color = LoseTextColor;
			DifficultyText.text = wordToGuess.word + " - " + wordToGuess.definition;
			ScoreText.text = "";
            AudioManager.instance.PlaySFX("MinigameLoseSfx");
		}
	}
}
