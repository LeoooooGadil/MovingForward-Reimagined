using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
	public GameObject keyboardLetterPrefab;
	public GameObject keyboardEnterPrefab;
	public GameObject keyboardBackspacePrefab;

	[HideInInspector]
	public WordleGame wordleGame;

	string[][] keyboardLayout = new string[3][];
	public Transform[] keyboardRows = new Transform[3];

	public Dictionary<string, int> letterStates = new Dictionary<string, int>();

	void Awake()
	{
		keyboardLayout[0] = new string[10] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };
		keyboardLayout[1] = new string[9] { "A", "S", "D", "F", "G", "H", "J", "K", "L" };
		keyboardLayout[2] = new string[9] { "Enter", "Z", "X", "C", "V", "B", "N", "M", "BackSpace" };
	}

	void Start()
	{
        InitializeKeyboard();
		UpdateLetterState();
	}

    void OnEnable()
    {
        InitializeKeyboard();
		UpdateLetterState();
    }

	void InitializeKeyboard()
	{   
        RemoveKeyboard();

		for (int i = 0; i < keyboardLayout.Length; i++)
		{
			for (int j = 0; j < keyboardLayout[i].Length; j++)
			{
				if (keyboardLayout[i][j].ToString().ToLower() == "enter")
				{
					Instantiate(keyboardEnterPrefab, keyboardRows[i]);
					LetterKeyButton letterKeyButton = keyboardEnterPrefab.GetComponent<LetterKeyButton>();
					letterKeyButton.isSpecial = true;
					letterKeyButton.letter = "Enter";
					letterKeyButton.keyboardInput = this;
				}
				else if (keyboardLayout[i][j].ToString().ToLower() == "backspace")
				{
					Instantiate(keyboardBackspacePrefab, keyboardRows[i]);
					LetterKeyButton letterKeyButton = keyboardBackspacePrefab.GetComponent<LetterKeyButton>();
					letterKeyButton.isSpecial = true;
					letterKeyButton.letter = "Backspace";
					letterKeyButton.keyboardInput = this;
				}
				else
				{
					GameObject keyboardLetter = Instantiate(keyboardLetterPrefab, keyboardRows[i]);
					LetterKeyButton letterKeyButton = keyboardLetter.GetComponent<LetterKeyButton>();
					letterKeyButton.letter = keyboardLayout[i][j];
					letterKeyButton.keyboardInput = this;
				}
			}
		}
	}

    void RemoveKeyboard()
    {
        foreach (Transform child in keyboardRows[0])
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in keyboardRows[1])
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in keyboardRows[2])
		{
			Destroy(child.gameObject);
		}
    }

	public void UpdateLetterState()
	{
		Dictionary<string, int> letterStates = wordleGame.GetAllLetterStates();

		for(int i = 0; i < keyboardRows.Length; i++)
		{
			for(int j = 0; j < keyboardRows[i].childCount; j++)
			{
				LetterKeyButton letterKeyButton = keyboardRows[i].GetChild(j).GetComponent<LetterKeyButton>();
				if (letterKeyButton != null)
				{
					if (letterStates.ContainsKey(letterKeyButton.letter))
					{
						letterKeyButton.UpdateState(letterStates[letterKeyButton.letter]);
					}
				}
			}
		}
	}

	public void OnKeyPress(string letter)
	{
		wordleGame.KeyboardInputHandler(letter);
	}

	void OnDisable()
	{
		RemoveKeyboard();
	}
}
