using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DailyMoodTrackerListButton : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
        AudioManager.instance.PlaySFX("PopClick");
		PopUpManager.instance.ShowMoodTrackerListPopUp();
	}
}
