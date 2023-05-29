using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyMoodTrackerPopUpController : MonoBehaviour
{
    public UnityAction closingAction;
    public UnityAction dailyMoodTrackerPopUpOkButtonAction;

    public Button dailyMoodTrackerPopUpOkButton;
	void Start()
	{
        dailyMoodTrackerPopUpOkButton.onClick.AddListener(OnPopUpOkButtonClicked);
	}

    public void OnPopUpOkButtonClicked()
    {
        DailyMoodManager.instance.SaveCurrentMood();
        closingAction?.Invoke();
    }
}
