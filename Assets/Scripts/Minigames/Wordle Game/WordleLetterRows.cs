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

    public void RemoveLetterFromRow()
    {
        for (int i = word.Length - 1; i >= 0; i--)
        {
            if (word[i] != '\0')
            {
                word[i] = '\0';
                letterBoxes[i].SetLetter('\0');
                break;
            }
        }
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
