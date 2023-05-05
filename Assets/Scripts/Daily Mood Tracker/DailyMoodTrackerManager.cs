using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMoodTrackerManager : MonoBehaviour
{
	public List<EmojiButton> emojiButtons;
	public MoodType currentMoodType = MoodType.Neutral;

	void Start()
	{
		InitializeEmojis();
		UpdateEmojis();
	}

	void InitializeEmojis()
	{
		foreach (EmojiButton emojiButton in emojiButtons)
		{
			emojiButton.dailyTrackerManager = this;
		}
	}

	void UpdateEmojis()
	{
		foreach (EmojiButton emojiButton in emojiButtons)
		{
			if (emojiButton.moodType == currentMoodType)
			{
				emojiButton.isActivated = true;
			}
			else
			{
				emojiButton.isActivated = false;
			}
		}
	}

	public void MoodChanged(MoodType moodType)
	{
		currentMoodType = moodType;
		UpdateEmojis();
		AudioManager.instance.PlaySFX("PopClick");
	}
}

public enum MoodType { Loved, Thankful, Happy, Content, Neutral, Tired, Angry, Dissapointed, Sad }

public class Mood
{
	public string name;
	public MoodType type;
	public Mood(string name, MoodType type)
	{
		this.name = name;
		this.type = type;
	}
}