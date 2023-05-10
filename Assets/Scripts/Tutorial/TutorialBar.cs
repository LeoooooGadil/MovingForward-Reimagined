using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBar : MonoBehaviour
{
	[HideInInspector]
	public TutorialPopUpController tutorialPopUpController;
	public MovingForwardFacesScriptableObject faces;
	public string title;
	public string body;
	public SequenceMood mood;

	public RawImage faceImage;
	public Text titleText;
	public Text bodyText;
	public string[] highlightObjects;

	void Start()
	{
		faceImage.gameObject.SetActive(false);
		StartCoroutine(ShowFace());
	}

	IEnumerator ShowFace()
	{
		yield return new WaitForSeconds(0.1f);
		faceImage.gameObject.SetActive(true);
	}

	public void SetSequence(Sequence seq)
	{
		SetFace(seq.Mood);
		setTitle(seq.Title);
		setBody(seq.Text);
		SetHighlightObjects(seq.HighlightObject);
		SetMood(seq.Mood);
	}

	public void SetFace(SequenceMood mood)
	{
		foreach (Faces face in faces.Faces)
		{
			if (face.name == mood)
			{
				faceImage.texture = face.sprite.texture;
				break;
			}
		}
	}

	public void setTitle(string _title)
	{
		title = _title;
		titleText.text = title.ToUpper();
	}

	public void setBody(string _body)
	{
		body = _body;
		bodyText.text = "";
		StartCoroutine(PrintBody());
	}

	IEnumerator PrintBody()
	{
		tutorialPopUpController.setState(1);

		int i = 0;

		while (i < body.Length)
		{
			bool isSpace = body[i] == ' ';
			bodyText.text += body[i];

			if (isSpace)
			{
				yield return new WaitForSeconds(0.1f);
			}

			i++;
			yield return new WaitForSeconds(0.05f);
		}

		tutorialPopUpController.setState(2);
	}

	public void Skip()
	{
		StopAllCoroutines();
		bodyText.text = body;
		tutorialPopUpController.setState(2);
	}

	public void SetHighlightObjects(string[] highlightObjects)
	{
		this.highlightObjects = highlightObjects;
	}

	public void SetMood(SequenceMood mood)
	{
		this.mood = mood;
	}
}
