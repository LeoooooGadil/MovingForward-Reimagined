using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialNextButton : MonoBehaviour, IPointerClickHandler
{
    public TutorialPopUpController tutorialPopUpController;

	public void OnPointerClick(PointerEventData eventData)
	{
        tutorialPopUpController.Next();
	}
}
