﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.Playables;
using Unity​Engine.Rendering.PostProcessing;

public class ColorTimeManager : MonoBehaviour
{

    [SerializeField] PlayableDirector darknessAnimation;
    [SerializeField] PostProcessVolume postProcessVolume;
    [SerializeField] int targetHour;
    [SerializeField] int targetMinute;
    [SerializeField] int gracePeriod;
    bool isDark;
    System.DateTime localDate;

    // Start is called before the first frame update
    void Start()
    {
        // Immediately set darkness (w/o animation) if entering the scene when not target time + grace period
        isDark = checkIfNotTime();
        if (isDark)
        {
            postProcessVolume.weight = 1.0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // If Time is still 10:10 (not dark yet), will stop checking after animation is played once
        if (!isDark)
        {
            // Try to trigger darkness
            triggerDarknessIfNotTime();
        }
    }

    void triggerDarknessIfNotTime()
    {
        // Check if it is not 10:10
        isDark = checkIfNotTime();
        if (isDark)
        {
            // Trigger darkness animation
            darknessAnimation.Play();
        }
    }

    bool checkIfNotTime()
    {
        localDate = System.DateTime.Now;
        // Return true if time is not target time or within grace period after target time
        if (localDate.Hour % 12 != targetHour)
        {
            return true;
        }
        if (localDate.Minute < targetMinute || localDate.Minute > targetMinute + gracePeriod)
        {
            return true;
        }

        // Return false during target time + grace period
        return false;
    }
}