using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class JournalShower : MonoBehaviour, IPointerClickHandler
{
	public GameObject BackDrop;
	public JournalEntry journalEntry;

	public TMP_Text journalEntryTitle;
	public TMP_Text journalEntryDate;
	public TMP_InputField journalEntryText;
	public JournalEntryListManager journalEntryListManager;

	void Start()
	{
		BackDrop.SetActive(true);
	}

	void OnDisable()
	{
		journalEntryTitle.text = "";
		journalEntryDate.text = "";
		journalEntryText.text = "";
		journalEntry = null;
		BackDrop.SetActive(false);
	}

	void OnEnable()
	{
		BackDrop.SetActive(true);
	}

	public void SetJournalEntry(JournalEntry journalEntry)
	{
		this.journalEntry = journalEntry;

        journalEntryTitle.text = journalEntry.entryTitle;
        journalEntryDate.text = journalEntry.entryDate.ToString();
        journalEntryText.text = journalEntry.entryText;
	}

	void Update()
	{
		if (journalEntry != null)
		{
			journalEntryTitle.text = journalEntry.entryTitle;
			journalEntryDate.text = journalEntry.entryDate.ToString();
			journalEntryText.text = journalEntry.entryText;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		journalEntryListManager.HideJournalEntry();
	}
}
