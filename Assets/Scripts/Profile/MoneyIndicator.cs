using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoneyIndicator : MonoBehaviour, IPointerDownHandler
{
	public Text moneyText;

	private float currentMoney = 0;
	private int devHoldCount = 3;
	private int devHoldCountNeeded = 5;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (devHoldCount > devHoldCountNeeded)
		{
            OnScreenNotificationManager.instance.CreateNotification("Dev Mode: Hello There!", OnScreenNotificationType.Warning);
            OnScreenNotificationManager.instance.CreateNotification("Motherlode Activated", OnScreenNotificationType.Sucess);
			ProfileManager.instance.AddMoney(1000);
			AudioManager.instance.PlaySFX("MinigameStartSfx");
            devHoldCount = 0;
		}
		else
		{
			devHoldCount++;
			AudioManager.instance.PlaySFX("PopClick");
		}
	}

	void Update()
	{
		if (ProfileManager.instance != null)
		{
			float money = ProfileManager.instance.GetMoney();
			if (money != currentMoney)
			{
				currentMoney = money;
			}
		}

		UpdateMoney();
	}

	void UpdateMoney()
	{
		currentMoney = Mathf.Lerp(currentMoney, currentMoney, Time.deltaTime * 5);
		moneyText.text = NumberFormatter.FormatNumberWithThousandsSeparator(currentMoney);
	}
}
