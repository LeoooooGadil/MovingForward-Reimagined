using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodTrackerListManager : MonoBehaviour
{
	List<CurrentMood> currentMoodList = new List<CurrentMood>();
    List<HowAreYouItem> howAreYouResponseList = new List<HowAreYouItem>();
    List<MoodTrackerListItem> moodList = new List<MoodTrackerListItem>();

    public GameObject MoodTrackerListItemPrefab;

	public string howAreYouFileName = "howAreYouSave";

    void Start()
    {
        GetAllMoods();
        GetAllHowAreYou();
        CreateMoodTrackerList();
        CreateMoodTrackerListUI();
    }

    public void CreateMoodTrackerList()
    {
        moodList.Clear();

        foreach (CurrentMood currentMood in currentMoodList)
        {
            MoodTrackerListItem moodTrackerListItem = new MoodTrackerListItem();
            moodTrackerListItem.mood = currentMood.moodType.ToString();
            moodTrackerListItem.timestamp = currentMood.timestamp;

            moodList.Add(moodTrackerListItem);
        }

        foreach (HowAreYouItem howAreYouItem in howAreYouResponseList)
        {
            MoodTrackerListItem moodTrackerListItem = new MoodTrackerListItem();
            moodTrackerListItem.mood = howAreYouItem.response.ToString();
            moodTrackerListItem.timestamp = howAreYouItem.timestamp;

            moodList.Add(moodTrackerListItem);
        }

        moodList.Sort((x, y) => y.timestamp.CompareTo(x.timestamp));
    }

    public void CreateMoodTrackerListUI()
    {
        foreach (MoodTrackerListItem moodTrackerListItem in moodList)
        {
            GameObject moodTrackerListItemObject = Instantiate(MoodTrackerListItemPrefab, transform);
            Text moodTrackerListItemUI = moodTrackerListItemObject.GetComponent<Text>();

            moodTrackerListItemUI.text = CalculateTimestamp(moodTrackerListItem.timestamp) + " - " + moodTrackerListItem.mood;
        }
    }

	public void GetAllMoods()
	{
		currentMoodList.Clear();

		List<CurrentMood> currentMoodListTemp = DailyMoodManager.instance.AllMoods();

		foreach (CurrentMood currentMood in currentMoodListTemp)
		{
			currentMoodList.Add(currentMood);
		}
	}

	public void GetAllHowAreYou()
	{
		HowAreYouSaveData howAreYouSaveData = SaveSystem.Load(howAreYouFileName) as HowAreYouSaveData;
        HowAreYouSave howAreYouSave;


		if (howAreYouSaveData != null)
		{
			howAreYouSave = new HowAreYouSave(howAreYouSaveData);
		}
		else
		{
			howAreYouSave = new HowAreYouSave();
		}

        List<HowAreYouItem> howAreYouResponseListTemp = howAreYouSave.AllResponses();
        
        foreach (HowAreYouItem howAreYouItem in howAreYouResponseListTemp)
        {
            howAreYouResponseList.Add(howAreYouItem);
        }
	}

    string CalculateTimestamp(long timeStamp)
	{
		//activityTimestampText.text = "Just now"; default value. Do not change this line
		DateTimeOffset now = DateTimeOffset.UtcNow;
		TimeSpan timeDiff = now - DateTimeOffset.FromUnixTimeSeconds(timeStamp);
		
		if (timeDiff.TotalMinutes < 1)
		{
			return "Just now";
		}
		else if (timeDiff.TotalHours < 1)
		{
			int minutesAgo = (int)Math.Round(timeDiff.TotalMinutes);
			return minutesAgo + " minute" + (minutesAgo != 1 ? "s" : "") + " ago";
		}
		else if (timeDiff.TotalDays < 1)
		{
			int hoursAgo = (int)Math.Round(timeDiff.TotalHours);
			return hoursAgo + " hour" + (hoursAgo != 1 ? "s" : "") + " ago";
		}
		else if (timeDiff.TotalDays < 7)
        {
            int daysAgo = (int)Math.Round(timeDiff.TotalDays);
            return daysAgo + " day" + (daysAgo != 1 ? "s" : "") + " ago";
        }
        else if (timeDiff.TotalDays < 365)
        {
            int weeksAgo = (int)Math.Round(timeDiff.TotalDays / 7);
            return weeksAgo + " week" + (weeksAgo != 1 ? "s" : "") + " ago";
        }
        else
        {
            int yearsAgo = (int)Math.Round(timeDiff.TotalDays / 365);
            return yearsAgo + " year" + (yearsAgo != 1 ? "s" : "") + " ago";
        }
	}
}

public class MoodTrackerListItem
{
    public string mood;
    public long timestamp;
}
