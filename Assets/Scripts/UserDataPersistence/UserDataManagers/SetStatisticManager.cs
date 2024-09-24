using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public enum setPieceType { HEAD, TORSO, LEGS, SHOES, BACKPIECE};
public struct SetPieceCounter
{
    // Constructor
    public SetPieceCounter (int headCount = 0, int torsoCount = 0, int legsCount = 0, int shoesCount = 0, int backPieceCount = 0)
    {
        HeadCount = headCount;
        TorsoCount = torsoCount;
        LegsCount = legsCount;
        ShoesCount = shoesCount;
        BackPieceCount = backPieceCount;
    }

    // Getter functions
    public int HeadCount { get; set; }
    public int TorsoCount { get; set; }
    public int LegsCount { get; set; }
    public int ShoesCount { get; set; }
    public int BackPieceCount { get; set; }

}

public class SetStatisticManager : MonoBehaviour
{
    // Cosmetic Set database
    [SerializeField] CosmeticSetDatabase csdb;

    // Dictionary to track how many times a piece has been rolled
    Dictionary<string, SetPieceCounter> setRollStatistics;

    // data service object to save/load data
    private IDataService dataService = new JsonDataService();
    public void SerializeJson()
    {
        // Successful Save 
        if (dataService.SaveData("/player-roll-statistics.json", setRollStatistics)) {   
            // Attempt to Load data
            try
            {
                setRollStatistics = dataService.LoadData<Dictionary<string, SetPieceCounter>>("/player-roll-statistics.json");
            } catch (Exception e)
            {
                Debug.LogError($"Could not load data due to: {e.Message} {e.StackTrace}");
            }
        } else
        {
            Debug.LogWarning($"Could not save data!");
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            // Attempt to retrieve setRollStatistics
            setRollStatistics = dataService.LoadData<Dictionary<string, SetPieceCounter>>("/player-roll-statistics.json");

            // check if there are missing sets and add them
            foreach (CosmeticSet set in csdb.cosmeticSets)
            {
                // Add fresh set entry if set is not in setRollStatistics
                if (!setRollStatistics.ContainsKey(set.setName))
                {
                    setRollStatistics[set.setName] = new SetPieceCounter();
                }
            }

            // Save and Load
            SerializeJson();
        } catch {
            // If unable to load data, create fresh slate for stats
            setRollStatistics = new Dictionary<string, SetPieceCounter>();

            // insert all items with fresh counters (default counts = 0)
            foreach (CosmeticSet set in csdb.cosmeticSets)
            {
                setRollStatistics[set.setName] = new SetPieceCounter();
            }

            // Save and Load
            SerializeJson();
        }
    }

    public void incrementSetPieceCount( string setName, setPieceType setPieceType )
    {
        SetPieceCounter setPieceCounter = setRollStatistics[setName];
        switch (setPieceType)
        {
            case setPieceType.HEAD:
                setPieceCounter.HeadCount += 1;
                break;
            case setPieceType.TORSO:
                setPieceCounter.TorsoCount += 1;
                break;
            case setPieceType.LEGS:
                setPieceCounter.LegsCount += 1;
                break;
            case setPieceType.SHOES:
                setPieceCounter.ShoesCount += 1;
                break;
            default:
                setPieceCounter.BackPieceCount += 1;
                break;
        }

        // Update counter of set
        setRollStatistics[setName] = setPieceCounter;
    }
}
