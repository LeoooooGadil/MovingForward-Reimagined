using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordleBackButton : MonoBehaviour
{
	public GameObject DialogBox;
	public GameObject BackDrop;
	public WordleGame wordleGame;

	private Button button;

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenDialogBox);
	}

	void OpenDialogBox()
	{
		wordleGame.ResetGame();
		AudioManager.instance.PlaySFX("PopClick");
		DialogBox.SetActive(true);
		BackDrop.SetActive(true);
	}
}
