using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustMeOffBackButton : MonoBehaviour
{
    public GameObject DialogBox;
	public GameObject BackDrop;
	public DustMeOffGame dustMeOffGame;

	private Button button;

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenDialogBox);
	}

	void OpenDialogBox()
	{
		dustMeOffGame.ResetTheGame();
		AudioManager.instance.PlaySFX("PopClick");
		DialogBox.SetActive(true);
		BackDrop.SetActive(true);
	}
}
