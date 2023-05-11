using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustMeOffUI : MonoBehaviour
{
	public Button PlayButton;
	public GameObject BackDrop;
    public GameObject TopPanel;

	[HideInInspector]
	public DialogAnimator dialogAnimator;
	[HideInInspector]
	public BackDropLifeCycle backDropLifeCycle;
	public DustMeOffGame dustMeOffGame;

	void Start()
	{
        TopPanel.SetActive(false);
        dialogAnimator = GetComponent<DialogAnimator>();
        backDropLifeCycle = BackDrop.GetComponent<BackDropLifeCycle>();
        PlayButton.onClick.AddListener(PlayButtonClicked);
	}

    void PlayButtonClicked()
    {
        StartCoroutine(ExitAnimation());
    }

    IEnumerator ExitAnimation()
    {
        AudioManager.instance.PlaySFX("PopClick");
        dialogAnimator.ExitDialog();
        backDropLifeCycle.ExitAnimation();
        yield return new WaitForSeconds(0.2f);
        // start the game here
        dustMeOffGame.StartTheGame();

        AudioManager.instance.PlaySFX("MinigameStartSfx");
        gameObject.SetActive(false);
        // BackDrop.SetActive(false);
    }
}
