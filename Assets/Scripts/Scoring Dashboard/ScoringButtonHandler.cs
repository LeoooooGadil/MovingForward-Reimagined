using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringButtonHandler : MonoBehaviour
{
	Button button;

	void Start()
	{
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeToScoringMenu);
	}

    void ChangeToScoringMenu()
    {
        AudioManager.instance.PlaySFX("PopClick");
        LevelManager.instance.ChangeScene("Scoring Dashboard", false);
    }
}
