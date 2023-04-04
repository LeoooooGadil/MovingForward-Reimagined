using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberLocationDifficultyTab : MonoBehaviour, IPointerClickHandler
{
	public Sprite NormalTab;
	public Sprite DisabledTab;
	public bool isActive = false;

	private Image image;
    private ButtonAnimator buttonAnimator;

	void Start()
	{
        image = GetComponent<Image>(); 
        buttonAnimator = GetComponent<ButtonAnimator>();       
	}

	void Update()
	{
		if (isActive)
		{
            image.sprite = NormalTab;
            buttonAnimator.isActive = false;
		}
		else
		{
			image.sprite = DisabledTab;
            buttonAnimator.isActive = true;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		NumberLocationDifficultyTabManager manager = GetComponentInParent<NumberLocationDifficultyTabManager>();
        int currentTab = manager.currentTab;
        int tab = transform.GetSiblingIndex();

        if(currentTab == tab) return;

        AudioManager.instance.PlaySFX("PopClick");
        manager.SetTab(tab);
	}
}
