using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMoodTrackerManager : MonoBehaviour
{
	public List<EmojiButton> emojiButtons;
	public MoodType currentMoodType = MoodType.Neutral;

	void Start()
	{
		AudioManager.instance.PlaySFX("PopClick");
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
		DailyMoodManager.instance.SetCurrentMood(moodType);
		UpdateEmojis();
		AudioManager.instance.PlaySFX("PopClick");
	}
}

public enum MoodType { Loved, Thankful, Happy, Content, Neutral, Tired, Angry, Dissapointed, Sad }