using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuTab : MonoBehaviour
{
	public string tabName;
	public int tabIndex;
	public Texture2D tabIcon;
	public bool isActive = false;

    private TMP_Text tabNameText;
	private Button button;
	private TabAnimator tabAnimator;

	void Awake()
	{
        tabNameText = GetComponentInChildren<TMP_Text>();
		tabAnimator = GetComponent<TabAnimator>();

        if (tabNameText == null)
        {
            Debug.LogError("tabNameText is null");
        }

		if (tabAnimator == null)
		{
			Debug.LogError("tabAnimator is null");
		}
	}

	void Start() {
		tabNameText.text = tabIndex.ToString();
		

		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void Update() {
		if (isActive)
		{
			// color: #0F0F0F
			tabNameText.color = new Color32(15, 15, 15, 255);
			button.interactable = false;
			tabAnimator.SelectTab();
		}
		else
		{
			// color: #F1F1F1
			tabNameText.color = new Color32(241, 241, 241, 255);
			button.interactable = true;
			tabAnimator.DeselectTab();
		}
	}

	public void SetTabActive()
	{
		isActive = true;
	}

	public void SetTabInactive()
	{
		isActive = false;
	}

	public void OnClick()
	{
		AudioManager.instance.PlaySFX("ButtonClick");
		MenuTabManager.Instance.setTabActive(tabIndex);
	}
}
