using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayActivator : MonoBehaviour, IPointerClickHandler
{
	public GameObject howToPlayPanel;
	public GameObject BackDrop;
	public GameObject MinigameMenu;
	public HowToPlayExitButton exitButton;

	void Start()
	{
		// howToPlayPanel.SetActive(false);
		// BackDrop.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		howToPlayPanel.SetActive(true);
		BackDrop.SetActive(true);

		if (MinigameMenu!.activeSelf)
		{
			exitButton.isMinigameMenuActive = true;
			MinigameMenu.SetActive(false);
		}
		else
		{
			gameObject.SetActive(false);
		}

		AudioManager.instance.PlaySFX("PopClick");
	}
}
