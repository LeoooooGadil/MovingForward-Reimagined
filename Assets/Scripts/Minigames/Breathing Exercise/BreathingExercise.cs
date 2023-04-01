using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingExercise : MonoBehaviour
{
	public Animator BreathingCircles;
	public Animator BreathingText;

	private bool isActivated = false;

	void Update()
	{
        #if UNITY_EDITOR || UNITY_STANDALONE
            DesktopInput();
        #elif UNITY_ANDROID || UNITY_IOS
            MobileInput();
        #endif
	}

	void DesktopInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!isActivated)
			{
				Activate();
			}
			else
			{
				Deactivate();
			}
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			isOut();
		}
	}

    // mobile input. 2 fingers hold to activate, 2 fingers out to isOut, 2 fingers tap to deactivate
    void MobileInput()
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began)
            {
                if (!isActivated)
                {
                    Activate();
                }
                else
                {
                    Deactivate();
                }
            }
        } else if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isOut();
            }
        }
    }

	public void isOut()
	{
        AudioManager.instance.PlaySFX("PopClick");
		BreathingCircles.SetTrigger("isOut");
		BreathingText.SetTrigger("isOut");
	}

	public void Activate()
	{
        AudioManager.instance.PlaySFX("AcceptClick");
		BreathingCircles.SetBool("isActive", true);
		BreathingText.SetBool("isActive", true);
		isActivated = true;
	}

	public void Deactivate()
	{
        AudioManager.instance.PlaySFX("CloseClick");
		BreathingCircles.SetBool("isActive", false);
		BreathingText.SetBool("isActive", false);
		isActivated = false;
	}
}
