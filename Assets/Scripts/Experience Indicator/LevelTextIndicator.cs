using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextIndicator : MonoBehaviour
{

	public Text levelText;
	private int currentLevel = 0;

	// implement indicating the level of the player
	// make sure to update the text when the level changes
	// make sure to update the text when the experience changes
	void Update()
	{
		if (ExperienceManager.instance != null)
		{
			int level = ExperienceManager.instance.GetExperienceSave().GetLevel();
			if (level != currentLevel)
			{
				currentLevel = level;
				levelText.text = currentLevel.ToString();
				UpdateFontSize();
			}
		}
	}

	// implement updating the font size of the text
	// if the level is 0 to 9 the font size should be 50
	// if the level is 10 to 99 the font size should be __
	// if the level is 100 to 999 the font size should be __
	// the max level is 999
	// the biggest font size should be 50
	// find the best font for each case
	void UpdateFontSize()
	{
			if(currentLevel < 10)
			{
				levelText.fontSize = 50;
			}
			else if(currentLevel < 100)
			{
				levelText.fontSize = 38;
			}
			else
			{
				levelText.fontSize = 28;
			}
	}
}
