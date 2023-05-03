using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
	private Button button;

	void Start()
	{
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowPopUp);
	}

	void ShowPopUp()
	{
		DailyMoodTrackerPopUpController dailyMoodTrackerPopUpController = PopUpManager.instance.ShowDailyMoodTrackerPopUp();
	}
}
