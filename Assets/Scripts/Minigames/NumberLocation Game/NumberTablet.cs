using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberTablet : MonoBehaviour, IPointerClickHandler
{
	public GameObject Cover;
	public Text NumberText;


	public NumberLocationGame numberLocationGame;
	public int number = 0;
	public bool isOccupied = false;
	public bool isCovered = true;
	public bool isInteractable = false;


	void Update()
	{
		if (isCovered)
		{
			Cover.SetActive(true);
		}
		else
		{
			Cover.SetActive(false);
		}

		if (numberLocationGame.state == 2 && isOccupied) isInteractable = true;
		else isInteractable = false;
	}

	public void SetNumber(int number)
	{
		if (isOccupied) return;

		this.number = number;
		NumberText.text = number.ToString();
		isOccupied = true;
	}

	public void Reset()
	{
		isCovered = true;
		Cover.SetActive(true);
		isInteractable = false;
		isOccupied = false;
		NumberText.text = "";
		number = 0;
		Image image = GetComponent<Image>();
		// no color and transparent
		image.color = new Color(0f, 0f, 0f, 0f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!isInteractable) return;

		Cover.SetActive(false);
		isCovered = false;
		isInteractable = false;
		bool isAccepted = numberLocationGame.OnTabletClicked(this);

		if (isAccepted)
		{
			Image image = GetComponent<Image>();
			// #33d9b2
			image.color = new Color(0.2f, 0.85f, 0.7f);
		}
		else
		{
			StartCoroutine(WrongNumber());
		}
	}

	public void WinGameAnimation()
	{
		StartCoroutine(WinGame());
	}

	IEnumerator WrongNumber()
	{
		Image image = GetComponent<Image>();
		for (int i = 0; i < 3; i++)
		{
			yield return new WaitForSeconds(0.1f);
			image.color = new Color(0f, 0f, 0f, 0f);
			yield return new WaitForSeconds(0.1f);
			image.color = new Color(1f, 0.32f, 0.32f);
		}
	}

	IEnumerator WinGame()
	{
		Image image = GetComponent<Image>();
		for (int i = 0; i < 5; i++)
		{
			yield return new WaitForSeconds(0.3f);
			image.color = new Color(0f, 0f, 0f, 0f);
			yield return new WaitForSeconds(0.3f);
			image.color = new Color(0.2f, 0.85f, 0.7f);
		}
	}
}
