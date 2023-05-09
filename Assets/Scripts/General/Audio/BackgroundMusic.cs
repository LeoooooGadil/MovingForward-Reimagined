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
		while (AudioManager.instance == null)
		{
			try
			{
				AudioManager.instance.PlayMusic(musicName, 1, true);
			} catch
			{
				Debug.Log("AudioManager not found");
			}
			
			yield return new WaitForSeconds(0.1f);
		}
	}

	void OnDisable()
	{
		if (AudioManager.instance != null)
			AudioManager.instance.StopMusic();
	}
}
