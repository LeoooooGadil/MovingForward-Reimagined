using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordleUI : MonoBehaviour
{
	public GameObject TopPanel;
	public Button PlayButton;
	public GameObject BackDrop;

	[HideInInspector]
	public DialogAnimator dialogAnimator;
	[HideInInspector]
	public BackDropLifeCycle backDropLifeCycle;

	void Start()
	{
		dialogAnimator = GetComponent<DialogAnimator>();
		backDropLifeCycle = BackDrop.GetComponent<BackDropLifeCycle>();
		PlayButton.onClick.AddListener(PlayButtonClicked);
	}

	void OnEnable()
	{
		TopPanel.SetActive(false);
	}

	void OnDisable()
	{
		TopPanel.SetActive(true);
	}

	void PlayButtonClicked()
	{
		StartCoroutine(exitAnimation());
	}

	IEnumerator exitAnimation()
	{
		AudioManager.instance.PlaySFX("PopClick");
		dialogAnimator.ExitDialog();
		backDropLifeCycle.ExitAnimation();
		yield return new WaitForSeconds(0.2f);
		// start the game here


		AudioManager.instance.PlaySFX("MinigameStartSfx");
		gameObject.SetActive(false);
		BackDrop.SetActive(false);
	}
}
