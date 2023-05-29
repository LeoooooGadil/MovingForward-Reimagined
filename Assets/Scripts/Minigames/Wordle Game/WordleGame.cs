using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordleGame : MonoBehaviour
{
	public MovingForwardWordleWordsObject movingForwardWordleWordsObject;
	public WordleLetterRows[] wordleLetterRows;
	public KeyboardInput keyboardInput;
	public GameObject keyboardTogglerGameObject;
	public KeyboardToggler keyboardToggler;
	public GameObject wordleWinLosePanelGameObject;
	public GameObject topPanel;
	public GameObject gamePanel;

	private WordleWinLosePanel wordleWinLosePanel;
	private MovingForwardWordleWordsObject.Word wordToGuess;
	private int currentRow = 0;
	private int maxRevealCount = 2;
	private bool isShownDefinition = false;
	private char[] wordToGuessCharArray = new char[5];
	private char[] currentWord = new char[5] { '\0', '\0', '\0', '\0', '\0' };
	private char[] allOverrideWord = new char[5] { '\0', '\0', '\0', '\0', '\0' };
	private char[] overrideWord = new char[5] { '\0', '\0', '\0', '\0', '\0' };
	private int[] letterBoxStates = new int[5];
	private bool isKeyboardEnabled = false;
	private bool isKeyboardEnabledOld = false;
	private Vector3 currentGameYPosition = Vector3.zero;
	private Vector3 newGameYPosition;
	private Vector3 gamePanelStartPosition;

	private Dictionary<string, int> allLetterStates = new Dictionary<string, int>();
	private List<MovingForwardWordleWordsObject.WordListItem> wordList = new List<MovingForwardWordleWordsObject.WordListItem>();
	void PickWordToGuess()
	{
		List<MovingForwardWordleWordsObject.Word> words = movingForwardWordleWordsObject.Words;
		int randomWordIndex = UnityEngine.Random.Range(0, words.Count);
		wordToGuess = words[randomWordIndex];

		Debug.Log("Word to guess: " + wordToGuess.word);
	}

	void Start()
	{
		gamePanelStartPosition = gamePanel.transform.position;
		currentGameYPosition = gamePanel.transform.position;
		newGameYPosition = gamePanel.transform.position;
		wordleWinLosePanelGameObject.SetActive(false);
		keyboardTogglerGameObject.SetActive(false);
		topPanel.SetActive(false);
		wordleWinLosePanel = wordleWinLosePanelGameObject.GetComponent<WordleWinLosePanel>();
		keyboardInput.wordleGame = this;
		PickWordToGuess();
		wordList = ReadWordList();
	}

	void Update()
	{
		if (isKeyboardEnabledOld != keyboardToggler.isActivated && currentRow > 2)
		{
			isKeyboardEnabledOld = isKeyboardEnabled;
			isKeyboardEnabled = keyboardToggler.isActivated;


			// working version

			if (isKeyboardEnabled)
			{
				UpTheGame();
			}
			else
			{
				LowerTheGame();
			}
		}

		UpdateGamePanelTransform();
	}

	// working version 

	void UpdateGamePanelTransform()
	{
		if (currentGameYPosition == newGameYPosition) return;

		currentGameYPosition = gamePanel.transform.position;
		gamePanel.transform.position = new Vector3(
			Mathf.Lerp(currentGameYPosition.x, newGameYPosition.x, Time.deltaTime * 5f),
			Mathf.Lerp(currentGameYPosition.y, newGameYPosition.y, Time.deltaTime * 5f),
			Mathf.Lerp(currentGameYPosition.z, newGameYPosition.z, Time.deltaTime * 5f)
			);
	}

	void UpTheGame()
	{
		Debug.Log("UpTheGame");
		Transform thisTransform = gamePanel.transform;
		// keep in mind that the transform anchor preset is stretch stretch
		float PercentOfScreenHeight = Screen.height * 0.35f;
		Vector3 newYPosition = new Vector3(thisTransform.position.x, thisTransform.position.y + PercentOfScreenHeight, thisTransform.position.z);
		newGameYPosition = newYPosition;
	}

	void LowerTheGame()
	{
		Debug.Log("LowerTheGame");
		// keep in mind that the transform anchor preset is stretch stretch
		newGameYPosition = gamePanelStartPosition;
	}

	public void StartGame()
	{
		keyboardTogglerGameObject.SetActive(true);
		topPanel.SetActive(true);
	}

	public void ResetGame()
	{
		keyboardTogglerGameObject.SetActive(false);
		topPanel.SetActive(false);
		wordleWinLosePanelGameObject.SetActive(false);
		wordleWinLosePanel = wordleWinLosePanelGameObject.GetComponent<WordleWinLosePanel>();
		keyboardInput.wordleGame = this;
		PickWordToGuess();

		overrideWord = new char[5] { '\0', '\0', '\0', '\0', '\0' };
		allOverrideWord = new char[5] { '\0', '\0', '\0', '\0', '\0' };

		// do this later: reset the keyboard
		currentRow = 0;
		maxRevealCount = 2;
		for (int i = 0; i < wordleLetterRows.Length; i++)
		{
			wordleLetterRows[i].ResetRow();
		}
		for (int i = 0; i < currentWord.Length; i++)
		{
			currentWord[i] = '\0';
		}
		for (int i = 0; i < letterBoxStates.Length; i++)
		{
			letterBoxStates[i] = 0;
		}
		allLetterStates.Clear();
	}

	public void KeyboardInputHandler(string key)
	{
		if (key == "Backspace")
		{
			RemoveLetterFromRow();
			wordleLetterRows[currentRow].RemoveLetterFromRow(overrideWord);
		}
		else if (key == "Enter")
		{
			StartCoroutine(Enter());
		}
		else
		{
			char letter = key[0];
			letter = char.ToUpper(letter);
			AddLetterToCurrentWord(letter);
			wordleLetterRows[currentRow].AddLetterToRow(letter);
		}

		OverrideCurrentWord();
		Debug.Log("Current word: " + new string(currentWord));
	}

	void OverrideCurrentWord()
	{
		for (int i = 0; i < currentWord.Length; i++)
		{
			// dont override index if its empty
			if (overrideWord[i] != '\0')
			{
				currentWord[i] = overrideWord[i];
			}
		}
	}

	public bool checkIfRowIsIncomplete()
	{
		for (int i = 0; i < currentWord.Length; i++)
		{
			if (currentWord[i] == '\0')
			{
				return true;
			}
		}
		return false;
	}

	public List<MovingForwardWordleWordsObject.WordListItem> ReadWordList()
	{

		string filePath = Application.streamingAssetsPath + "/WordList.txt";
		List<MovingForwardWordleWordsObject.WordListItem> wordList = new List<MovingForwardWordleWordsObject.WordListItem>();

		// string[] lines = System.IO.File.ReadAllLines(filePath);
		// foreach (string line in lines)
		// {
		// 	MovingForwardWordleWordsObject.WordListItem wordListItem = new MovingForwardWordleWordsObject.WordListItem();
		// 	wordListItem.word = line;
		// 	wordList.Add(wordListItem);
		// }

		foreach (string line in Wordlist.words)
		{
			MovingForwardWordleWordsObject.WordListItem wordListItem = new MovingForwardWordleWordsObject.WordListItem();
			wordListItem.word = line;
			wordList.Add(wordListItem);
		}

		return wordList;
	}

	public bool checifWordIsInWordList()
	{
		for (int i = 0; i < wordList.Count; i++)
		{
			if (wordList[i].word.ToLower() == new string(currentWord).ToLower())
			{
				return true;
			}
		}
		return false;
	}

	IEnumerator Enter()
	{
		if (checkIfRowIsIncomplete())
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			wordleLetterRows[currentRow].AnimateEmptyLetterBoxes();
			OnScreenNotificationManager.instance.CreateNotification("Word is incomplete", OnScreenNotificationType.Warning);
			yield break;
		}

		if (!checifWordIsInWordList())
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			wordleLetterRows[currentRow].AnimateEmptyLetterBoxes();
			OnScreenNotificationManager.instance.CreateNotification("Word is not in the list", OnScreenNotificationType.Warning);
			yield break;
		}

		int[] state = CheckRow();
		allOverrideWord = new char[currentWord.Length];
		overrideWord = new char[currentWord.Length];
		wordleLetterRows[currentRow].SetLetterBoxState(state);
		Dictionary<string, int> letterStates = new Dictionary<string, int>();
		// add allLettersStates to letterStates
		foreach (KeyValuePair<string, int> entry in allLetterStates)
		{
			letterStates.Add(entry.Key, entry.Value);
		}

		for (int i = 0; i < currentWord.Length; i++)
		{
			if (currentWord[i] != '\0')
			{
				// add to the letterStates but if it already exists, then replace it
				if (letterStates.ContainsKey(currentWord[i].ToString().ToUpper()))
				{
					letterStates[currentWord[i].ToString().ToUpper()] = state[i];
				}
				else
				{
					letterStates.Add(currentWord[i].ToString().ToUpper(), state[i]);
				}

				// add to the allLetterStates but if it already exists, then replace it
				if (allLetterStates.ContainsKey(currentWord[i].ToString().ToUpper()))
				{
					allLetterStates[currentWord[i].ToString().ToUpper()] = letterBoxStates[i];
				}
				else
				{
					allLetterStates.Add(currentWord[i].ToString().ToUpper(), letterBoxStates[i]);
				}
			}
		}

		keyboardToggler.ToggleKeyboard();
		yield return new WaitForSeconds(0.01f);
		// keyboardTogglerGameObject.SetActive(false);
		topPanel.SetActive(false);
		yield return new WaitForSeconds(1.5f);

		if (state[0] == 3 && state[1] == 3 && state[2] == 3 && state[3] == 3 && state[4] == 3)
		{
			// The word is correct
			StartCoroutine(TotallyWin());
			yield break;
		}

		keyboardTogglerGameObject.SetActive(true);
		topPanel.SetActive(true);

		if (currentRow == 5)
		{
			wordleWinLosePanel.isWin = false;
			wordleWinLosePanel.wordToGuess = wordToGuess;
			wordleWinLosePanel.score = 0;
			wordleWinLosePanelGameObject.SetActive(true);
			yield break;
		}

		currentRow++;
		currentWord = new char[5];
		Debug.Log("Entering New Line");
	}

	IEnumerator TotallyLose()
	{
		wordleWinLosePanel.isWin = false;
		wordleWinLosePanel.wordToGuess = wordToGuess;
		wordleWinLosePanel.score = 0;
		wordleWinLosePanelGameObject.SetActive(true);
		yield break;
	}

	IEnumerator TotallyWin()
	{
		wordleLetterRows[currentRow].Win();
		yield return new WaitForSeconds(1.5f);
		UpdateChoreManager();
		yield return new WaitForSeconds(1f);
		UpdateStatistics();
		CompensatePlayer();
		AffirmationManager.instance.ScheduleRandomAffirmation();

		TicketAccess.RemoveOneFromTicket("Wordle");
		int ticketCount = TicketAccess.GetTicketCount("Wordle");
		Debug.Log("Ticket Count: " + ticketCount);
		if (ticketCount == 0) CreateNewLifeCycle();

		wordleWinLosePanel.isWin = true;
		wordleWinLosePanel.wordToGuess = wordToGuess;
		wordleWinLosePanel.score = 50;
		wordleWinLosePanelGameObject.SetActive(true);

	}

	void CreateNewLifeCycle()
	{
		float hours = 1.5f;

		LifeCycleItem lifeCycleItem = new LifeCycleItem();
		lifeCycleItem.name = "Wordle";
		lifeCycleItem.startTime = System.DateTime.Now + System.TimeSpan.FromHours(hours);
		lifeCycleItem.maxRepeatCount = -1;
		lifeCycleItem.repeatType = LifeCycleRepeatType.Custom;
		lifeCycleItem.customRepeatTime = System.TimeSpan.FromHours(hours).Seconds;

		LifeCycleManager.instance.AddLifeCycleItem(lifeCycleItem);

		NotificationManager.instance.SendNotification(
			"Moving Forward",
			"Wordle is ready to play again!",
			System.DateTime.Now + System.TimeSpan.FromHours(hours));
	}

	void UpdateStatistics()
	{
		float points = 50;

		ExperienceManager.instance.AddExperience(points);

		WordleCompletedEvent wordleCompletedEvent = new WordleCompletedEvent(
			"Wordle Completed",
			currentRow,
			wordToGuess,
			points
		);

		Aggregator.instance.Publish(wordleCompletedEvent);
	}

	void UpdateChoreManager()
	{
		Chore chore = ChoresManager.instance.GetActiveChore();

		if (chore != null && chore.dailyChoreType == DailyChoreType.Wordle)
		{
			ChoresManager.instance.CompleteChore(chore);
		}
		else
		{
			chore = ChoresManager.instance.FindChore(DailyChoreRoom.None, DailyChoreType.Wordle);

			if (chore != null)
			{
				ChoresManager.instance.CompleteChore(chore);
			}
		}

	}

	void CompensatePlayer()
	{
		ProfileManager.instance.AddMoney(25);
	}

	public Dictionary<string, int> GetAllLetterStates()
	{
		return allLetterStates;
	}

	void AddLetterToCurrentWord(char letter)
	{
		for (int i = 0; i < currentWord.Length; i++)
		{
			if (currentWord[i] == '\0')
			{
				currentWord[i] = letter;
				break;
			}
		}
	}

	void RemoveLetterFromRow()
	{
		for (int i = currentWord.Length - 1; i >= 0; i--)
		{

			if (currentWord[i] != '\0')
			{
				if (overrideWord[i] != '\0') continue;

				currentWord[i] = '\0';
				break;
			}
		}
	}

	int[] CheckRow()
	{
		// 0 = normal
		// 1 = letter not in word
		// 2 = right letter wrong position
		// 3 = right letter right position
		int[] _letterBoxStates = new int[5];

		// Check if the word is correct
		// regardless of the upper/lower case
		// and the order of the letters
		// and if there are two or more of the same letter in the word
		string currentWordString = new string(currentWord);
		string wordToGuessString = wordToGuess.word;

		if (currentWordString.ToLower() == wordToGuessString.ToLower())
		{
			// The word is correct
			// Check if the letters are in the right position
			for (int i = 0; i < currentWord.Length; i++)
			{
				if (currentWordString[i].ToString().ToLower() == wordToGuessString[i].ToString().ToLower())
				{
					_letterBoxStates[i] = 3;
				}
				else
				{
					_letterBoxStates[i] = 2;
				}
			}
		}
		else
		{
			// The word is not correct
			// Check if the letters are in the word
			for (int i = 0; i < currentWord.Length; i++)
			{
				if (wordToGuessString.ToLower().Contains(currentWord[i].ToString().ToLower()))
				{
					// check if the letter is in the right position
					if (currentWordString[i].ToString().ToLower() == wordToGuessString[i].ToString().ToLower())
					{
						_letterBoxStates[i] = 3;
					}
					else
					{
						_letterBoxStates[i] = 2;
					}
				}
				else
				{
					_letterBoxStates[i] = 1;
				}
			}
		}

		letterBoxStates = _letterBoxStates;
		return _letterBoxStates;
	}

	void RevealDefinition()
	{
		WordlePopUpController wordlePopUpController = PopUpManager.instance.ShowWordleHintPopUp();
		wordlePopUpController.SetWordleDefinition(wordToGuess.definition);
	}

	IEnumerator RevealRandomLetter()
	{
		maxRevealCount--;

		if (maxRevealCount >= 0)
		{
			OnScreenNotificationManager.instance.CreateNotification(maxRevealCount + " Reveal Left", OnScreenNotificationType.Info);
		}
		else
		{
			OnScreenNotificationManager.instance.CreateNotification("No Reveal Left", OnScreenNotificationType.Info);
		}

		int index = PickHintLetter();
		wordleLetterRows[currentRow].SetLetter(index, overrideWord[index]);
		yield return new WaitForSeconds(0.5f);
		currentWord[index] = overrideWord[index].ToString().ToUpper()[0];
		wordleLetterRows[currentRow].SetLetterState(index, 3);
	}

	int PickHintLetter()
	{
		string word = wordToGuess.word;
		int randomIndex = UnityEngine.Random.Range(0, word.Length);

		while (allOverrideWord[randomIndex] != '\0' && currentWord[randomIndex] != '\0')
		{
			randomIndex = UnityEngine.Random.Range(0, word.Length);
		}

		overrideWord[randomIndex] = word[randomIndex].ToString().ToUpper()[0];
		allOverrideWord[randomIndex] = word[randomIndex].ToString().ToUpper()[0];
		return randomIndex;
	}

	internal int ActivateHint(WordleHints hintType)
	{
		Debug.Log("Activating Hint: " + hintType.ToString());

		// isAcceptedStates
		// 0 = is not accepted
		// 1 = is accepted and cost money
		// 2 = is accepted and cost no money

		switch (hintType)
		{
			case WordleHints.None:
				return 0;
			case WordleHints.RevealRandomLetter:
				if (maxRevealCount <= 0)
				{
					OnScreenNotificationManager.instance.CreateNotification("No more hints left!", OnScreenNotificationType.Error);
					return 0;
				}
				StartCoroutine(RevealRandomLetter());

				return 1;
			case WordleHints.RevealDefinition:
				RevealDefinition();

				if (isShownDefinition)
				{
					return 2;
				}
				else
				{
					isShownDefinition = true;
				}

				return 1;
			default:
				return 0;
		}
	}
}

public enum WordleHints { None, RevealRandomLetter, RevealDefinition }


