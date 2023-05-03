using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
	public static NotificationManager instance;

	private string channelID = "channel_id";
	private string channelName = "Default Channel";
	private string channelDescription = "Moving Forward notifications";

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		var channel = new AndroidNotificationChannel()
		{
			Id = channelID,
			Name = channelName,
			Description = channelDescription,
			Importance = Importance.High,
		};

		AndroidNotificationCenter.RegisterNotificationChannel(channel);
	}

	public void SendNotification(string title, string text, int delay)
	{
		var notification = new AndroidNotification();
		notification.Title = title;
		notification.ShowTimestamp = true;
		notification.Text = text;
		notification.SmallIcon = "default_icon";
		notification.FireTime = System.DateTime.Now.AddSeconds(delay);

		AndroidNotificationCenter.SendNotification(notification, channelID);

		Debug.Log("Notification sent");
	}

	public void SendNotification(string title, string text)
	{
		var notification = new AndroidNotification();
		notification.Title = title;
		notification.ShowTimestamp = true;
		notification.Text = text;
		notification.SmallIcon = "default_icon";
		notification.FireTime = System.DateTime.Now.AddSeconds(5);

		AndroidNotificationCenter.SendNotification(notification, channelID);

		Debug.Log("Notification sent");
	}

	public void SendNotification(string title, string text, string time)
	{
		var notification = new AndroidNotification();
		notification.Title = title;
		notification.Text = text;
		notification.SmallIcon = "default_icon";
		notification.FireTime = System.DateTime.Parse(time);

		AndroidNotificationCenter.SendNotification(notification, channelID);

		Debug.Log("Notification sent");
	}

	public void SendNotification(string title, string text, DateTime time)
	{
		var notification = new AndroidNotification();
		notification.Title = title;
		notification.Text = text;
		notification.SmallIcon = "default_icon";
		notification.FireTime = time;

		AndroidNotificationCenter.SendNotification(notification, channelID);

		Debug.Log("Notification sent");
	}

	public void CancelNotification()
	{
		AndroidNotificationCenter.CancelAllNotifications();
		Debug.Log("Notification cancelled");
	}

	public void CancelNotification(int id)
	{
		AndroidNotificationCenter.CancelNotification(id);
		Debug.Log("Notification cancelled");
	}
}
