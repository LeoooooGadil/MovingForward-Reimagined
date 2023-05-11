using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
	public Image LoadingBar;

	void Start()
	{
		StartCoroutine(FakeLoadingScreen());
	}

	IEnumerator FakeLoadingScreen()
	{
		float fakeloading = 0f;
		while (fakeloading < 1f)
		{
			float randomProgress = Random.Range(0.01f, 0.1f);
			fakeloading += randomProgress;
			LoadingBar.fillAmount = fakeloading;
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.5f);
		LevelManager.instance.ChangeScene("Game", true, SceneTransitionMode.None, true);
	}

}
