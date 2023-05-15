using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastActivityItem : MonoBehaviour
{
	public Text activityNameText;
	public Text activityScoreText;
	public Text activityTimestampText;

	public string activityName;
	public float activityScore;
	public string activityDescription;
	public long activityTimestamp;

	void Start()
	{
		activityNameText.text = activityName;
		if(activityScore > 0)
			activityScoreText.text = "+" + activityScore.ToString() + "Points";
		else if(activityDescription != null)
			activityScoreText.text = activityDescription.ToString();
		else
			activityScoreText.text = "";
		CalculateTimestamp();
	}

	// Calculate the timestamp from the current time and the activity timestamp
	// get the difference between timestamp and current time
	// if difference is less than 1 minute, display "Just now"
	// if difference is less than 1 hour, display "x minutes ago"
	// if difference is less than 1 day, display "x hours ago"
	// use the DateTimeOffset.FromUnixTimeSeconds method to convert the timestamp to a DateTime object
	void CalculateTimestamp()
	{
		//activityTimestampText.text = "Just now"; default value. Do not change this line
		DateTimeOffset now = DateTimeOffset.UtcNow;
		TimeSpan timeDiff = now - DateTimeOffset.FromUnixTimeSeconds(activityTimestamp);
		
		if (timeDiff.TotalMinutes < 1)
		{
			activityTimestampText.text = "Just now";
		}
		else if (timeDiff.TotalHours < 1)
		{
			int minutesAgo = (int)Math.Round(timeDiff.TotalMinutes);
			activityTimestampText.text = minutesAgo + " minute" + (minutesAgo != 1 ? "s" : "") + " ago";
		}
		else if (timeDiff.TotalDays < 1)
		{
			int hoursAgo = (int)Math.Round(timeDiff.TotalHours);
			activityTimestampText.text = hoursAgo + " hour" + (hoursAgo != 1 ? "s" : "") + " ago";
		}
		else
		{
			activityTimestampText.text = "More than a day ago";
		}
	}
}
