using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
	public static JournalManager instance;
	public JournalSave journalSave;

	public GameObject PaperAirplane;
	public GameObject Paper;
	public GameObject JournalMainPanel;
	public Button JournalSendButton;
	public Button JournalEntriesButton;
	public Button BackButton;
	public GameObject JournalEntriesPanel;
	public GameObject JournalResultsPanel;
	public GameObject EscapeButton;
	public Button MakeJournalEntryButton;

	public TMP_Text journalDate;
	public TMP_Text journalTitle;
	public TMP_Text journalCharactersLeft;
	public TMP_InputField journalInputField;
	public Button journalSendButton;

	private int CharactersLeft = 200;
	private Animator PaperAnimator;
	private string saveFileName = "journalSaveData";
	private int panelState = 0;
	private int panelStatePrevious = -1;
	// 0 = main panel
	// 1 = sent entry show panel
	// 2 = entries panel
	// 3 = mailed entry show panel

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		LoadJournal();

		JournalMainPanel.SetActive(true);
		Paper.SetActive(true);

		PaperAnimator = Paper.GetComponent<Animator>();
		journalSendButton.onClick.AddListener(SendToJournal);
		JournalEntriesButton.onClick.AddListener(JournalEntriesButtonHandler);
		BackButton.onClick.AddListener(BackButtonHandler);
		UpdateJournalTitle();
		UpdateJournalDate();

		DisclaimerPopUpController disclaimer = PopUpManager.instance.ShowDisclaimer();
		// disclaimer that appears when you first open the journal
		// it's a disclaimer that says that we don't read your journal entries and that they're stored locally
		disclaimer.disclaimer = "We DO NOT read your journal entries. They are stored LOCALLY on your device.";
	}

	void LoadJournal()
	{
		JournalSaveData data = SaveSystem.Load(saveFileName) as JournalSaveData;

		if (data != null)
		{
			journalSave = new JournalSave(data);
		}
		else
		{
			journalSave = new JournalSave();
		}

		Debug.Log("Journal loaded.");
	}

	void Update()
	{
		UpdateCharactersLeft();
		UpdateSendItButton();
		UpdatePanels();
	}

	void BackButtonHandler()
	{
		if (panelState! < 1) return;
		AudioManager.instance.PlaySFX("PopClick");

		if (panelState == 1)
		{
			panelState = 0;
		}
		else if (panelState == 2)
		{
			panelState = 0;
		}
	}

	void JournalEntriesButtonHandler()
	{
		if (panelState! < 0) return;
		AudioManager.instance.PlaySFX("PopClick");
		panelState = 2;
	}

	void UpdatePanels()
	{
		if (panelState == panelStatePrevious) return;

		switch (panelState)
		{
			case 0:
				Paper.gameObject.SetActive(true);
				JournalMainPanel.SetActive(true);
				JournalEntriesPanel.SetActive(false);
				JournalResultsPanel.SetActive(false);
				BackButton.gameObject.SetActive(false);
				journalSendButton.gameObject.SetActive(true);
				EscapeButton.SetActive(true);

				if (journalSave.journalEntries.Count > 0) JournalEntriesButton.gameObject.SetActive(true);
				else JournalEntriesButton.gameObject.SetActive(false);
				break;
			case 1:
				Paper.SetActive(false);
				JournalMainPanel.SetActive(false);
				JournalEntriesPanel.SetActive(false);
				JournalResultsPanel.SetActive(true);
				BackButton.gameObject.SetActive(false);
				journalSendButton.gameObject.SetActive(false);
				EscapeButton.SetActive(false);

				if (journalSave.journalEntries.Count > 0) JournalEntriesButton.gameObject.SetActive(true);
				else JournalEntriesButton.gameObject.SetActive(false);
				break;
			case 2:
				Paper.gameObject.SetActive(false);
				JournalMainPanel.SetActive(false);
				JournalEntriesPanel.SetActive(true);
				JournalResultsPanel.SetActive(false);
				BackButton.gameObject.SetActive(true);
				EscapeButton.SetActive(false);

				journalSendButton.gameObject.SetActive(false);
				JournalEntriesButton.gameObject.SetActive(false);
				break;
				// case 2:
				// 	JournalMainPanel.SetActive(false);
				// 	JournalEntriesPanel.SetActive(false);
				// 	JournalResultsPanel.SetActive(true);
				// 	break;
		}

		panelStatePrevious = panelState;
	}

	void UpdateJournalTitle()
	{
		journalTitle.text = "Journal Entry #" + (journalSave.journalEntries.Count + 1);
	}

	void UpdateJournalDate()
	{
		journalDate.text = System.DateTime.Now.ToString("dd/MM/yyyy");
	}

	void UpdateCharactersLeft()
	{
		journalCharactersLeft.text = CharactersLeft - journalInputField.text.Length + " characters left";
	}

	void UpdateSendItButton()
	{
		if (journalInputField.text.Length > 0)
		{
			journalSendButton.interactable = true;
		}
		else
		{
			journalSendButton.interactable = false;
		}
	}

	void SaveJournal()
	{
		JournalSaveData data = new JournalSaveData(journalSave);
		SaveSystem.Save(saveFileName, data);
		Debug.Log("Journal saved.");
		AffirmationManager.instance.ScheduleRandomAffirmation();
	}

	void ResetJournalForm()
	{
		panelState = 0;
	}

	void SendToJournal()
	{
		JournalEntry newEntry = new JournalEntry();
		int entryNumber = journalSave.journalEntries.Count + 1;
		newEntry.entryTitle = "Journal Entry #" + entryNumber;
		newEntry.entryDate = System.DateTime.Now;
		newEntry.entryText = journalInputField.text;
		newEntry.authorName = ProfileManager.instance.GetUserName();
		newEntry.entryIsPublic = true;
		newEntry.entryIsAnonymous = true;

		journalSave.AddEntry(newEntry);
		UpdateStatistics(newEntry);
		Debug.Log("Journal Entry #" + entryNumber + " added to the Journal.");

		StartCoroutine(SendPaperAirplane());
		SaveJournal();
	}

	void UpdateChoreManager()
	{
		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore == null) return;

		if (chore.dailyChoreType == DailyChoreType.JournalEntry)
		{
			ChoresManager.instance.CompleteChore(chore);
		}
		else
		{
			chore = ChoresManager.instance.FindChore(DailyChoreRoom.None, DailyChoreType.JournalEntry);

			if (chore != null)
			{
				ChoresManager.instance.CompleteChore(chore);
			}
		}
	}

	void UpdateStatistics(JournalEntry entry)
	{
		JournalCompletedEvent completedEvent = new JournalCompletedEvent(entry.entryTitle, entry.entryId);
		Aggregator.instance.Publish(completedEvent);
	}

	IEnumerator SendPaperAirplane()
	{
		JournalSendButton.gameObject.SetActive(false);
		EscapeButton.SetActive(false);
		JournalEntriesButton.gameObject.SetActive(false);
		AudioManager.instance.PlaySFX("PopClick");
		yield return new WaitForSeconds(0.2f);
		AudioManager.instance.PlaySFX("WhooshSfx");
		PaperAnimator.SetTrigger("isFinished");
		yield return new WaitForSeconds(0.3f);
		UpdateChoreManager();
		AudioManager.instance.PlaySFX("PaperCrumbleSfx");
		yield return new WaitForSeconds(1f);
		AudioManager.instance.PlaySFX("WhooshSfx");




		yield return new WaitForSeconds(0.2f);
		Paper.SetActive(false);
		PaperAirplane.SetActive(true);
		yield return new WaitForSeconds(1f);
		AudioManager.instance.PlaySFX("FlyAwaySfx");

		// reset journal
		journalInputField.text = "";


		yield return new WaitForSeconds(1f);
		panelState = 1;
		MakeJournalEntryButton.onClick.AddListener(ResetJournalForm);
		yield return new WaitForSeconds(1f);
		PaperAirplane.SetActive(false);
	}
}
