using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayExitButton : MonoBehaviour, IPointerClickHandler
{
	public GameObject howToPlayPanel;
	public GameObject BackDrop;
	public GameObject howToPlayButton;
	public GameObject MinigameMenu;
	public bool isMinigameMenuActive = false;

	public void OnPointerClick(PointerEventData eventData)
	{
		howToPlayPanel.SetActive(false);
		howToPlayButton.SetActive(true);

		if (isMinigameMenuActive)
		{
			MinigameMenu.SetActive(true);
			isMinigameMenuActive = false;
		}
		else
		{
			BackDrop.SetActive(false);
		}

		AudioManager.instance.PlaySFX("PopClick");
	}
}
