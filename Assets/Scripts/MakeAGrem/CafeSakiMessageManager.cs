using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CafeSakiMessageManager : MonoBehaviour
{
    // Cafe Characters
    [SerializeField] List<CafeCharacter> cafeCharacters;

    // Cafe Background Game Object
    [SerializeField] GameObject cafeBackgroundLight;
    [SerializeField] GameObject cafeBackgroundDark;
    
    // Message Assets
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Image messageTextBackground;
    [SerializeField] Image messageTextBackgroundDark;

    // Scene Color and Time Objects
    [SerializeField] ColorTimeManager colorTimeManager;
    static System.DateTime localDate = System.DateTime.Now;
    static System.Random randNumGen = new System.Random();
    bool isProperTime;


    // Start is called before the first frame update
    void Start()
    {
        // Set saki message
        isProperTime = checkIfProperTime();
        setCafeMessageAndBackground();
    }
    void Update()
    {
        // check if exiting or entering target time (if so, change message accordingly)
        bool updatedIsProperTime = checkIfProperTime();
        if (isProperTime != updatedIsProperTime)
        {
            isProperTime = updatedIsProperTime;
            setCafeMessageAndBackground();
        }
    }
    bool checkIfProperTime()
    {
        localDate = System.DateTime.Now;

        // Return true if time is target time or within grace period after target time
        return (localDate.Hour % 12 == colorTimeManager.targetHour && localDate.Minute >= colorTimeManager.targetMinute && localDate.Minute <= colorTimeManager.targetMinute + colorTimeManager.gracePeriod);
    }

    void setCafeMessageAndBackground()
    {
        // Get random character if it's proper time, otherwise, default to cafe saki (index 0)
        CafeCharacter character = (checkIfProperTime()) ? cafeCharacters[randNumGen.Next(0, cafeCharacters.Count)] : cafeCharacters[0];

        // set up message and background
        character.setBackgroundSprite(cafeBackgroundLight.GetComponent<Image>(), cafeBackgroundDark.GetComponent<Image>());
        character.setRandomMessage(checkIfProperTime(), messageText, messageTextBackground, messageTextBackgroundDark);
    }
}

