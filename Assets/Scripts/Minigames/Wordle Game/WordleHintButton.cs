using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordleHintButton : MonoBehaviour, IPointerClickHandler
{
	public WordleGame wordleGame;
	public WordleHints hintType;
	public float hintCost = 1f;

	public void OnPointerClick(PointerEventData eventData)
	{
		float userMoney = ProfileManager.instance.GetMoney();

		if (userMoney < hintCost)
		{
			OnScreenNotificationManager.instance.CreateNotification("You don't have enough money to buy this hint!", OnScreenNotificationType.Error);
			AudioManager.instance.PlaySFX("EhhEhhClick");
			return;
		}

		int isActivated = wordleGame.ActivateHint(hintType);

		if (isActivated > 0)
		{
			if (isActivated == 1)
			{
				ProfileManager.instance.NegateMoney(hintCost);
			}

			AudioManager.instance.PlaySFX("PopClick");
		}
		else
		{
			AudioManager.instance.PlaySFX("EhhEhhClick");
		}
	}
}
