using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLocationLookTimeManager : MonoBehaviour
{
	public Text timeText;

	public float lookTime = 0f;

	void Update()
	{
        timeText.text = lookTime.ToString("F2") + "s";
	}
}
