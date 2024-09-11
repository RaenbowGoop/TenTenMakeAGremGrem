using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using System;

public class AndroidNotifications : MonoBehaviour
{
    // Request authorization to send notifications
    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATION");
        }
    }

    // Register a notificaiton channel
    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Make A Grem Time"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    // Set up notification template
    public void SendNotification(string title, string text, DateTime fireTime, TimeSpan repeatInterval)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = fireTime;
        notification.RepeatInterval = repeatInterval;
        notification.SmallIcon = "icon_1";
        notification.LargeIcon = "icon_0";

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }
}
