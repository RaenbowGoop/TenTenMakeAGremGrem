using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName = "New Cafe Character", menuName = "Cafe Character/New Cafe Character")]
public class CafeCharacter : ScriptableObject
{
    [SerializeField] string characterName;

    // Cafe Character Sprites
    [SerializeField] Sprite cafeBackgroundLight;
    [SerializeField] Sprite cafeBackgroundDark;

    // Lists of possible messages
    [SerializeField] List<string> linesDuringTime;
    [SerializeField] List<string> linesNotDuringTime;
    [SerializeField] List<string> rareLines;

    // Rare line rate
    [SerializeField] int specialNumber;  // 1 out of [specialNumber] chance to get rare line

    // Message Box Colors for Rare Lines
    [SerializeField] Color textColor;
    [SerializeField] Color lightBackgroundColor;
    [SerializeField] Color darkBackgroundColor;
    System.Random randNumGen = new System.Random();

    bool checkForRareLine()
    {
        int randNum = randNumGen.Next(1, specialNumber + 1);

        // Chip and Pondo were here
        return randNum == specialNumber;
    }
    public void setRandomMessage(bool isProperTime, TextMeshProUGUI messageText, Image messageTextBackground, Image messageTextBackgroundDark)
    {
        // determine which list of messages to choose from
        if (checkForRareLine())
        {
            // Change Text Box Appearance
            messageText.color = textColor;
            messageTextBackground.color = lightBackgroundColor;
            messageTextBackgroundDark.color = darkBackgroundColor;

            // Get Random Rare Line
            int lineIndex = randNumGen.Next(0, rareLines.Count);
            messageText.text = rareLines[lineIndex];

        }
        else
        {
            if (isProperTime)
            {
                // Get random line to message box
                int lineIndex = randNumGen.Next(0, linesDuringTime.Count);
                messageText.text = linesDuringTime[lineIndex];
            }
            else
            {
                // Get random line to message box
                int lineIndex = randNumGen.Next(0, linesNotDuringTime.Count);
                messageText.text = linesNotDuringTime[lineIndex];
            }
        }

    }

    public void setBackgroundSprite(Image cafeBackgroundLightImage, Image cafeBackgroundDarkImage)
    {
        cafeBackgroundLightImage.sprite = cafeBackgroundLight;
        cafeBackgroundDarkImage.sprite = cafeBackgroundDark;
    }
}
