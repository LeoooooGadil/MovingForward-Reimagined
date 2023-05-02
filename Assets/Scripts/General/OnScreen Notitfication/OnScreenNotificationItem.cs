using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenNotificationItem : MonoBehaviour
{
	public Text text;
	public Outline outline;
	[HideInInspector]
	public OnScreenNotificationType type;
	public Color infoColor;
	public Color warningColor;
	public Color errorColor;
	public Color sucessColor;
	public Transform textTransform;
	float animSpeed = 15f;

	void Start()
	{
		if (type == OnScreenNotificationType.Info)
			text.color = infoColor;
		else if (type == OnScreenNotificationType.Warning)
			text.color = warningColor;
		else if (type == OnScreenNotificationType.Error)
			text.color = errorColor;
		else if (type == OnScreenNotificationType.Sucess)
			text.color = sucessColor;


		StartCoroutine(ScaleNotification());
		StartCoroutine(ScaleText());
		StartCoroutine(FadeIn());
	}

	IEnumerator ScaleText()
	{
		// only scale y axis from 0 to 1
		textTransform.localScale = new Vector3(textTransform.localScale.x, 0f, textTransform.localScale.z);

		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime * animSpeed;
			textTransform.localScale = new Vector3(textTransform.localScale.x, Mathf.Lerp(0f, 1f, t), textTransform.localScale.z);
			yield return null;
		}
	}

	IEnumerator ScaleNotification()
	{
		// only scale height from 0 to 30
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0f);

		float t = 0f;
		float height = 30f;

		while (t < 1f)
		{
			t += Time.deltaTime * animSpeed;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Mathf.Lerp(0f, height, t));
			yield return null;
		}
	}

	IEnumerator FadeIn()
	{
		// fade in from 0 to 1
		// the text will fade in for like 25% of the totalTime
		// after that start fading out

		float t = 0f;
		float fadeTime = 4.5f;

		while (t < 1f)
		{
			t += Time.deltaTime * fadeTime;
			text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0f, 1f, t));
			outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, Mathf.Lerp(0f, 1f, t));
			yield return null;
		}
		
		yield return new WaitForSeconds(3f);
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		// fade out from 1 to 0
		// the text will fade out for like 75% of the totalTime
		// after that destroy the notification

		float t = 0f;
		float fadeTime = 2.5f;

		while (t < 1f)
		{
			t += Time.deltaTime * fadeTime ;
			text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1f, 0f, t));
			outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, Mathf.Lerp(1f, 0f, t));
			yield return null;
		}

		Destroy(gameObject);
	}

	public void SetNotification(string _text, OnScreenNotificationType _type)
	{
		this.text.text = _text.ToUpper();
		this.type = _type;
	}
}

public enum OnScreenNotificationType
{
	Info,
	Warning,
	Error,
	Sucess
}
