using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using System;

public class NotificationController : MonoBehaviour
{
    [SerializeField] AndroidNotifications androidNotifications;

    // Start is called before the first frame update
    private void Start()
    {
        // set Up Notification Channel
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel();

        // Set Up Make A Grem Notifications
        AndroidNotificationCenter.CancelAllNotifications();

        // Time of notification fire
        System.DateTime now = System.DateTime.Now;

        System.DateTime notificationTimePrefire = new System.DateTime(now.Year, now.Month, now.Day, 10, 05, 0);
        System.DateTime notificationTime = new System.DateTime(now.Year, now.Month, now.Day, 10, 10, 0);

        // TimeSpan of repeat notification
        System.TimeSpan notificationRepeatInterval = new System.TimeSpan(12, 0, 0);

        // 5 minutes until Make A Grem
        androidNotifications.SendNotification("10:10 Make A Grem", "5 Minutes Until Make a Grem!", notificationTimePrefire, notificationRepeatInterval);

        // 10:10 Make A Grem time
        androidNotifications.SendNotification("10:10 Make A Grem", "It's Time To Make A Grem! GO GO GO!", notificationTime, notificationRepeatInterval);
    }
}
