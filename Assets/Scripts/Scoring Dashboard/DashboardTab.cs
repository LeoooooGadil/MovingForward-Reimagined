using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DashboardTab : MonoBehaviour, IPointerClickHandler
{
    public Sprite tabDefaultSprite;
    public Sprite tabSelectedSprite;

    public bool isSelected;

	void Update() {
        if (isSelected) {
            GetComponent<Image>().sprite = tabSelectedSprite;
        } else {
            GetComponent<Image>().sprite = tabDefaultSprite;
        }                       
    }

    public void OnPointerClick(PointerEventData eventData)
	{
        Debug.Log("Tab clicked");
        isSelected = true;
        AudioManager.instance.PlaySFX("PopClick");
        GetComponentInParent<DashboardTabManagers>().SelectTab(this);
	}
}


