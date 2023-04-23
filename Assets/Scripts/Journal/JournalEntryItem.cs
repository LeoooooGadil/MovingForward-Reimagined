using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalEntryItem : MonoBehaviour, IPointerClickHandler
{
	public JournalEntry journalEntry;
	public TMP_Text journalEntryTitle;
	public Text journalEntryDate;
    public JournalEntryListManager journalEntryListManager;

	void Start()
	{
		journalEntryTitle.text = journalEntry.entryTitle;
        UpdateEntryDate();
	}

    public void OnPointerClick(PointerEventData eventData)
	{
		journalEntryListManager.ShowJournalEntry(journalEntry);
	}

	public void SetJournalEntry(JournalEntry journalEntry)
	{
		this.journalEntry = journalEntry;
	}

    public void SetJournalEntryListManager(JournalEntryListManager journalEntryListManager)
    {
        this.journalEntryListManager = journalEntryListManager;
    }

	void UpdateEntryDate()
	{
        // example: Just now, 1 minute ago, 2 hours ago, yesterday, 2 days ago, 3 weeks ago, 4 months ago, 5 years ago
        // calculate the difference between now and the entry date
        // if the difference is less than 1 minute, display "Just now"
        // if the difference is less than 1 hour, display "x minutes ago"
        // if the difference is less than 1 day, display "x hours ago"
        // if the difference is less than 1 week, display "yesterday"
        // if the difference is less than 1 month, display "x days ago"
        // if the difference is less than 1 year, display "x weeks ago"
        // if the difference is less than 2 years, display "x months ago"
        // if the difference is less than 5 years, display "x years ago"
    
        journalEntryDate.text = "Just now";

        long difference = (long)(System.DateTime.Now - journalEntry.entryDate).TotalSeconds;

        if (difference < 60)
        {
            journalEntryDate.text = "Just now";
        }
        else if (difference < 3600)
        {
            journalEntryDate.text = (difference / 60) + " minutes ago";
        }
        else if (difference < 86400)
        {
            journalEntryDate.text = (difference / 3600) + " hours ago";
        }
        else if (difference < 604800)
        {
            journalEntryDate.text = "yesterday";
        }
        else if (difference < 2592000)
        {
            journalEntryDate.text = (difference / 86400) + " days ago";
        }
        else if (difference < 31536000)
        {
            journalEntryDate.text = (difference / 604800) + " weeks ago";
        }
        else if (difference < 63072000)
        {
            journalEntryDate.text = (difference / 2592000) + " months ago";
        }
        else
        {
            journalEntryDate.text = (difference / 31536000) + " years ago";
        }
        
	}
}
