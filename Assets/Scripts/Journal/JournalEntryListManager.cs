using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JournalEntryListManager : MonoBehaviour
{
	public JournalSave journalSave;
	public GameObject JournalEntryPrefab;

    public GameObject journalShower;
    private JournalShower journalShowerScript;

	// Start is called before the first frame update
	void Start()
	{
        journalShower.SetActive(false);

		journalSave = JournalManager.instance.journalSave;
		UpdateJournalEntryList();
	}

	void OnEnable()
	{
		journalSave = JournalManager.instance.journalSave;
		UpdateJournalEntryList();
	}

	public void UpdateJournalEntryList()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

        StartCoroutine(AddJournalEntryToList());
	}

    public void ShowJournalEntry(JournalEntry entry)
    {
        journalShower.SetActive(true);
        AudioManager.instance.PlaySFX("PopClick");
        journalShowerScript = journalShower.GetComponent<JournalShower>();
        journalShowerScript.SetJournalEntry(entry);
        journalShowerScript.journalEntryListManager = this;
    }

    public void HideJournalEntry()
    {
        journalShower.SetActive(false);
    }

    IEnumerator AddJournalEntryToList()
    {
        // reverse the journal entries so that the newest entries are at the top
        JournalEntry[] reversedJournalEntries = new JournalEntry[journalSave.journalEntries.Count];

        int i = 0;
        foreach (JournalEntry entry in journalSave.journalEntries.Values)
        {
            reversedJournalEntries[reversedJournalEntries.Length - 1 - i] = entry;
            i++;
        }


        // journalEntries are stored in a dictionary, so we need to iterate through the values
		foreach (JournalEntry entry in reversedJournalEntries)
		{
			GameObject entryObject = Instantiate(JournalEntryPrefab, transform);
			entryObject.GetComponent<JournalEntryItem>().SetJournalEntry(entry);
            entryObject.GetComponent<JournalEntryItem>().SetJournalEntryListManager(this);
            yield return new WaitForSeconds(0.05f);
		}
    }
}
