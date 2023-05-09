using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeMeOutWinLosePanel : MonoBehaviour
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
	public int highscore = 0;
	public int score = 0;

	void OnEnable()
	{
		BackDrop.SetActive(true);

		if (isWin)
		{
			CenterPiece.sprite = WinSprite;
			TitleText.text = "TRASHED!";
			TitleText.color = WinTextColor;

			if (score > highscore)
			{
				DifficultyText.text = "NEW HIGH SCORE!";
			}
			else
			{
				DifficultyText.text = "SCORE: " + score.ToString();
			}

			ScoreText.text = "HIGH SCORE: " + highscore.ToString();
			AudioManager.instance.PlaySFX("MinigameWinSfx");
		}
		else
		{
			CenterPiece.sprite = LoseSprite;
			TitleText.text = "PILED UP!";
			TitleText.color = LoseTextColor;

			if (score > highscore)
			{
				DifficultyText.text = "NEW HIGH SCORE!";
			}
			else
			{
				DifficultyText.text = "SCORE: " + score.ToString();
			}

			ScoreText.text = "HIGH SCORE: " + highscore.ToString();
			AudioManager.instance.PlaySFX("MinigameLoseSfx");
		}
	}
}
