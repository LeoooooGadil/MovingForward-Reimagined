using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTimeCycle : MonoBehaviour
{
    public GameObject[] backgrounds;

    private void Update() {
        
        // if system time is between 6am and 6pm
        if (System.DateTime.Now.Hour >= 6 && System.DateTime.Now.Hour <= 18) {
            // set background to day
            backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(false);
        } else {
            // set background to night
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(true);
        }
    }
}
