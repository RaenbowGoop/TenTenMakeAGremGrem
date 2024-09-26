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
        AndroidNotificationCenter.Initialize();

        // Register Channels
        androidNotifications.RegisterNotificationChannel("grem_warning_channel", "10:10 Make A Grem 5 Minute Warning", "10:10 Make A Grem Warnings");
        androidNotifications.RegisterNotificationChannel("grem_alert_channel", "10:10 Make A Grem Alert", "10:10 Make A Grem Alerts");

        // Set up notifications
        SetUpMakeAGremNotifications();
#endif
    }

    private void OnApplicationFocus(bool focus)
    {
#if UNITY_ANDROID
        if (focus)
        {
            SetUpMakeAGremNotifications();
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

        System.DateTime notificationTimeWarningAM = new System.DateTime(now.Year, now.Month, now.Day, 10, 04, 55);
        System.DateTime notificationTimeAlertAM = new System.DateTime(now.Year, now.Month, now.Day, 10, 9, 55);

        // Shift notification time depending on current time
        // 10 AM
        if (now.Hour == 10)
        {
            // If 10:05 passed, shift notification to next day
            if (now.Minute >= 5)
            {
                notificationTimeWarningAM = notificationTimeWarningAM.AddHours(12);
            }
            // If 10:10 passed, shift notification to next day
            if (now.Minute >= 10)
            {
                notificationTimeAlertAM = notificationTimeAlertAM.AddHours(12);
            }
        }
        // Past 10 AM
        else if (now.Hour > 10)
        {
            // Shift both notifications to the next 12 hour cycle
            notificationTimeWarningAM = notificationTimeWarningAM.AddHours(12);
            notificationTimeAlertAM = notificationTimeAlertAM.AddHours(12);
        }

        // 10 PM
        if (now.Hour == 22)
        {
            // If 10:05 passed, shift notification to next day
            if (now.Minute >= 5)
            {
                notificationTimeWarningAM = notificationTimeWarningAM.AddHours(12);
            }
            // If 10:10 passed, shift notification to next day
            if (now.Minute >= 10)
            {
                notificationTimeAlertAM = notificationTimeAlertAM.AddHours(12);
            }
        }
        // Past 10 PM
        else if (now.Hour > 22)
        {
            // Shift both notifications to the next 12 hour cycle
            notificationTimeWarningAM = notificationTimeWarningAM.AddHours(12);
            notificationTimeAlertAM = notificationTimeAlertAM.AddHours(12);
        }

        // Schedule Warnings and Alerts for specified number of days
        int numberOfDays = 10;
        for (int day = 0; day < numberOfDays; day++)
        {
            // 5 minutes until Make A Grem (AM)
            androidNotifications.SendNotification("10:10 AM Make A Grem", "5 Minutes Until Make a Grem!", notificationTimeWarningAM.AddHours(day * 24), "grem_warning_channel");

            // 5 minutes until Make A Grem (PM)
            androidNotifications.SendNotification("10:10 PM Make A Grem", "5 Minutes Until Make a Grem!", notificationTimeWarningAM.AddHours(12 + day * 24), "grem_warning_channel");

            // 10:10 Make A Grem time (AM)
            androidNotifications.SendNotification("10:10 AM Make A Grem", "It's Time To Make A Grem! GO GO GO!", notificationTimeAlertAM.AddHours(day * 24), "grem_alert_channel");

            // 10:10 Make A Grem time (PM)
            androidNotifications.SendNotification("10:10 PM Make A Grem", "It's Time To Make A Grem! GO GO GO!", notificationTimeAlertAM.AddHours(12 + day * 24), "grem_alert_channel");
        }
#endif
    }
}

