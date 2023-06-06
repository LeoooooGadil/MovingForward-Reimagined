using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseShowcaser : MonoBehaviour
{
	public ExerciseTitle titleText;
	public Text descriptionText;
	public Button startButton;

	public string title;
	public string description;

	void Update()
	{
        titleText.exerciseName = title;
        descriptionText.text = description;
	}
}
