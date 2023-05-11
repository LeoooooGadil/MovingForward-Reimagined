using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	// 0 = Introduction To World Navigation
	// 1 = Introduction To Daily Tasks
	// 3 = Introduction To Minigames 

	public int PhaseState = 0;
	public bool isTutorialActive = false;

	public static TutorialManager instance;

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
}
