using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskButtonHandler : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeToWorldMenu);
    }

    void ChangeToWorldMenu()
    {
        AudioManager.instance.PlaySFX("PopClick");
        MenuManager.Instance.OpenMenu(1);
    }
}
