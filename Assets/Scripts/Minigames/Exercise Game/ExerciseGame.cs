using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseGame : MonoBehaviour
{
	public static ExerciseGame instance;

	public MovingForwardExerciseScriptableObject exerciseScriptableObject;
	public GameObject StartGamePanel;
	public Button StartGameButton;
	public ExerciseShowcaser ExerciseShowcasePanel;
	public Button NextExerciseButton;
	public ExerciseCounting ExerciseCountingPanel;

	// 0 = stretching, 1 = exercise
	public int exerciseState = 0;
	public int exerciseIndex = 0;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	public void Start()
	{
		StartGameButton.onClick.AddListener(StartGame);
	}

	void StartGame()
	{
		StartGamePanel.SetActive(false);
		ExerciseShowcasePanel.gameObject.SetActive(true);
		ExerciseCountingPanel.gameObject.SetActive(false);
		UpdateExercise();
	}

	void UpdateChoreStatus()
	{
		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore != null && chore.dailyChoreType == DailyChoreType.Exercise)
		{
			UpdateStatistics();
			ChoresManager.instance.CompleteChore(chore);
			AffirmationManager.instance.ScheduleRandomAffirmation();
		}
	}

	void UpdateStatistics()
	{
		PhysicalExerciseCompletedEvent completedEvent = new PhysicalExerciseCompletedEvent(
			"Completed Physical Exercise Session",
			true
		);

		Aggregator.instance.Publish(completedEvent);
	}

	void UpdateExercise()
	{
		// responsible for updating the exercise index
		// and exercise state
		// and showing the exercise

		if (exerciseState == 2)
		{
			// do nothing
			UpdateChoreStatus();
			StartGamePanel.SetActive(true);
			ExerciseShowcasePanel.gameObject.SetActive(false);
			ExerciseCountingPanel.gameObject.SetActive(false);
			return;
		}
		ShowExercise();

		if (exerciseState == 0)
		{
			if (exerciseIndex < exerciseScriptableObject.Stretchings.Count - 1)
			{
				exerciseIndex++;
			}
			else
			{
				exerciseIndex = 0;
				exerciseState = 1;
			}
		}
		else if (exerciseState == 1)
		{
			if (exerciseIndex < exerciseScriptableObject.Exercises.Count - 1)
			{
				exerciseIndex++;
			}
			else
			{
				exerciseIndex = 0;
				exerciseState = 2;
			}
		}
	}

	public void ShowExercise()
	{
		if (exerciseState == 2) return;

		Debug.Log("Index" + exerciseIndex);

		AudioManager.instance.PlaySFX("PopClick");

		if (exerciseState == 0)
		{
			ExerciseShowcasePanel.title = exerciseScriptableObject.Stretchings[exerciseIndex].name;
			ExerciseShowcasePanel.description = "";
			foreach (string step in exerciseScriptableObject.Stretchings[exerciseIndex].steps)
			{
				ExerciseShowcasePanel.description += "• " + step + "\n";
			}
		}
		else if (exerciseState == 1)
		{
			ExerciseShowcasePanel.title = exerciseScriptableObject.Exercises[exerciseIndex].name;
			ExerciseShowcasePanel.description = "";
			foreach (string step in exerciseScriptableObject.Exercises[exerciseIndex].steps)
			{
				ExerciseShowcasePanel.description += "• " + step + "\n";
			}
		}
	}

	public string GetExerciseName()
	{
		if (exerciseState == 0)
		{
			return exerciseScriptableObject.Stretchings[exerciseIndex].name;
		}
		else if (exerciseState == 1)
		{
			return exerciseScriptableObject.Exercises[exerciseIndex].name;
		}
		else
		{
			return "";
		}
	}

	public void StartExercise()
	{
		NextExerciseButton.onClick.RemoveListener(StartExercise);
		ExerciseShowcasePanel.gameObject.SetActive(false);
		ExerciseCountingPanel.gameObject.SetActive(true);
		AudioManager.instance.PlaySFX("PopClick");
	}

	public void NextExercise()
	{
		AudioManager.instance.PlaySFX("PopClick");
		ExerciseShowcasePanel.gameObject.SetActive(true);
		ExerciseCountingPanel.gameObject.SetActive(false);
		UpdateExercise();
	}
}
