using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_PlayOnClick : MonoBehaviour, IPointerDownHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		// Play the SFX
        AudioManager.instance.PlaySFX("ButtonClick");
	}
}
