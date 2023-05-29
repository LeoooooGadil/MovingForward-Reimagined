using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_OpenLink : MonoBehaviour
{
	public string linkUrl;

    private Button button;

	void Start()
	{
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenLink);
	}

    public void OpenLink()
    {
        AudioManager.instance.PlaySFX("PopClick");
        StartCoroutine(WaitAndOpenLink());
    }

	IEnumerator WaitAndOpenLink()
	{
		yield return new WaitForSeconds(0.5f);
		Application.OpenURL(linkUrl);
	}
}
