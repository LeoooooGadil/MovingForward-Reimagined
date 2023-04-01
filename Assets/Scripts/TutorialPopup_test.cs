using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup_test : MonoBehaviour
{
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Show();
        }
	}

    public void Show()
    {
        LevelManager.instance.ChangeScene("Tutorial PopUp", false);
    }
}
