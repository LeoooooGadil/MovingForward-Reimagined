using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowAreYouManager : MonoBehaviour
{
	public static HowAreYouManager instance;

	public bool isHowAreYouOpen = false;

	public string lyfeCycleName = "HowAreYou";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		StartCoroutine(OpenHowAreYouFormCoroutine());
	}

	IEnumerator OpenHowAreYouFormCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		CheckLifeCycle();
	}

	void CheckLifeCycle()
	{
		LifeCycleItem thisLifeCycle = LifeCycleManager.instance.GetLifeCycleItem(lyfeCycleName);

		if (thisLifeCycle == null)
		{
			Debug.Log("No life cycle" + lyfeCycleName + " found, creating new life cycle");
			CreateNewLifeCycle();
			OpenHowAreYouForm();
			return;
		}

		if (thisLifeCycle.Envoke)
		{
			Debug.Log("Life cycle " + lyfeCycleName + " is envoke, opening form");
			OpenHowAreYouForm();
		}
	}

	void CreateNewLifeCycle()
	{
		int hours = 2;

		LifeCycleItem lifeCycleItem = new LifeCycleItem();
		lifeCycleItem.name = lyfeCycleName;
		lifeCycleItem.startTime = System.DateTime.Now + System.TimeSpan.FromHours(hours);
		lifeCycleItem.maxRepeatCount = -1;
		lifeCycleItem.repeatType = LifeCycleRepeatType.Custom;
		lifeCycleItem.customRepeatTime = System.TimeSpan.FromHours(hours).Seconds;

		LifeCycleManager.instance.AddLifeCycleItem(lifeCycleItem);

		SendNotification();
	}

	void SendNotification()
	{
		int hours = 2;

		System.DateTime in2Hours = System.DateTime.Now + System.TimeSpan.FromHours(hours);
		string[] notificationText = new string[4];
		notificationText[0] = "How are you?";
		notificationText[1] = "How are you feeling today?";
		notificationText[2] = "How are you doing today?";
		notificationText[3] = "How are things going?";
		int randomIndex = Random.Range(0, notificationText.Length);

		NotificationManager.instance.SendNotification("Checking Up", notificationText[randomIndex], in2Hours);
	}

	public void OpenHowAreYouForm()
	{
		LevelManager.instance.ChangeScene("How Are You", false);
		isHowAreYouOpen = true;
	}

	public void CloseHowAreYouForm()
	{
		if (isHowAreYouOpen == false) return;

		LevelManager.instance.RemoveScene("How Are You");
		isHowAreYouOpen = false;
	}
}
