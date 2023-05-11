using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartingScene : MonoBehaviour
{
	public Text timerText;

	public float TotalSeconds = 3;

	public float currentTimer = 1f;

	public UnityAction OnTimerEnd;
	public GameObject BackDrop;

	void OnEnable()
	{
        StartCoroutine(StartTimer());
	}

	public void SetUnityAction(UnityAction unityAction)
	{
		OnTimerEnd = unityAction;
	}

	IEnumerator StartTimer()
	{
		BackDrop.SetActive(true);
		while (true)
		{
			currentTimer -= Time.deltaTime;

			if (currentTimer < 0)
			{
				TotalSeconds--;

				if (TotalSeconds <= 0)
				{
					AudioManager.instance.PlaySFX("TimerTickSfx");
					BackDrop.SetActive(false);
					break;
				}

				AudioManager.instance.PlaySFX("TimerTickSfx");

				currentTimer = 1f;
			}

			timerText.text = TotalSeconds.ToString("F0");

			yield return null;
		}
	}


}
