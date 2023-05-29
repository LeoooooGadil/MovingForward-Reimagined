using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TutorialOnStart : MonoBehaviour
{
	public List<Phase> phases;

	void OnEnable()
	{
		// log the name of the gameobject this script is attached to
		Debug.Log("TutorialOnStart: " + gameObject.name);
		StartCoroutine(Start());
	}

	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.3f);
		int phase = TutorialManager.instance.GetPhaseState();
		Debug.Log("Phase: " + phase);
		
		// find the phase that matches the current phase
		foreach (var p in phases)
		{
			if (p.phaseState == phase)
			{
				p.Run();
				break;
			}
		}
	}
}

[System.Serializable]
[IncludeInSettings(true)]
public class Phase
{
	public int phaseState;
	public MovingForwardTutorialSequenceScriptableObject[] Sequences;

	public void Run()
	{

		foreach (var sequence in Sequences)
		{
			Debug.Log("Checking Tutorial Sequence: " + sequence.SequenceName);
			bool isCompleted = PlayerPrefs.GetInt(sequence.SequenceName, 0) == 1;

			if (isCompleted) continue;

			Debug.Log("Running Tutorial Sequence: " + sequence.SequenceName);
			PopUpManager.instance.ShowTutorial(sequence);
			break;
		}
	}
}
