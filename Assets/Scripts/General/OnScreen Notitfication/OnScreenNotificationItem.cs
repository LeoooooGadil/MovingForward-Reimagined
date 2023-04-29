using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenNotificationItem : MonoBehaviour
{
	public Text text;
	[HideInInspector]
	public OnScreenNotificationType type;
	public Color infoColor;
	public Color warningColor;
	public Color errorColor;
	public Color sucessColor;

	void Start()
	{
		StartCoroutine(WaitAndDestroy());
	}

	IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(2.5f);
		Destroy(gameObject);
	}

	void Update()
	{
		if(type == OnScreenNotificationType.Info)
			text.color = infoColor;
		else if(type == OnScreenNotificationType.Warning)
			text.color = warningColor;
		else if(type == OnScreenNotificationType.Error)
			text.color = errorColor;
		else if(type == OnScreenNotificationType.Sucess)
			text.color = sucessColor;
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
