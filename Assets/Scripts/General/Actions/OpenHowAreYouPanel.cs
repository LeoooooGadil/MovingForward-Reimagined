using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHowAreYouPanel : MonoBehaviour
{
	public void OpenHowAreYouPopUp()
	{
		StartCoroutine(OpenHowAreYouPanelCoroutine());
	}

	IEnumerator OpenHowAreYouPanelCoroutine()
	{
        yield return new WaitForSeconds(0.1f);

        HowAreYouManager.instance.OpenHowAreYouForm();
	}
}
