using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif
using System;

public class AndroidNotifications : MonoBehaviour
{
    
    // Request authorization to send notifications
    public void RequestAuthorization()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATION");
        }
#endif
    }

    // Register a notificaiton channel
    public void RegisterNotificationChannel(string id, string name, string description)
    {
#if UNITY_ANDROID
        var channel = new AndroidNotificationChannel
        {
            Id = id,
            Name = name,
            Importance = Importance.High,
            EnableVibration = true,
            Description = description,
            LockScreenVisibility = LockScreenVisibility.Public
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
#endif
    }

    // Set up notification template
    public void SendNotification(string title, string text, DateTime fireTime, TimeSpan repeatInterval, string channelID)
    {
#if UNITY_ANDROID
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = fireTime;
        notification.RepeatInterval = repeatInterval;
        notification.SmallIcon = "icon_1";
        notification.LargeIcon = "icon_0";

        AndroidNotificationCenter.SendNotification(notification, channelID);
#endif
    }
}

