using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmojiButton : MonoBehaviour
{
    [HideInInspector]
    public DailyMoodTrackerManager dailyTrackerManager;
    public MoodType moodType;
	public bool isActivated = false;
    public Transform emoji;
	public Button button;
    private Vector3 scale = Vector3.one;
    private Color disabledColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    private Color textColor;
    private RawImage buttonImage;
    private ButtonAnimator buttonAnimator;
    private float lerpSpeed = 20f;
    public Text emojiText;

	// Start is called before the first frame update
	void Start()
	{
        textColor = emojiText.color; 
        buttonImage = button.gameObject.GetComponent<RawImage>();
        buttonAnimator = button.gameObject.GetComponent<ButtonAnimator>();
		button.onClick.AddListener(() => OnPointerClick());
	}

	void Update()
	{
        if (isActivated)
        {
           scale = Vector3.Lerp(scale, Vector3.one * 1.2f, Time.deltaTime * lerpSpeed);
           buttonImage.color = Color.Lerp(buttonImage.color, Color.white, Time.deltaTime * lerpSpeed);
           emojiText.color = Color.Lerp(emojiText.color, Color.clear, Time.deltaTime * lerpSpeed);
           buttonAnimator.isActive = false;
           button.interactable = false;
        }
        else
        {
            scale = Vector3.Lerp(scale, Vector3.one, Time.deltaTime * lerpSpeed);
            buttonImage.color = Color.Lerp(buttonImage.color, disabledColor, Time.deltaTime * lerpSpeed);
            emojiText.color = Color.Lerp(emojiText.color, textColor, Time.deltaTime * lerpSpeed);
            buttonAnimator.isActive = true;
            button.interactable = true;
        }

        emoji.localScale = scale;
	}

	public void OnPointerClick()
	{
		dailyTrackerManager.MoodChanged(moodType);
	}
}
