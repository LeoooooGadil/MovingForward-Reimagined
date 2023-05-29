using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCaptureKeybind : MonoBehaviour
{
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.F12))
        {
            string screenshotName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            ScreenCapture.CaptureScreenshot(screenshotName, 4);
            Debug.Log("Screenshot taken: " + screenshotName);
            Debug.Log("Screenshot saved to: " + Application.persistentDataPath);
            AudioManager.instance.PlaySFX("ScreenshotSfx");
        }
	}
}
