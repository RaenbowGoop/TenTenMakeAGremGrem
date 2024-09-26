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

public struct GremCapsule
{
    public GremCapsule (int score, int basePoints, float multiplier, string head, string torso, string legs, string shoes, string backPiece, bool hasGrem)
    {
        HasGrem = hasGrem;
        Score = score;
        BasePoints = basePoints;
        Multiplier = multiplier;
        Head = head;
        Torso = torso;
        Legs = legs;
        Shoes = shoes;
        BackPiece = backPiece;
    }

    public int Score { get; set; }

    public int BasePoints { get; set; }
    public float Multiplier { get; set; }
    public bool HasGrem { get; set; }
    public string Head { get; set; }
    public string Torso { get; set; }
    public string Legs { get; set; }
    public string Shoes { get; set; }
    public string BackPiece { get; set; }
}

public class SetStatisticManager : MonoBehaviour
{
    // Cosmetic Set database
    [SerializeField] CosmeticSetDatabase csdb;

    // Dictionary to track how many times a piece has been rolled
    public Dictionary<string, SetPieceCounter> setRollStatistics;

    // Number of Legal Grems rolled
    [HideInInspector] public int legalGremsMade;
    // Number of illegal Grems rolled
    [HideInInspector] public int illegalGremsMade;

    // highest score grem grem
    public GremCapsule highestScoreGrem;

    // lowest score grem grem
    public GremCapsule lowestScoreGrem;


    // data service object to save/load data
    private IDataService dataService = new JsonDataService();
    public void SerializeJson()
    {
        // Successful player roll statistics Save 
        if (dataService.SaveData("/player-roll-statistics.json", setRollStatistics)) {   
            // Attempt to Load data
            try
            {
                setRollStatistics = dataService.LoadData<Dictionary<string, SetPieceCounter>>("/player-roll-statistics.json");
            } catch (Exception e)
            {
                Debug.LogError($"Could not load data due to: {e.Message} {e.StackTrace}");
            }
        } 
        else
        {
            Debug.LogWarning($"Could not save data!");
        }

        // Successful legal grems made counter Save 
        if (dataService.SaveData("/legal-grem-statistics.json", legalGremsMade))
        {
            // Attempt to Load data
            try
            {
                legalGremsMade = dataService.LoadData<int>("/legal-grem-statistics.json");
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not load data due to: {e.Message} {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogWarning($"Could not save data!");
        }

        // Successful illegal grems made counter Save 
        if (dataService.SaveData("/illegal-grem-statistics.json", illegalGremsMade))
        {
            // Attempt to Load data
            try
            {
                illegalGremsMade = dataService.LoadData<int>("/illegal-grem-statistics.json");
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not load data due to: {e.Message} {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogWarning($"Could not save data!");
        }

        // Successfull highest score grem save
        if (dataService.SaveData("/highest-score-grem-statistic.json", highestScoreGrem))
        {
            // Attempt to Load data
            try
            {
                highestScoreGrem = dataService.LoadData<GremCapsule>("/highest-score-grem-statistic.json");
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not load data due to: {e.Message} {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogWarning($"Could not save data!");
        }

        // Successfull lowest score grem save
        if (dataService.SaveData("/lowest-score-grem-statistic.json", lowestScoreGrem))
        {
            // Attempt to Load data
            try
            {
                lowestScoreGrem = dataService.LoadData<GremCapsule>("/lowest-score-grem-statistic.json");
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not load data due to: {e.Message} {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogWarning($"Could not save data!");
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // SetRollStatistic
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
        } catch {
            // If unable to load data, create fresh slate for stats
            setRollStatistics = new Dictionary<string, SetPieceCounter>();

            // insert all items with fresh counters (default counts = 0)
            foreach (CosmeticSet set in csdb.cosmeticSets)
            {
                setRollStatistics[set.setName] = new SetPieceCounter();
            }
        }

        // Legal Grems Made Counter
        try
        {
            // Attempt to retrieve legal grem roll statistics
            legalGremsMade = dataService.LoadData<int>("/legal-grem-statistics.json");
        }
        catch
        {
            // If unable to load data, create fresh counter
            legalGremsMade = 0;
        }

        // Illegal Grems Made Counter
        try
        {
            // Attempt to retrieve illegal grem roll statistics
            illegalGremsMade = dataService.LoadData<int>("/illegal-grem-statistics.json");
        }
        catch
        {
            // If unable to load data, create fresh counter
            illegalGremsMade = 0;
        }

        // Highest Score
        try
        {
            // Attempt to retrieve highest score grem roll statistics
            highestScoreGrem = dataService.LoadData<GremCapsule>("/highest-score-grem-statistic.json");
        }
        catch
        {
            // If unable to load data, insert empty grem capsule
            highestScoreGrem = new GremCapsule(0, 0, 0.0f, "N/A", "N/A", "N/A", "N/A", "N/A", false);
        }

        // Lowest Score
        try
        {
            // Attempt to retrieve lowest score grem roll statistics
            lowestScoreGrem = dataService.LoadData<GremCapsule>("/lowest-score-grem-statistic.json");
        }
        catch
        {
            // If unable to load data, insert empty grem capsule
            lowestScoreGrem = new GremCapsule(0, 0, 0.0f, "N/A", "N/A", "N/A", "N/A", "N/A", false);
        }

        // Save and Load
        SerializeJson();
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

    public void incrementLegalGremCounter()
    {
        legalGremsMade += 1;
    }

    public void incrementIllegalGremCounter()
    {
        illegalGremsMade += 1;
    }

    public void contestExtremeScores(int score, int basePoints, float multiplier, string head, string torso, string legs, string shoes, string backPiece)
    {
        GremCapsule contestant = new GremCapsule(score, basePoints, multiplier, head, torso, legs, shoes, backPiece, true);

        // Base Case: no grem score logged yet
        if (!highestScoreGrem.HasGrem || !lowestScoreGrem.HasGrem)
        {
            // set the highest and lowest grems to the contestant
            highestScoreGrem = contestant;
            lowestScoreGrem = contestant;
        }
        // Check if highest score yet
        else if (score > highestScoreGrem.Score)
        {
            highestScoreGrem = contestant;
        }
        // Check if highest score yet
        else if (score < lowestScoreGrem.Score)
        {
            lowestScoreGrem = contestant;
        }
    }
}
