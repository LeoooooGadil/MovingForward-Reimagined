using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterKeyButton : MonoBehaviour
{
	public Sprite normalSprite;
	public Sprite rightLetterWrongLocationSprite;
	public Sprite rightLetterRightLocationSprite;
	public Sprite LetterNotInWordSprite;

	[HideInInspector]
	public KeyboardInput keyboardInput;
	public string letter;
	public string[] audioClips = new string[3] { "KeyClickSfx", "KeyClickSfx2", "KeyClickSfx3" };
	public bool isSpecial = false;

	int state = 0;
	// 0 = normal
	// 1 = rightLetterWrongLocation
	// 2 = rightLetterRightLocation
	// 3 = LetterNotInWord

	Text letterText;
	Button button;

	void Start()
	{
		button = GetComponent<Button>();
		letterText = GetComponentInChildren<Text>();

		if (button == null)
		{
			button = gameObject.AddComponent<Button>();
		}

		if (!isSpecial && letterText != null)
		{
			letterText.text = letter;
		}

		button.onClick.AddListener(OnClick);
	}

	void Update()
	{
		if (!isSpecial)
		{
			if (state == 0)
			{
				GetComponent<Image>().sprite = normalSprite;
			}
			else if (state == 1)
			{
				GetComponent<Image>().sprite = LetterNotInWordSprite;
			}
			else if (state == 2)
			{
				GetComponent<Image>().sprite = rightLetterWrongLocationSprite;
			}
			else if (state == 3)
			{
				GetComponent<Image>().sprite = rightLetterRightLocationSprite;
			}
		}
	}

	public void UpdateState(int newState)
	{
		state = newState;
	}

	void OnClick()
	{
		int randomIndex = Random.Range(0, audioClips.Length);
		AudioManager.instance.PlaySFX(audioClips[randomIndex]);
		// do something
		keyboardInput.OnKeyPress(letter);
	}
}
