using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOnStart : MonoBehaviour
{
    public MovingForwardTutorialSequenceScriptableObject movingForwardTutorialSequenceScriptableObject;

    void Start()
    {
        // if(PlayerPrefs.HasKey(movingForwardTutorialSequenceScriptableObject.SequenceName)) return;

        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(0.7f);
        PopUpManager.instance.ShowTutorial(movingForwardTutorialSequenceScriptableObject);
    }
}
