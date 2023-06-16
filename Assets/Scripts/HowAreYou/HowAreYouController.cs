using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowAreYouController : MonoBehaviour
{
	public static HowAreYouController instance;
	public HowAreYouSave howAreYouSave;

	public Slider howAreYouSlider;
	public Text howAreYouText;
	public Text niceToSeeYouText;

	public int cachedValue = 3;
	public int currentValue = 3;
	public HowAreYouResponse currentResponse = HowAreYouResponse.Okay;

	public string saveFileName = "HowAreYouSave";

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

	void LoadTheResponses()
	{
		HowAreYouSaveData howAreYouSaveData = SaveSystem.Load(saveFileName) as HowAreYouSaveData;

		if (howAreYouSaveData != null)
		{
			howAreYouSave = new HowAreYouSave(howAreYouSaveData);
		}
		else
		{
			howAreYouSave = new HowAreYouSave();
		} 
	}

	void SaveTheResponses()
	{
		SaveSystem.Save(saveFileName, new HowAreYouSaveData(howAreYouSave));
	}

	void Start()
	{
		LoadTheResponses();

		if (ProfileManager.instance.CheckIfNoPlayer())
		{
			niceToSeeYouText.text = "Nice to see you!";
		}
		else
		{
			niceToSeeYouText.text = "Nice to see you, " + ProfileManager.instance.GetUserName() + "!";
		}

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
		HowAreYouItem howAreYouItem = new HowAreYouItem(currentResponse);	
		howAreYouSave.AddAResponse(howAreYouItem);
		SaveTheResponses();
		CloseThePanel();
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
				currentResponse = HowAreYouResponse.Great;
				return "Great";
			case 5:
				currentResponse = HowAreYouResponse.VeryGood;
				return "Very Good";
			case 4:
				currentResponse = HowAreYouResponse.Good;
				return "Good";
			case 3:
				currentResponse = HowAreYouResponse.Okay;
				return "Okay";
			case 2:
				currentResponse = HowAreYouResponse.NotGood;
				return "Not Good";
			case 1:
				currentResponse = HowAreYouResponse.Bad;
				return "Bad";
			case 0:
				currentResponse = HowAreYouResponse.Awful;
				return "Awful";
		}

		currentResponse = HowAreYouResponse.Okay;
		return "Okay";
	}
}
