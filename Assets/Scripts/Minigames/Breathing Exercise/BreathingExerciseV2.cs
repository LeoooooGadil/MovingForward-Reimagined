using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingExerciseV2 : MonoBehaviour
{
	// WHEN INHALING COUNT UP THE TIMER UNTIL THE MAX TIMER AND USE THE TIMER TO EXHALE AND MAKE THE CIRCLE SMALLER

	public Text centerText;
	public Text upperText;
	public Transform[] outerCircles = new Transform[4];
	float currentTimer = 0f;
	int currentCountdown = 3;
	public float maxTimerWhenInhaling = 7f;
	public float timerWhenInhaling = 0f;
	public float maxTimerWhenExhaling = 7f;
	public float timerWhenExhaling = 0f;
	public int howManyTimesToBreath = 10;
	public int howLongToHold = 0;

	public bool isChoreCompleted = false;

	int inhaleCount = 0;
	int exhaleCount = 0;

	public string[] compliments = new string[6]{
		"Wow!",
		"Keep it up!",
		"You're doing great!",
		"Awesome!",
		"Nice!",
		"Breathetaking!"
	};


	// 0 = nothing
	// 1 = countdown to inhale
	// 2 = inhale
	// 3 = hold
	// 4 = exhale
	int state = 0;
	string previousCompliment = "";

	void Start()
	{
		SetCenterText("Ready");
		SetUpperText("Complete " + howManyTimesToBreath + " cycles of breathing to complete a session.\nPlace your two thumbs on the screen to begin");
		outerCircleFade();
	}

	void Update()
	{
		checkFingers();

		switch (state)
		{
			case 0:
				currentTimer = 0f;
				if (inhaleCount == 0 && exhaleCount == 0)
				{
					SetCenterText("Ready");
					SetUpperText("Place your two thumbs on the screen to begin");
				}
				else if (howManyTimesToBreath <= 0)
				{
					SetCenterText("Done");
					SetUpperText("You have completed " + exhaleCount + " cycle(s). Well done!");

					CheckChoreStatus();
				}
				else
				{
					SetCenterText("Again");
					SetUpperText("You have completed " + exhaleCount + " cycle(s).");
				}
				break;
			case 1:
				countdownToInhale();
				break;
			case 2:
				Inhaling();
				break;
			case 3:
				HoldingToExhale();
				break;
			case 4:
				Exhaling();
				break;
			case 5:
				break;
			default:
				break;
		}

		currentTimer += Time.deltaTime;
	}

	void CheckChoreStatus()
	{
		if (isChoreCompleted) return;

		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore != null && chore.dailyChoreType == DailyChoreType.Breathe)
		{
			if (chore.minScore <= exhaleCount)
			{
				ChoresManager.instance.CompleteChore(chore);
				UpdateStatistics();
				AffirmationManager.instance.ScheduleRandomAffirmation();
				isChoreCompleted = true;
			}
		}
	}

	void UpdateStatistics()
	{
		BreathingExerciseV2CompletedEvent completedEvent = new BreathingExerciseV2CompletedEvent(
			"Completed Breathing Exercise Session",
			(int)exhaleCount
		);

		Aggregator.instance.Publish(completedEvent);
	}

	// a coroutine that counts down from 3 to 0
	// when it reaches 0, it changes the state to 2
	// and changes the text to inhale
	void countdownToInhale()
	{

		if (inhaleCount != 0 || exhaleCount != 0)
		{
			state = 2;
			return;
		}

		SetUpperText("Get ready to inhale");
		// countdown from 3 to 0
		// wait 1 second count down



		if (currentCountdown <= 0)
		{
			currentTimer = 0f;
			state = 2;
			AudioManager.instance.PlaySFX("TimerTickSfx");
			SetCenterText("Inhale");
		}
		else
		{
			SetCenterText(currentCountdown.ToString());
			if (currentTimer >= 1f)
			{
				currentTimer = 0f;
				currentCountdown--;
				AudioManager.instance.PlaySFX("TimerTickSfx");
			}
		}
	}

	void Inhaling()
	{
		SetUpperText("Inhaling for " + (maxTimerWhenInhaling - currentTimer).ToString("F1") + " seconds.\nRelease anytime to exhale.");
		SetCenterText("Inhale");
		makeOuterCircleBigger();
		if (currentTimer >= maxTimerWhenInhaling)
		{
			currentTimer = 0f;
			state = 3;
			inhaleCount++;
			SetCenterText("Hold");
			return;
		}

		timerWhenInhaling = currentTimer;
	}

	void Exhaling()
	{
		SetUpperText("Exhaling for " + (timerWhenInhaling - currentTimer).ToString("F1") + " seconds");
		SetCenterText("Exhale");
		makeOuterCircleSmaller();
		if (currentTimer >= timerWhenInhaling)
		{
			currentTimer = 0f;
			state = 0;
			howManyTimesToBreath--;
			howLongToHold = 0;
			exhaleCount++;
			// PlayerStatisticsManager.instance.weeklyStatsManager.AddBreathingExerciseCompleted();
			return;
		}

		timerWhenExhaling = currentTimer;
	}

	// count how long the player holds their breath
	void HoldingToExhale()
	{
		SetUpperText("Holding for " + howLongToHold + " seconds.\nRelease your finger(s) to exhale.");


		if (currentTimer >= 1f)
		{
			currentTimer = 0f;
			howLongToHold++;

			// for every 10 seconds the player holds their breath, give them a compliment
			if (howLongToHold % 10 == 0)
			{
				// pick a random compliment from the array
				// check if the compliment is the same as the previous one
				// if it is, pick another one
				// if it isn't, set the compliment
				int randomCompliment = Random.Range(0, compliments.Length);
				if (compliments[randomCompliment] == previousCompliment)
				{
					while (compliments[randomCompliment] == previousCompliment)
					{
						randomCompliment = Random.Range(0, compliments.Length);
					}
				}
				else
				{
					previousCompliment = compliments[randomCompliment];
					SetCenterText(compliments[randomCompliment]);
				}

				// SoundManager.instance.PlaySFX(4);
			}
		}
	}

	void SetCenterText(string text)
	{
		centerText.text = text;
	}

	void SetUpperText(string text)
	{
		upperText.text = text;
	}

	void outerCircleFade()
	{
		float maxAlpha = 1f;
		float denominator = maxAlpha / outerCircles.Length;

		for (int i = 0; i < outerCircles.Length; i++)
		{
			Color color = outerCircles[i].GetComponent<SpriteRenderer>().color;
			color.a = maxAlpha - (denominator * i);
			outerCircles[i].GetComponent<SpriteRenderer>().color = color;
		}
	}

	// check how many fingers are on the screen
	// if fingers is 2 and state is 0, change state to 1
	// if fingers is 1 and state is 1, change state to 0
	// if fingers is 0 and state is 1, change state to 0
	// if fingers is 1 and state is 3, change state to 
	// if fingers is 0 and state is 3, change state to 4
	// if fingers is 0 and state is 2, change state to 4
	// and reset the timer when the state changes
	void checkFingers()
	{
		if (Input.touchCount == 2 && state == 0)
		{
			state = 1;
			currentTimer = 0f;
		}
		else if (Input.touchCount == 1 && state == 1)
		{
			state = 0;
			currentTimer = 0f;
		}
		else if (Input.touchCount == 0 && state == 1)
		{
			state = 0;
			currentTimer = 0f;
		}
		else if (Input.touchCount == 1 && state == 3)
		{
			state = 4;
			currentTimer = 0f;
		}
		else if (Input.touchCount == 0 && state == 3)
		{
			state = 4;
			currentTimer = 0f;
		}
		else if (Input.touchCount == 0 && state == 2)
		{
			state = 4;
			currentTimer = 0f;
		}
	}

	public void makeOuterCircleBigger()
	{
		// make the outer circle bigger based on the index
		// make the scale of the circle bigger based on the maxTimerWhenInhaling and the percentage of 3
		float outerCircleGrowthRate = 0.035f / maxTimerWhenInhaling;
		// a float which determines how fast each circle grows


		// for each circle, make it bigger at a different rate


		for (int i = 0; i < outerCircles.Length; i++)
		{
			float percentage = (i + 1) / (float)outerCircles.Length;
			outerCircles[i].localScale += new Vector3(outerCircleGrowthRate * percentage, outerCircleGrowthRate * percentage, 0f);
		}
	}

	public void makeOuterCircleSmaller()
	{
		// make the outer circle smaller based on the index
		// make the scale of the circle smaller based on the maxTimerWhenExhaling and the percentage of 3

		float outerCircleGrowthRate = 0.035f / maxTimerWhenExhaling;
		for (int i = 0; i < outerCircles.Length; i++)
		{
			float percentage = (i + 1) / (float)outerCircles.Length;

			if (outerCircles[i].localScale.x <= 5f) continue;

			outerCircles[i].localScale -= new Vector3(outerCircleGrowthRate * percentage, outerCircleGrowthRate * percentage, 0f);
		}
	}
}
