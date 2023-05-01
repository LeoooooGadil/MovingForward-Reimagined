using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuTabItem : MonoBehaviour, IPointerClickHandler
{
    public string tabName;
	public Sprite tabIcon;
	public Sprite tabEnabledSprite;
	public Sprite tabDisabledSprite;
	public bool isEnabled = false;
	public RawImage tabImage;
    public MenuTabsManager menuTabsManager;
	private Image tabComponent;
    private Transform tabImageTransform;
    private Vector3 tabImageCurrentPos;
	private Vector3 tabImageActivePos;
	private Vector3 tabImageInactivePos;

    private float animSpeed = 15f;

	void Start()
	{
		tabComponent = GetComponent<Image>();
        menuTabsManager = GetComponentInParent<MenuTabsManager>();
		tabImage.texture = tabIcon.texture;
        tabImageTransform = tabImage.transform;
		tabImageInactivePos = tabImageTransform.localPosition;
        tabImageActivePos = tabImageInactivePos + new Vector3(0f, 10f, 0f);
	}

	void Update()
	{
		if (isEnabled)
		{
			tabComponent.sprite = tabEnabledSprite;
            tabImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(tabImage.color.a, 1f, Time.deltaTime * animSpeed));
            tabImageTransform.localScale = Vector3.Lerp(tabImageTransform.localScale, new Vector3(1.1f, 1.1f, 1.1f), Time.deltaTime * animSpeed);
		}
		else
		{
			tabComponent.sprite = tabDisabledSprite;
            tabImage.color = new Color(
                Mathf.Lerp(tabImage.color.r, 0.5f, Time.deltaTime * animSpeed), 
                Mathf.Lerp(tabImage.color.g, 0.5f, Time.deltaTime * animSpeed),
                Mathf.Lerp(tabImage.color.b, 0.5f, Time.deltaTime * animSpeed),
                1f);
            tabImageTransform.localScale = Vector3.Lerp(tabImageTransform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * animSpeed);
		}

        UpdateTabImage();
	}

	void UpdateTabImage()
	{
        if (isEnabled)
        {
            tabImageCurrentPos = tabImageActivePos;
        }
        else
        {
            tabImageCurrentPos = tabImageInactivePos;
        }

        tabImageTransform.localPosition = Vector3.Lerp(tabImageTransform.localPosition, tabImageCurrentPos, Time.deltaTime * animSpeed);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        if (isEnabled)
        {
            return;
        }

        menuTabsManager.SetCurrentTab(tabName);
		AudioManager.instance.PlaySFX("PopClick");
	}
}
