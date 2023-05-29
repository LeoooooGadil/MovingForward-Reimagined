using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPopUpController : MonoBehaviour
{
	public GameObject tutorialPopUpPrefab;
	public GameObject Container;
	public GameObject BackDrop;

	[HideInInspector]
	public MovingForwardTutorialSequenceScriptableObject movingForwardTutorialSequenceScriptableObject;
	public UnityAction closingAction;

	private int currentStep = 0;
	private int totalSteps = 0;


	// 0 = initial
	// 1 = writing
	// 2 = done
	private int sequenceState = 0;
	private VerticalLayoutGroup verticalLayoutGroup;
	private TutorialBar currentTutorialBar;

	void Start()
	{
		TutorialManager.instance.SetTutorialActive(true);
		verticalLayoutGroup = Container.GetComponent<VerticalLayoutGroup>();
		totalSteps = movingForwardTutorialSequenceScriptableObject.Sequences.Count;
		setState(0);
		RemoveCurrentStep();
		ShowCurrentStep();
	}

	public void SetBackDrop(GameObject backdrop)
	{
		BackDrop = backdrop;
	}

	void RemoveCurrentStep()
	{
		if (currentTutorialBar)
		{
			currentTutorialBar.CleanUp();
		}

		foreach (Transform child in Container.transform)
		{
			Destroy(child.gameObject);
		}
	}

	void setBackDropActive(bool active)
	{
		if (BackDrop != null)
		{
			BackDrop.SetActive(active);
		}
	}

	void ShowCurrentStep()
	{
		Sequence sequence = movingForwardTutorialSequenceScriptableObject.Sequences[currentStep];

		setLocation(sequence.Position);

		GameObject tutorialPopUp = Instantiate(tutorialPopUpPrefab, Container.transform);
		TutorialBar tutorialBar = tutorialPopUp.GetComponent<TutorialBar>();
		tutorialBar.tutorialPopUpController = this;
		tutorialBar.SetSequence(sequence);
		setBackDropActive(!sequence.disableBackdrop);

		currentTutorialBar = tutorialBar;

		AudioManager.instance.PlaySFX("ButtonClick");
	}

	public void setState(int state)
	{
		sequenceState = state;
	}

	public void Next()
	{
		if (sequenceState == 1)
		{
			currentTutorialBar.Skip();
			return;
		}

		currentStep++;
		if (currentStep < totalSteps)
		{

			sequenceState = 0;
			RemoveCurrentStep();
			ShowCurrentStep();
		}
		else
		{
			// get actions from the last sequence
			// get actions from the sequence
			string[] actions = movingForwardTutorialSequenceScriptableObject.Sequences[
				currentStep == 0 ? 0 : currentStep - 1
			].actions;

			if (actions != null)
			{
				// perform actions
				foreach (string action in actions)
				{
					TutorialManager.instance.RunAction(action);
				}
			}

			if (currentTutorialBar)
			{
				currentTutorialBar.CleanUp();
			}

			if (movingForwardTutorialSequenceScriptableObject.GoToNextPhase)
			{
				TutorialManager.instance.GoToNextPhase();
			}

			TutorialManager.instance.SetTutorialActive(false);
			PlayerPrefs.SetInt(movingForwardTutorialSequenceScriptableObject.SequenceName, 1);
			closingAction?.Invoke();
			PlayerPrefs.Save();
		}
	}

	void setLocation(SequencePosition sequencePosition)
	{
		switch (sequencePosition)
		{
			case SequencePosition.Top:
				verticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
				break;
			case SequencePosition.Bottom:
				verticalLayoutGroup.childAlignment = TextAnchor.LowerCenter;
				break;
			case SequencePosition.Middle:
				verticalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
				break;
		}
	}
}