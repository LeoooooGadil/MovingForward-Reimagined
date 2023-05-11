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
	public Sprite highlightSprite;
	public RuntimeAnimatorController highlightAnimatorController;

	public List<GameObject> highlightedObjects;

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
			AudioManager.instance.PlaySFX("LetterSfx", 0.1f);

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

		ShowHighLightObjects();
	}

	public void SetMood(SequenceMood mood)
	{
		this.mood = mood;
	}

	public void ShowHighLightObjects()
	{
		foreach (string obj in highlightObjects)
		{
			GameObject go = GameObject.Find(obj);
			if (go != null)
			{
				// get the position of the object
				// get the width of the object
				// get the height of the object

				Vector3 position = go.transform.position;
				Vector3 size = go.transform.localScale;
				AddHighlightObject(go);
			}
		}
	}

	void AddHighlightObject(GameObject obj)
	{
		// instantiate an object with the same size as the object and a child of the object
		// add rect transform to the object

		// this is a canvas object
		GameObject highlightObject = new GameObject();
		highlightObject.transform.SetParent(obj.transform);
		highlightObject.transform.localPosition = Vector3.zero;
		highlightObject.transform.localScale = Vector3.one;
		highlightObject.name = "HighlightObject";

		// add rect transform
		RectTransform rectTransform = highlightObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(1, 1);
		rectTransform.pivot = new Vector2(0.5f, 0.5f);
		rectTransform.sizeDelta = Vector2.zero + new Vector2(10, 10);

		// add image
		Image image = highlightObject.AddComponent<Image>();
		image.sprite = highlightSprite;
		image.color = new Color(1, 1, 1, 1);

		// set image type to Sliced
		image.type = Image.Type.Sliced;
		image.pixelsPerUnitMultiplier = 0.9f;

		// add animator
		Animator animator = highlightObject.AddComponent<Animator>();
		animator.runtimeAnimatorController = highlightAnimatorController;

		// add to list
		highlightedObjects.Add(highlightObject);
	}

	void ClearHighlightObjects()
	{
		Debug.Log("Clearing Highlight Objects");
		foreach (GameObject obj in highlightedObjects)
		{
			Destroy(obj);
		}
	}

	public void CleanUp()
	{
		ClearHighlightObjects();
	}
}
