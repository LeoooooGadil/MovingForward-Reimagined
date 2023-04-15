using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLetterBox : MonoBehaviour, IPointerClickHandler
{
	public Sprite normalSprite;
	public Sprite letterNotInWordSprite;
	public Sprite rightLetterWrongPositionSprite;
	public Sprite rightLetterRightPositionSprite;

	public int animateBox = 0;
    public int animateText = 0;

	public int state = 0;
	public string letter;
	// 0 = normal
	// 1 = letter not in word
	// 2 = right letter wrong position
	// 3 = right letter right position

	private Image image;
	private Animator animator;
	private Text text;

	public void Start()
	{
		image = GetComponent<Image>();
		animator = GetComponent<Animator>();
		text = GetComponentInChildren<Text>();
	}

	void Update()
	{
		UpdateSprite();
        UpdateLetter();
	}

	public void SetLetter(char letter)
	{
		this.letter = letter.ToString();
        animator.SetTrigger("NewLetter");
	}

	public void Win()
	{
		animator.SetTrigger("Win");
	}

	public void UpdateLetter()
	{
        if(animateText == 0) return;

        text.text = letter;
	}

	public void UpdateLetterBox(int state)
	{
		this.state = state;
		animator.SetTrigger("Animate");
	}

	void UpdateSprite()
	{
		if (animateBox == 0) return;

		switch (state)
		{
			case 0:
				image.sprite = normalSprite;
				break;
			case 1:
				image.sprite = letterNotInWordSprite;
				break;
			case 2:
				image.sprite = rightLetterWrongPositionSprite;
				break;
			case 3:
				image.sprite = rightLetterRightPositionSprite;
				break;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		// this.state = Random.Range(0, 4);
		// animator.SetTrigger("Animate");
	}
}
