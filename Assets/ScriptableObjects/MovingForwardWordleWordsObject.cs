using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Wordle Word Database", menuName = "Moving Forward/Wordle Words", order = 1)]
public class MovingForwardWordleWordsObject : ScriptableObject
{
	[SerializeField]
	public List<Word> Words = new List<Word>();

	[System.Serializable]
	[IncludeInSettings(true)]
	public class Word
	{
		[SerializeField]
		public string word;
		[SerializeField]
		public string definition;
	}

	[System.Serializable]
	public class WordListItem
	{
		[SerializeField]
		public string word;
	}
}
