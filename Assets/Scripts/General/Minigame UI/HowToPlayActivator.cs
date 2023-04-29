using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayActivator : MonoBehaviour, IPointerClickHandler
{
	public GameObject howToPlayPanel;
	public GameObject BackDrop;

	void Start()
	{
        howToPlayPanel.SetActive(false);
        BackDrop.SetActive(false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        howToPlayPanel.SetActive(true);
        BackDrop.SetActive(true);
		gameObject.SetActive(false);
        AudioManager.instance.PlaySFX("PopClick");
	}
}
