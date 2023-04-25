using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyIndicator : MonoBehaviour
{
	public Text moneyText;

	private float currentMoney = 0;

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
        moneyText.text = currentMoney.ToString("F0");
    }
}
