using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseCounting : MonoBehaviour
{
	public Text upperText;
	public Text centerText;

	// 0 = countdown, 1 = exercise
	public int state = 0;

	void OnEnable()
	{
		StartCoroutine(Countdown());
	}

	IEnumerator Countdown()
	{
		yield return new WaitForSeconds(1f);
		upperText.text = "GET STARTED";

		for (int i = 3; i >= 1; i--)
		{
			centerText.text = i.ToString();
			AudioManager.instance.PlaySFX("TimerTickSfx");
			yield return new WaitForSeconds(1f);
		}

		StartCoroutine(CountdownExercise());
	}

	IEnumerator CountdownExercise()
	{
		// this coroutine is responsible for counting down the exercise
		// from 1 to 8 and then going back to 1

		AudioManager.instance.PlaySFX("MinigameStartSfx");

		upperText.text = ExerciseGame.instance.GetExerciseName();
		for (int i = 1; i <= 8; i++)
		{
			centerText.text = i.ToString();
			AudioManager.instance.PlaySFX("TimerTickSfx");
			yield return new WaitForSeconds(1f);
		}

		AudioManager.instance.PlaySFX("WinSfx");

		for (int i = 8; i >= 1; i--)
		{
			centerText.text = i.ToString();
			AudioManager.instance.PlaySFX("TimerTickSfx");
			yield return new WaitForSeconds(1f);
		}

		centerText.text = "";
		AudioManager.instance.PlaySFX("TimerTickSfx");
		AudioManager.instance.PlaySFX("MinigameWinSfx");

		yield return new WaitForSeconds(1f);
		ExerciseGame.instance.NextExercise();
	}
}
