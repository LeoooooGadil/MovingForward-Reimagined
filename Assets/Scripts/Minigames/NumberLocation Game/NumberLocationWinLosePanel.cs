using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationWinLosePanel : MonoBehaviour
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

	public bool isWin = false;
	public NumberLocationDifficulty.Difficulty difficulty = NumberLocationDifficulty.Difficulty.Easy;
	public int score = 0;

	void OnEnable()
	{
        BackDrop.SetActive(true);

		if (isWin)
		{
			CenterPiece.sprite = WinSprite;
			TitleText.text = "YOU WIN";
			TitleText.color = WinTextColor;
			DifficultyText.text = difficulty.ToString().ToUpper() + " MODE";
			ScoreText.text = "+" + score.ToString() + " POINTS";
			AudioManager.instance.PlaySFX("MinigameWinSfx");
		}
		else
		{
			CenterPiece.sprite = LoseSprite;
			TitleText.text = "YOU LOSE";
			TitleText.color = LoseTextColor;
			DifficultyText.text = difficulty.ToString().ToUpper() + " MODE";
			ScoreText.text = "";
            AudioManager.instance.PlaySFX("MinigameLoseSfx");
		}
	}
}
