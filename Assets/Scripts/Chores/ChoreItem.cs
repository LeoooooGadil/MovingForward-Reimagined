using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoreItem : MonoBehaviour
{
	public GameObject checkmark;
	public Sprite defaultBackground;
	public Sprite completedBackground;
	public ChoresMenuManager choreMenuManager;

	public Text choreNameText;
	public Text choreCompensationText;
	public Button chorePlayButton;

	public Chore chore;
	public int index;

	private Image image;
	private Animator animator;
	private float animSpeed = 15f;

	void Start()
	{
		image = GetComponent<Image>();
		animator = GetComponent<Animator>();
		chorePlayButton.onClick.AddListener(PlayChore);

		if (chore == null) return;

		animator.SetBool("isCompleted", chore.isCompleted);
	}

	void Update()
	{
		UpdateText();
		UpdatePlayButton();
		UpdateBackground();
	}

	void UpdateText()
	{
		if (chore == null) return;

		choreNameText.text = chore.choreName;
		choreCompensationText.text = "+₱" + chore.choreComponensation.ToString("F0");
		// chorePointsText.text = chore.chorePoints.ToString() + "xp";
	}

	void UpdatePlayButton()
	{
		// do something about this
	}

	public void SetChore(Chore chore)
	{
		this.chore = chore;
	}

	public void SetIndex(int index)
	{
        this.index = index;
	}

	void UpdateBackground()
	{
		if (chore.isCompleted)
		{
			image.sprite = completedBackground;
			chorePlayButton.interactable = false;
			checkmark.SetActive(true);
			image.color = new Color(
				Mathf.Lerp(image.color.r, 0.75f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.g, 0.75f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.b, 0.75f, Time.deltaTime * animSpeed),
				1f);
		}
		else
		{
			image.sprite = defaultBackground;
			chorePlayButton.interactable = true;
			checkmark.SetActive(false);
			image.color = new Color(
				Mathf.Lerp(image.color.r, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.g, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.b, 1f, Time.deltaTime * animSpeed),
				1f);
		}
	}

	public void PlayChore()
	{
		AudioManager.instance.PlaySFX("AcceptClick");
		// play chore
	}
}
