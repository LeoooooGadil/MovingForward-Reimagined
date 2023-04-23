using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyScoreGreetings : MonoBehaviour
{
	public Text greetingsText;

	void Start()
	{
		greetingsText = GetComponent<Text>();
	}

    void Update()
    {
        UpdateText();
    }

	void UpdateText()
	{
		int hour = System.DateTime.Now.Hour;
		if (hour < 12)
		{
			greetingsText.text = "Good Morning";
		}
		else if (hour < 18)
		{
			greetingsText.text = "Good Afternoon";
		}
		else
		{
			greetingsText.text = "Good Evening";
		}

		try
		{
			string name = ProfileManager.instance.GetUserName();

			if (name != null)
			{
				greetingsText.text += ", " + name;
			}
		}
		catch (System.Exception)
		{
			// do nothing
		}
		
		greetingsText.text = greetingsText.text.ToUpper();

	}

}
