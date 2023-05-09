using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOnStart : MonoBehaviour
{
    public MovingForwardTutorialSequenceScriptableObject movingForwardTutorialSequenceScriptableObject;

    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1f);
        PopUpManager.instance.ShowTutorial(movingForwardTutorialSequenceScriptableObject);
    }
}
