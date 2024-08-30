using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CafeSakiMessageManager : MonoBehaviour
{
    [SerializeField] List<string> cafeSakiLines;
    [SerializeField] TextMeshProUGUI messageText;

    // Start is called before the first frame update
    void Start()
    {
        // Set up Random Number Generator
        System.Random randNumGen = new System.Random();

        // Set random line to message box
        int lineIndex = randNumGen.Next(0, cafeSakiLines.Count);
        messageText.text = cafeSakiLines[lineIndex];
    }
}
