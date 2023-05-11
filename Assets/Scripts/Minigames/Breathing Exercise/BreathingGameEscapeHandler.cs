using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingGameEscapeHandler : MonoBehaviour
{
	Button button;

	void Start()
	{
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeBackToGame);
	}

    void ChangeBackToGame()
    {
        AudioManager.instance.PlaySFX("CloseClick");
        LevelManager.instance.ChangeScene("Game", true, SceneTransitionMode.Slide, false);
    }
}
