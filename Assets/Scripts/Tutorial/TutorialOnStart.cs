using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TutorialOnStart : MonoBehaviour
{
	public MovingForwardTutorialSequenceScriptableObject movingForwardTutorialSequenceScriptableObject;

    public List<Phase> phases;

	void OnEnable()
	{
		if (PlayerPrefs.HasKey(movingForwardTutorialSequenceScriptableObject.SequenceName)) return;

		StartCoroutine(StartTutorial());
	}

	IEnumerator StartTutorial()
	{
		yield return new WaitForSeconds(0.7f);
		PopUpManager.instance.ShowTutorial(movingForwardTutorialSequenceScriptableObject);
	}
}

[System.Serializable]
[IncludeInSettings(true)]
public class Phase
{
    public int phaseState;
    public MovingForwardTutorialSequenceScriptableObject[] Sequences;
}
