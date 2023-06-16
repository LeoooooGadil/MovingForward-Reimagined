using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoodTrackerListPopUpController : MonoBehaviour
{
    public UnityAction closingAction;
    public UnityAction moodTrackerListPopUpOkButtonAction;

    public Button moodTrackerExitButton;
    public Button moodTrackerAddButton;

	void Start()
	{
        moodTrackerExitButton.onClick.AddListener(OnWordlePopUpOkButtonClicked);
        moodTrackerAddButton.onClick.AddListener(OnMoodTrackerAddButtonClicked);
	}

    public void OnWordlePopUpOkButtonClicked()
    {
        closingAction?.Invoke();
    }

    public void OnMoodTrackerAddButtonClicked()
    {
        closingAction?.Invoke();
        HowAreYouManager.instance.OpenHowAreYouForm();
        LevelManager.instance.RemoveScene("Stats Menu");
    }
}
