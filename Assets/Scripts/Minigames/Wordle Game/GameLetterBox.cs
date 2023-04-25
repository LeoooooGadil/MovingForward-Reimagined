using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLetterBox : MonoBehaviour
{
	public Sprite normalSprite;
	public Sprite letterNotInWordSprite;
	public Sprite rightLetterWrongPositionSprite;
	public Sprite rightLetterRightPositionSprite;
	public Sprite emptySprite;

	public int animateBox = 0;
    public int animateText = 0;

	public int state = 0;
	public char letter;
	// 0 = normal
	// 1 = letter not in word
	// 2 = right letter wrong position
	// 3 = right letter right position

	private Image image;
	private Animator animator;
	private Text text;

	public void Start()
	{
		// set the letter to empty
		letter = '\0';
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
		this.letter = letter;
        animator.SetTrigger("NewLetter");
	}

	public void Win()
	{
		animator.SetTrigger("Win");
	}

	public void UpdateLetter()
	{
        if(animateText == 0) return;

        text.text = letter.ToString();
	}

	public void UpdateLetterBox(int state)
	{
		this.state = state;
		animator.SetTrigger("Animate");
	}

	public void ResetLetterBox()
	{
		this.state = 0;
		animateBox = 1;
	}

	public void EmptyLetterBox()
	{
		StartCoroutine(AnimateEmptyLetterBox());
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

		animateBox = 0;
	}

	IEnumerator AnimateEmptyLetterBox()
	{
		image.sprite = emptySprite;
		yield return new WaitForSeconds(0.1f);
		image.sprite = normalSprite;
		yield return new WaitForSeconds(0.1f);
		image.sprite = emptySprite;
		yield return new WaitForSeconds(0.1f);
		image.sprite = normalSprite;
	}
}
