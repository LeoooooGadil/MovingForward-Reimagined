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
	public long activityTimestamp;

	void Start()
	{
		activityNameText.text = activityName;
		activityScoreText.text = "+" + activityScore.ToString() + "Points";
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
		activityTimestampText.text = "Just now"; // default value. Do not change this line
	}
}
