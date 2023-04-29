using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenNotificationManager : MonoBehaviour
{
    public static OnScreenNotificationManager instance;

    public GameObject notificationPrefab;
    public Transform notificationParent;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void CreateNotification(string text, OnScreenNotificationType type = OnScreenNotificationType.Info)
    {
        GameObject notification = Instantiate(notificationPrefab, notificationParent);
        notification.GetComponent<OnScreenNotificationItem>().SetNotification(text, type);
    }
}
