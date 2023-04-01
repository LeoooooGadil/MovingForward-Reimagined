using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreathingTextAnimation : MonoBehaviour
{
	private TMP_Text text;

	public int state = 0;

	void Start()
	{
		text = GetComponent<TMP_Text>();
	}

	void Update()
	{
		switch (state)
		{
            case 0:
                #if UNITY_EDITOR || UNITY_STANDALONE
                    text.text = "PRESS SPACE TO START";
                #elif UNITY_ANDROID || UNITY_IOS
                    // get the finger count
                    int fingerCount = 0;
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                            fingerCount++;
                    }
                    
                    // if 0 fingers, lay 2 fingers on the screen to start
                    // if 1 finger, lay 1 more finger on the screen to start

                    if (fingerCount == 0)
                    {
                        text.text = "PRESS 2 FINGERS TO START";
                    }
                    else if (fingerCount == 1)
                    {
                        text.text = "PRESS 1 MORE FINGER TO START";
                    }
                #endif
                break;
            case 1:
                text.text = "BREATHE OUT";
                break;
            case 2:
                text.text = "BREATHE IN";
                break;
            case 3:
                text.text = "HOLD";
                break;
            default:
                break;
		}   
	}

	public void SetState(int state)
    {
        this.state = state;
    }
}
