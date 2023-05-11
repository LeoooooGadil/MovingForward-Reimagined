using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileInformationIndicator : MonoBehaviour
{
    public Text usernameText;
    public Text statusText;

    // Update is called once per frame
    void Update()
    {
        try {
            usernameText.text = ProfileManager.instance.GetUserName();
            statusText.text = ProfileManager.instance.GetStatus();
        } catch (System.Exception) {
            usernameText.text = "Player";
            statusText.text = "You are doing great!";
        }
    }
}
