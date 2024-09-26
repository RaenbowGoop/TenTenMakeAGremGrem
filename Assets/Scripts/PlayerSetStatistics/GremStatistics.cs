using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GremStatistics : MonoBehaviour
{
    [SerializeField] GameObject gremStatisticsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        // Find Set StatisticManager
        SetStatisticManager setStatisticManager = GameObject.FindWithTag("Player").GetComponent<SetStatisticManager>();

        // Display number of legal and illegal grems made
        gremStatisticsDisplay.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = setStatisticManager.legalGremsMade.ToString("N0");
        gremStatisticsDisplay.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = setStatisticManager.illegalGremsMade.ToString("N0");
    }
}
