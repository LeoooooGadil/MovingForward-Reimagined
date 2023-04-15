using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardToggler : MonoBehaviour
{
	public GameObject onScreenKeyboard;
    public Sprite togglerToOnSprite;
    public Sprite togglerToOffSprite;

    public bool isActivated = false;

	Button button;

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void Update()
	{
        if (isActivated)
        {
            onScreenKeyboard.SetActive(true);
            GetComponent<Image>().sprite = togglerToOffSprite;
        }
        else
        {
            onScreenKeyboard.SetActive(false);
            GetComponent<Image>().sprite = togglerToOnSprite;
        }
	}

    public void ToggleKeyboard()
    {
        isActivated = !isActivated;
    }

	void OnClick()
	{
        ToggleKeyboard();
        AudioManager.instance.PlaySFX("PopClick");
	}
}
