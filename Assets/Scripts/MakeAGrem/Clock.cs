using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using TMPro;

public class Clock : MonoBehaviour
{
    System.DateTime clock;
    [SerializeField] TextMeshProUGUI timeText;
    Dictionary<int, string> months;


    // Start is called before the first frame update
    void Start()
    {
        // Store Months
        months = new Dictionary<int, string>();
        months[1] = "Jan.";
        months[2] = "Feb.";
        months[3] = "Mar.";
        months[4] = "Apr.";
        months[5] = "May";
        months[6] = "June";
        months[7] = "July";
        months[8] = "Aug.";
        months[9] = "Sept.";
        months[10] = "Oct.";
        months[11] = "Nov.";
        months[12] = "Dec.";

        updateDateTimeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        updateDateTimeDisplay();
    }

    void updateDateTimeDisplay()
    {
        clock = System.DateTime.Now;

        // Write Day of Week
        string dateTimeText = clock.DayOfWeek + ", ";

        // Write Month
        dateTimeText = dateTimeText + months[clock.Month] + " ";

        // Write Day
        dateTimeText = dateTimeText + clock.Day + ", ";

        // Write Year
        dateTimeText = dateTimeText + clock.Year + "  ";

        // Write Time
        dateTimeText += clock.ToLongTimeString();

        // Update Display
        timeText.text = dateTimeText;
    }
}
