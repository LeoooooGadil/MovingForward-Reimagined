using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUpBackground;

    void Start() {
        Button backgroundButton = popUpBackground.AddComponent<Button>();
        backgroundButton.transition = Selectable.Transition.None;
        backgroundButton.onClick.AddListener(BackgroundIsClicked);
    }

    void BackgroundIsClicked() {
        LevelManager.instance.RemoveScene("Tutorial PopUp");
    }
}
