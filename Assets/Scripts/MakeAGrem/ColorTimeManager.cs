using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.Playables;
using Unity​Engine.Rendering.PostProcessing;

public class ColorTimeManager : MonoBehaviour
{
    public PlayableDirector darkenAnimation;
    public PlayableDirector lightenAnimation;
    [SerializeField] GameObject iOSDarkenScreen;
    [SerializeField] PostProcessVolume postProcessVolume;
    [SerializeField] GameObject CafeSaki;

    [SerializeField] public int targetHour;
    [SerializeField] public int targetMinute;
    [SerializeField] public int gracePeriod;
    bool isDark;
    [HideInInspector] public bool buildForiOSMobile;
    System.DateTime localDate;

    // Start is called before the first frame update
    void Start()
    {
        // Immediately set darken (w/o animation) if entering the scene when not target time + grace period
        isDark = checkIfNotTime();
        if (isDark)
        {
            // Change iOSDarkenScreen obj if building for iOS/mobile
            if (buildForiOSMobile) {
                iOSDarkenScreen.GetComponent<CanvasGroup>().alpha = 0.95f;
            // Change post processing volume if building for normal builds
            } else {
                postProcessVolume.weight = 1.0f;
                CafeSaki.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.0f;
                CafeSaki.transform.GetChild(2).transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 0.0f;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // If Time is still 10:10 (not dark yet)
        if (!isDark)
        {
            // Try to trigger darken
            triggerDarkenfNotTime();
        } else {
            // Try to trigger lighten
            triggeLightenIfTime();
        }
    }

    void triggerDarkenfNotTime()
    {
        // Check if it is not 10:10
        isDark = checkIfNotTime();
        if (isDark)
        {
            // Trigger darkness animation
            darkenAnimation.Play();
        }
    }

    void triggeLightenIfTime()
    {
        // Check if it is not 10:10
        isDark = checkIfNotTime();
        if (!isDark)
        {
            // Trigger lighten animation
            lightenAnimation.Play();
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
