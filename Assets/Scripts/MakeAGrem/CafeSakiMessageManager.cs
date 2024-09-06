using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class CafeSakiMessageManager : MonoBehaviour
{
    [SerializeField] List<string> cafeSakiLinesDuringTime;
    [SerializeField] List<string> cafeSakiLinesNotDuringTime;
    [SerializeField] List<string> cafeSakiLinesRareLines;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] ColorTimeManager colorTimeManager;
    System.DateTime localDate;
    System.Random randNumGen;


    // Start is called before the first frame update
    void Start()
    {
        // Setting up Date Time obj
        localDate = System.DateTime.Now;

        // Set up Random Number Generator
        randNumGen = new System.Random();

        // determine which list of messages to choose from
        if (checkForRareLine())
        {
            int lineIndex = randNumGen.Next(0, cafeSakiLinesRareLines.Count);
            messageText.text = cafeSakiLinesRareLines[lineIndex];
        } else {
            if (checkIfProperTime())
            {
                // Set random line to message box
                int lineIndex = randNumGen.Next(0, cafeSakiLinesDuringTime.Count);
                messageText.text = cafeSakiLinesDuringTime[lineIndex];
            }
            else
            {
                // Set random line to message box
                int lineIndex = randNumGen.Next(0, cafeSakiLinesNotDuringTime.Count);
                messageText.text = cafeSakiLinesNotDuringTime[lineIndex];
            }
        }
    }

    bool checkIfProperTime()
    {
        // Return true if time is target time or within grace period after target time
        if (localDate.Hour % 12 == colorTimeManager.targetHour)
        {
            return true;
        }
        if (localDate.Minute > colorTimeManager.targetMinute && localDate.Minute < colorTimeManager.targetMinute + colorTimeManager.gracePeriod)
        {
            return true;
        }

        // Return false not during target time + grace period
        return false;
    }

    bool checkForRareLine()
    {
        int randNum = randNumGen.Next(1, 667);

        // Chip and Pondo were here
        if (randNum == 246)
        {
            return true;
        }
        return false;
    }
}
