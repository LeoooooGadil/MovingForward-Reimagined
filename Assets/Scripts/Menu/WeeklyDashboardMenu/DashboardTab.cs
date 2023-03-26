using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DashboardTab : MonoBehaviour, IPointerClickHandler
{
	public int tabID;
	public string tabName;
	public Sprite tabImageActive;
	public Sprite tabImageInactive;
	public TMP_Text tabNameText;
	public DashboardTabManager dashboardTabManager;

	public bool isActive;
	private Image tabImage;

	void Start()
	{
		tabImage = GetComponent<Image>();
		tabNameText.text = tabName;
	}

	void Update()
	{
		if (isActive)
		{
			tabImage.sprite = tabImageActive;
		}
		else
		{
			tabImage.sprite = tabImageInactive;
		}
	}

	public void SetActive(bool active)
	{
		isActive = active;

	}

	void OnClick()
	{
		if (isActive) return;

        AudioManager.instance.PlaySFX("PopClick");
		dashboardTabManager.SetActiveTab(tabID);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick();
	}
}
