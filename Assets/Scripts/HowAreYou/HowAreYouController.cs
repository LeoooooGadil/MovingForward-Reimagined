using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowAreYouController : MonoBehaviour
{
	public static HowAreYouController instance;

	public Slider howAreYouSlider;
	public Text howAreYouText;

	public int cachedValue = 3;
	public int currentValue = 3;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Start()
	{
		AudioManager.instance.PlaySFX("PopClick");
		howAreYouSlider.value = 3;
		howAreYouText.text = TextEquivalent();
	}

	void Update()
	{
		currentValue = Mathf.CeilToInt(howAreYouSlider.value);

		if (cachedValue != currentValue)
		{
			AudioManager.instance.PlaySFX("PloukSfx");
			cachedValue = currentValue;
			howAreYouText.text = TextEquivalent();
		}
	}

	public void SaveTheResponse()
	{

	}

	public void CloseThePanel()
	{
		AudioManager.instance.PlaySFX("CloseClick");
		HowAreYouManager.instance.CloseHowAreYouForm();
	}

	string TextEquivalent()
	{
		switch (currentValue)
		{
			case 6:
				return "Great";
			case 5:
				return "Very Good";
			case 4:
				return "Good";
			case 3:
				return "Okay";
			case 2:
				return "Not Good";
			case 1:
				return "Bad";
			case 0:
				return "Awful";
		}

		return "Okay";
	}
}
