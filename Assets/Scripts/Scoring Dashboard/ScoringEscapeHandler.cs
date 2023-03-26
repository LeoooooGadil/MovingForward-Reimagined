using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringEscapeHandler : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeBackToMain);
    }

    void ChangeBackToMain()
    {
        AudioManager.instance.PlaySFX("CloseClick");
        LevelManager.instance.RemoveScene("Scoring Dashboard");
    }
}
