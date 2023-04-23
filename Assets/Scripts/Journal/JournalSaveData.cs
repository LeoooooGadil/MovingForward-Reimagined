using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JournalSaveData
{
    public Dictionary<string, JournalEntry> journalEntries = new Dictionary<string, JournalEntry>();

    public JournalSaveData(JournalSave _journalSave)
    {
        journalEntries = _journalSave.journalEntries;
    }
}
