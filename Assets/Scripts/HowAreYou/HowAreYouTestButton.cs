using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowAreYouTestButton : MonoBehaviour
{
	void Start()
	{
        GetComponent<Button>().onClick.AddListener(OpenHowAreYou);
	}

	void OpenHowAreYou()
	{
        if(HowAreYouManager.instance != null)
            HowAreYouManager.instance.OpenHowAreYouForm();
        else
            Debug.Log("HowAreYouManager Not Found");
	}
}
