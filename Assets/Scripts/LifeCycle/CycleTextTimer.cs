using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleTextTimer : MonoBehaviour
{
	public string lifeCycleTimer;
	private Text timerText;
	private string timerTextString;
    private float timer = 1;

	private void Awake()
	{
		timerText = GetComponent<Text>();
	}

	private void Update()
	{
        if(timer < 1)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
		checkWithLifeCycleManager();
		timerText.text = timerTextString;
	}

	void checkWithLifeCycleManager()
	{
		if (LifeCycleManager.instance == null)
		{
			Debug.Log("LifeCycleManager is null");
			timerTextString = "";
			return;
		}

		string tempTimer = LifeCycleManager.instance.GetTimeLeft(lifeCycleTimer);
		if (tempTimer != "-1") timerTextString = tempTimer;
        else timerTextString = "LifeCycle Not Found";
	}
}
