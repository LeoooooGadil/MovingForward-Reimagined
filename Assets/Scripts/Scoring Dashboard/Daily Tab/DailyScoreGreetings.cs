using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyScoreGreetings : MonoBehaviour
{
	public Text greetingsText;

    void OnEnable()
    {
        greetingsText = GetComponent<Text>();
        UpdateText();
    }

    void Start()
    {
        greetingsText = GetComponent<Text>();
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

        string name = ProfileManager.instance.GetUserName();

        if (name != null)
        {
            greetingsText.text += ", " + name;
        }   
	}

}
