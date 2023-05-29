using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayedMinigameItem : MonoBehaviour
{
	public Text minigameRowText;
	public Text minigameNameText;
	public Text minigameTimesText;
	public Text minigameScoreText;

	public int minigameRow;
	public string minigameName;
	public int minigameTimes;
	public float minigameScore;

	void Update()
	{
		minigameRowText.text = minigameRow.ToString();
		minigameNameText.text = minigameName;
		minigameTimesText.text = NumberFormatter.FormatNumberWithThousandsSeparator(minigameTimes) + "x";
		minigameScoreText.text = NumberFormatter.FormatNumberWithThousandsSeparator(minigameScore);

		if (minigameRow == 1)
		{
			// #ffda79
			minigameRowText.color = new Color32(255, 218, 121, 255);
		}
		else
		{
            // #ccae62
            minigameRowText.color = new Color32(204, 174, 98, 255);
		}
	}

	public void SetMinigame(int number, string name, int times, float score)
	{
		minigameRow = number;
		minigameName = name;
		minigameTimes = times;
		minigameScore = score;
	}
}
