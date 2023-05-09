using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeMeOutBackButton : MonoBehaviour
{
    public GameObject DialogBox;
	public GameObject BackDrop;
	public TakeMeOutGame takeMeOutGame;

	private Button button;

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenDialogBox);
	}

	void OpenDialogBox()
	{
		takeMeOutGame.ResetTheGame();
		AudioManager.instance.PlaySFX("PopClick");
		DialogBox.SetActive(true);
		BackDrop.SetActive(true);
	}
}
