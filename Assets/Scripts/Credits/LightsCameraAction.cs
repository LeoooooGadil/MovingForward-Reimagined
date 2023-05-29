using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsCameraAction : MonoBehaviour
{
	public Text lastText;

	void Start()
	{
		Text lastText = GetComponent<Text>();
	}

	void Update()
	{
        if(ProfileManager.instance == null) return;

        lastText.text = "Thank you " + ProfileManager.instance.GetUserName() + " for playing!";
	}
}
