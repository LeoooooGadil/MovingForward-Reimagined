using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBreathingGame : MonoBehaviour
{
    public void OpenBreathingGameScene()
    {
        LevelManager.instance.ChangeScene("Old Breathing Game", true, SceneTransitionMode.Slide, false);
    }
}
