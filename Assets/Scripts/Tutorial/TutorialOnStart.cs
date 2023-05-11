using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TutorialOnStart : MonoBehaviour
{
	public List<Phase> phases;

	void OnEnable()
	{
		int phase = TutorialManager.instance.GetPhaseState();

		if (phase >= phases.Count) return;

		phases[phase].Run();
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
		// check each sequences if they are completed
		// if not continue to next sequence
		// if completed, check if there is a next sequence
		// if there is, continue to next sequence

		foreach (var sequence in Sequences)
		{
			bool isCompleted = PlayerPrefs.GetInt(sequence.SequenceName, 0) == 1;

			if(isCompleted) continue;

			StartTutorial(sequence);
			break;
		}
	}

	IEnumerator StartTutorial(MovingForwardTutorialSequenceScriptableObject sequence)
	{
		yield return new WaitForSeconds(0.7f);
		PopUpManager.instance.ShowTutorial(sequence);
	}
}
