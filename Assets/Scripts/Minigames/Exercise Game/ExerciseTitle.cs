using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseTitle : MonoBehaviour
{
	public Text title;
	public Text titleShadow;
	public string exerciseName;

	void Update()
	{
		title.text = exerciseName;
		titleShadow.text = exerciseName;
	}
}
