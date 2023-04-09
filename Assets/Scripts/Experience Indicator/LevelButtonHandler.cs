using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
	Button button;

	void Start()
	{
		button = GetComponent<Button>();

		if (button != null)
		{
            button.onClick.AddListener(OnClick);
		} else {
            button = gameObject.AddComponent<Button>();
        }

		button.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		AudioManager.instance.PlaySFX("PopClick");
		LevelManager.instance.ChangeScene("Profile Creator");
	}
}
