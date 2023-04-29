using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayExitButton : MonoBehaviour, IPointerClickHandler
{
	public GameObject howToPlayPanel;
	public GameObject BackDrop;
	public GameObject howToPlayButton;

	public void OnPointerClick(PointerEventData eventData)
	{
        howToPlayPanel.SetActive(false);
        BackDrop.SetActive(false);
		howToPlayButton.SetActive(true);
        AudioManager.instance.PlaySFX("PopClick");
	}
}
