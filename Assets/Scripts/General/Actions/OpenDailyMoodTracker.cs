using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDailyMoodTracker : MonoBehaviour
{
    public void OpenDailyMoodPopUp()
    {
        StartCoroutine(OpenDailyMoodPopUpCoroutine());
    }

    IEnumerator OpenDailyMoodPopUpCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        PopUpManager.instance.ShowDailyMoodTrackerPopUp();
    }
}
