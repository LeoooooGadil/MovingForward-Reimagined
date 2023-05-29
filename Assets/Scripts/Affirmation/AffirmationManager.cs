using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffirmationManager : MonoBehaviour
{
	public static AffirmationManager instance;
	public List<MovingForwardTutorialSequenceScriptableObject> affirmationSequences;
	public MovingForwardTutorialSequenceScriptableObject inQueueAffirmationSequence;

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
		// add a listener to the OnSceneLoaded event
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
	{
		if (scene.name != "Game") return;

		if (inQueueAffirmationSequence != null)
		{
			MovingForwardTutorialSequenceScriptableObject affirmationSequence = inQueueAffirmationSequence;
			inQueueAffirmationSequence = null;
			StartCoroutine(StartAffirmationSequence(affirmationSequence));
		}
	}

	IEnumerator StartAffirmationSequence(MovingForwardTutorialSequenceScriptableObject affirmationSequence)
	{
        yield return new WaitForSeconds(0.3f);

        PopUpManager.instance.ShowTutorial(affirmationSequence);
	}

	public void ScheduleRandomAffirmation()
	{
		if (inQueueAffirmationSequence != null) return;

		inQueueAffirmationSequence = affirmationSequences[Random.Range(0, affirmationSequences.Count)];
	}
}
