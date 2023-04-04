using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationBackButton : MonoBehaviour
{
    public GameObject DialogBox;
    public GameObject BackDrop;
    public NumberLocationGame numberLocationGame;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenDialogBox);
    }

    void OpenDialogBox()
    {
        numberLocationGame.ResetGame();
        AudioManager.instance.PlaySFX("PopClick");
        DialogBox.SetActive(true);
        BackDrop.SetActive(true);
    }
}
