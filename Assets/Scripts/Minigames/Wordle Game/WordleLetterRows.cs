using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordleLetterRows : MonoBehaviour
{
	public GameLetterBox[] letterBoxes;
	public int[] letterBoxStates = new int[5];

	public char[] word = new char[5];

	public void AddLetterToRow(char letter)
	{
		for (int i = 0; i < word.Length; i++)
		{
			if (word[i] == '\0')
			{
				word[i] = letter;
				letterBoxes[i].SetLetter(letter);
				break;
			}
		}
	}

	public void SetLetter(int index, char letter)
	{
		letterBoxes[index].SetLetter(letter.ToString().ToUpper().ToCharArray()[0]);
		word[index] = letter;
	}

	public void SetLetterState(int index, int state)
	{
		letterBoxes[index].UpdateLetterBox(state);
	}

	public void RemoveLetterFromRow(char[] _override)
	{
		for (int i = word.Length - 1; i >= 0; i--)
		{
			if (word[i] != '\0')
			{
				// if the override is not null, then skip the letter and go to the next one
				if (_override[i] != '\0') continue;

				word[i] = '\0';
				letterBoxes[i].SetLetter('\0');
				break;
			}
		}
	}

	public void ResetRow()
	{
		for (int i = 0; i < word.Length; i++)
		{
			word[i] = '\0';
			letterBoxes[i].SetLetter('\0');
			letterBoxes[i].ResetLetterBox();
		}
	}

	public void AnimateEmptyLetterBoxes()
	{
		StartCoroutine(AnimateEmptyLetterBoxesCoroutine());
	}

	public void SetLetterBoxState(int[] states)
	{
		letterBoxStates = states;
		UpdateLetterBoxes(letterBoxStates);
	}

	public void UpdateLetterBoxes(int[] state)
	{
		StartCoroutine(AnimateLetterBoxes(state));
	}

	public void Win()
	{
		StartCoroutine(AnimateWinBoxes());
	}

	IEnumerator AnimateEmptyLetterBoxesCoroutine()
	{
		Handheld.Vibrate();
		for (int i = 0; i < letterBoxes.Length; i++)
		{
			// get the letter variable from the letterbox
			char letter = letterBoxes[i].letter;
			//  only animate if the letter is empty
			if (letter == '\0')
			{
				letterBoxes[i].EmptyLetterBox();
			}
		}
		yield return null;
	}

	IEnumerator AnimateLetterBoxes(int[] state)
	{
		for (int i = 0; i < state.Length; i++)
		{
			letterBoxes[i].UpdateLetterBox(state[i]);
			yield return new WaitForSeconds(0.2f);
		}
		yield return null;
	}

	IEnumerator AnimateWinBoxes()
	{
		for (int i = 0; i < letterBoxes.Length; i++)
		{
			letterBoxes[i].Win();
			yield return new WaitForSeconds(0.2f);
		}
		yield return null;
	}
}
