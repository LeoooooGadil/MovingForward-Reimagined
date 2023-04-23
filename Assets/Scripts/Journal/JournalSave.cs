using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalSave
{
	public Dictionary<string, JournalEntry> journalEntries;

	public JournalSave()
	{
		journalEntries = new Dictionary<string, JournalEntry>();
	}

	public JournalSave(JournalSaveData _journalSaveData)
	{
		journalEntries = _journalSaveData.journalEntries;
	}

	public void AddEntry(JournalEntry _entry)
	{
		journalEntries.Add(_entry.entryId, _entry);
	}
}

[System.Serializable]
public class JournalEntry
{
	public string entryId { get; set; }
	public string entryTitle { get; set; }
	public DateTime entryDate { get; set; }
	public string entryText { get; set; }
	public string authorName { get; set; }
	public bool entryIsPublic { get; set; }
	public bool entryIsAnonymous { get; set; }

	public JournalEntry()
	{
        entryId = KeyGenerator.GetKey();
	}

	public JournalEntry(string title, DateTime date, string text, string author, bool isPublic, bool isAnonymous)
	{
		entryId = KeyGenerator.GetKey();
		entryTitle = title;
		entryDate = date;
		entryText = text;
		authorName = author;
		entryIsPublic = isPublic;
		entryIsAnonymous = isAnonymous;
	}
}
