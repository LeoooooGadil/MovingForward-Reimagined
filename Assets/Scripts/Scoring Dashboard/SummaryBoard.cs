using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    // a summary board
    // displays a text of the player's most played mini game or activity
    // summarizes the overall activity of the player
    // displays the player's most played mini game or activity
    // shows how many times the player did the activity inside 24 hours
    // shows how many times the player did the activity inside 7 days
public class SummaryBoard : MonoBehaviour
{
    public Text summaryText;

    private Dictionary<string, int> activityCount = new Dictionary<string, int>();

    public void ActivityPlayed(string activityName)
    {
        if (activityCount.ContainsKey(activityName))
        {
            activityCount[activityName]++;
        }
        else
        {
            activityCount.Add(activityName, 1);
        }

        UpdateSummaryText();
    }

    private void UpdateSummaryText()
    {
        string summary = "Summary:\n";

        foreach (KeyValuePair<string, int> activity in activityCount)
        {
            summary += activity.Key + ": " + activity.Value + " times\n";
        }

        summary += "Last played at: " + System.DateTime.Now.ToString("hh:mm:ss tt");

        summaryText.text = summary;
    }


}
