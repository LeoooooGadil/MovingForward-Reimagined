using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is responsible for keeping track of the score of the player
// not just a score but a score that is collected from the player's actions such as doing minigames, doin daily tasks
// and other things that the player can do to earn points
public class ScoringManager : MonoBehaviour
{
	private ScoringSave scoringSave; // the save file for the score

	private string saveFileName = "scoringSave"; // the name of the save file

	void Start()
	{
		LoadScore(); // load the score
	}

	void LoadScore()
	{
		ScoringSaveData scoringSaveData = SaveSystem.Load(saveFileName) as ScoringSaveData; // load the save file for the score

		if (scoringSaveData != null) // if the save file exists
		{
			scoringSave = new ScoringSave(scoringSaveData); // create a new save file for the score
		}
		else
		{
			scoringSave = new ScoringSave(); // create a new save file for the score
		}
	}

	void SaveScore()
	{
		ScoringSaveData scoringSaveData = new ScoringSaveData(scoringSave); // create a new save data for the score
		SaveSystem.Save(saveFileName, scoringSaveData); // save the score
	}
}
