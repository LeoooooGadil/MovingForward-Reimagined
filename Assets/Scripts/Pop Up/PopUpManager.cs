using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
	public static PopUpManager instance;
	public GameObject BackDrop;
	public List<PopUps> PopUps = new();

	private GameObject activePopUp;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public WordlePopUpController ShowWordleHintPopUp()
	{
		if (activePopUp != null) return null;

		BackDrop.SetActive(true);
		activePopUp = Instantiate(PopUps.Find(x => x.Type == PopUpType.WordleHint).PopUpPrefab, GetComponentInChildren<Canvas>().transform);
		activePopUp.GetComponent<WordlePopUpController>().closingAction = () => ClosingPopUp();
		return activePopUp.GetComponent<WordlePopUpController>();
	}

	public MoodTrackerListPopUpController ShowMoodTrackerListPopUp()
	{
		if (activePopUp != null) return null;

		BackDrop.SetActive(true);
		activePopUp = Instantiate(PopUps.Find(x => x.Type == PopUpType.MoodTrackerList).PopUpPrefab, GetComponentInChildren<Canvas>().transform);
		activePopUp.GetComponent<MoodTrackerListPopUpController>().closingAction = () => ClosingPopUp();
		return activePopUp.GetComponent<MoodTrackerListPopUpController>();
	}

	public DailyMoodTrackerPopUpController ShowDailyMoodTrackerPopUp()
	{
		if (activePopUp != null) return null;

		BackDrop.SetActive(true);
		activePopUp = Instantiate(PopUps.Find(x => x.Type == PopUpType.DailyMoodTracker).PopUpPrefab, GetComponentInChildren<Canvas>().transform);
		activePopUp.GetComponent<DailyMoodTrackerPopUpController>().closingAction = () => ClosingPopUp();
		return activePopUp.GetComponent<DailyMoodTrackerPopUpController>();
	}

	public TutorialPopUpController ShowTutorial(MovingForwardTutorialSequenceScriptableObject sequence)
	{
		if (activePopUp != null) return null;

		BackDrop.SetActive(true);
		activePopUp = Instantiate(PopUps.Find(x => x.Type == PopUpType.Tutorial).PopUpPrefab, GetComponentInChildren<Canvas>().transform);
		activePopUp.GetComponent<TutorialPopUpController>().closingAction = () => ClosingPopUp();
		activePopUp.GetComponent<TutorialPopUpController>().movingForwardTutorialSequenceScriptableObject = sequence;
		activePopUp.GetComponent<TutorialPopUpController>().SetBackDrop(BackDrop);
		return activePopUp.GetComponent<TutorialPopUpController>();
	}

	public DisclaimerPopUpController ShowDisclaimer()
	{
		BackDrop.SetActive(true);
		activePopUp = Instantiate(PopUps.Find(x => x.Type == PopUpType.Disclaimer).PopUpPrefab, GetComponentInChildren<Canvas>().transform);
		activePopUp.GetComponent<DisclaimerPopUpController>().closingAction = () => ClosingPopUp();
		return activePopUp.GetComponent<DisclaimerPopUpController>();
	}

	internal void ClosingPopUp()
	{
		BackDrop.SetActive(false);
		Destroy(activePopUp);
		AudioManager.instance.PlaySFX("CloseClick");
	}
}