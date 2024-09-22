using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class NotificationController : MonoBehaviour
{
#if UNITY_ANDROID
    [SerializeField] AndroidNotifications androidNotifications;
#endif

    // Start is called before the first frame update
    private void Start()
    {
#if UNITY_ANDROID
        // set Up Notification Channel
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel("grem_channel", "10:10 Make A Grem Notifications", "10:10 Make A Grem Warnings and Alerts");
        SetUpMakeAGremNotifications();
#endif
    }

    private void OnApplicationFocus(bool focus)
    {
#if UNITY_ANDROID
        if (focus)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
        }
#endif
    }

    private void SetUpMakeAGremNotifications()
    {
        // Only Run on Android
#if UNITY_ANDROID

        // Set Up Make A Grem Notifications
        AndroidNotificationCenter.CancelAllNotifications();

        // Time of notification fire
        System.DateTime now = System.DateTime.Now;

        System.DateTime notificationTimePrefireAM = new System.DateTime(now.Year, now.Month, now.Day, 10, 05, 0);
        System.DateTime notificationTimeAM = new System.DateTime(now.Year, now.Month, now.Day, 10, 10, 0);

        System.DateTime notificationTimePrefirePM = new System.DateTime(now.Year, now.Month, now.Day, 22, 05, 0);
        System.DateTime notificationTimePM = new System.DateTime(now.Year, now.Month, now.Day, 22, 10, 0);

        // Shift notification time depending on current time
        // 10 AM
        if (now.Hour == 10)
        {
            // If 10:05 passed, shift notification to next day
            if (now.Minute > 5)
            {
                notificationTimePrefireAM = notificationTimePrefireAM.AddDays(1);
            }
            // If 10:10 passed, shift notification to next day
            if (now.Minute > 10)
            {
                notificationTimeAM = notificationTimeAM.AddDays(1);
            }
        }
        // Past 10 AM
        else if (now.Hour > 10)
        {
            // Shift both AM notifications to the next day
            notificationTimePrefireAM = notificationTimePrefireAM.AddDays(1);
            notificationTimeAM = notificationTimeAM.AddDays(1);
        }

        // 10 PM
        if (now.Hour == 22)
        {
            // If 10:05 passed, shift notification to next day
            if (now.Minute > 5)
            {
                notificationTimePrefirePM = notificationTimePrefirePM.AddDays(1);
            }
            // If 10:10 passed, shift notification to next day
            if (now.Minute > 10)
            {
                notificationTimePM = notificationTimePM.AddDays(1);
            }
        }
        // Past 10 PM
        else if (now.Hour > 22)
        {
            // Shift both PM notifications to the next day
            notificationTimePrefirePM = notificationTimePrefirePM.AddDays(1);
            notificationTimePM = notificationTimePM.AddDays(1);
        }

        // TimeSpan of repeat notification (repeat every day)
        System.TimeSpan notificationRepeatInterval = TimeSpan.FromDays(1);

        // 5 minutes until Make A Grem (AM)
        androidNotifications.SendNotification("10:10 AM Make A Grem", "5 Minutes Until Make a Grem!", notificationTimePrefireAM, notificationRepeatInterval, "grem_channel");
        // 10:10 Make A Grem time (AM)
        androidNotifications.SendNotification("10:10 AM Make A Grem", "It's Time To Make A Grem! GO GO GO!", notificationTimeAM, notificationRepeatInterval, "grem_channel");

        // 5 minutes until Make A Grem (PM)
        androidNotifications.SendNotification("10:10 PM Make A Grem", "5 Minutes Until Make a Grem!", notificationTimePrefirePM, notificationRepeatInterval, "grem_channel");
        // 10:10 Make A Grem time (PM)
        androidNotifications.SendNotification("10:10 PM Make A Grem", "It's Time To Make A Grem! GO GO GO!", notificationTimePM, notificationRepeatInterval, "grem_channel");

#endif
    }
}

