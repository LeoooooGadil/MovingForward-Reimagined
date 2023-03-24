using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEscapeHandler : MonoBehaviour
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
        MenuManager.Instance.CloseMenu();
    }
}
