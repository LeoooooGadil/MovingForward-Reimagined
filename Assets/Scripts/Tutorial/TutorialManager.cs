using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
	// 0 = Introduction To World Navigation
	// 1 = Introduction To Daily Tasks
	// 3 = Introduction To Minigames 

	public int PhaseState = 0;
	public bool isTutorialActive = false;

	public static TutorialManager instance;

	public List<Actions> actions;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
        PhaseState = PlayerPrefs.GetInt("TutorialPhaseState", 0);
	}

	public void SetPhaseState(int state)
	{
		PhaseState = state;
        PlayerPrefs.SetInt("TutorialPhaseState", state);
        PlayerPrefs.Save();
	}

	public void GoToNextPhase()
	{
		PhaseState++;
		PlayerPrefs.SetInt("TutorialPhaseState", PhaseState);
		PlayerPrefs.Save();
	}

	public int GetPhaseState()
	{
		return PhaseState;
	}

	public void SetTutorialActive(bool state)
	{
		isTutorialActive = state;
	}

	public bool GetTutorialActive()
	{
		return isTutorialActive;
	}

	public void RunAction(string name)
	{
		foreach (var action in actions)
		{
			if (action.name == name)
			{
				action.function.Invoke();
				Debug.Log("Running Action: " + name);
				break;
			}
		}
	}
}

[System.Serializable]
[IncludeInSettings(true)]
public class Actions
{
	public string name;
	public UnityEvent function;
}

