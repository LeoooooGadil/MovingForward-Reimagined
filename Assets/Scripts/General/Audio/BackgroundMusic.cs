using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	void OnEnable()
	{
        AudioManager.instance.PlayMusic("Caketown", 1, true);
	}

	void OnDisable()
	{
        if (AudioManager.instance != null)
            AudioManager.instance.StopMusic();
	}
}
