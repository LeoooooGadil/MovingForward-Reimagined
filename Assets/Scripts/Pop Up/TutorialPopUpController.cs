using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPopUpController : MonoBehaviour
{
	public GameObject tutorialPopUpPrefab;
	public GameObject Container;

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
		verticalLayoutGroup = Container.GetComponent<VerticalLayoutGroup>();
		totalSteps = movingForwardTutorialSequenceScriptableObject.Sequences.Count;
		setState(0);
		RemoveCurrentStep();
		ShowCurrentStep();
	}

	void RemoveCurrentStep()
	{
		foreach (Transform child in Container.transform)
		{
			Destroy(child.gameObject);
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
			closingAction?.Invoke();
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