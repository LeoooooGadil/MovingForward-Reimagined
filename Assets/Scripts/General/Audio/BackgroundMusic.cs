using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	public string musicName;

	void OnEnable()
	{
		StartCoroutine(Start());
	}

	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.5f);
		AudioManager.instance.ChangeMusic(musicName);
	}

	void OnDisable()
	{
		if (AudioManager.instance != null)
			AudioManager.instance.StopMusic();
	}
}
