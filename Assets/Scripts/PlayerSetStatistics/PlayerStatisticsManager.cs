using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.WSA;

public class CosmeticSetCountPair : System.IComparable<CosmeticSetCountPair>, System.IEquatable<CosmeticSetCountPair>
{
    public CosmeticSet set; 
    public int count;

    public CosmeticSetCountPair(CosmeticSet set, int count)
    {
        this.set = set;
        this.count = count;
    }

    public int CompareTo(CosmeticSetCountPair other)
    {
        if (other == null)
        {
            return 1;
        } else
        {
            // First compare count
            int countComparison = this.CompareByCount(other);
            if (countComparison == 0) {
                // Second compare rarity
                int rarityComparison = this.set.CompareByRarity(other.set);
                if (rarityComparison == 0) {
                    //lastly, compare name
                    return this.set.CompareByName(other.set);
                } else {
                    return rarityComparison;
                }
                // If counts are the same, compare name
            } else {
                return countComparison;
            }
        }
    }

    public bool Equals(CosmeticSetCountPair other)
    {
        return this.set.setName == other.set.setName;
    }

    private int CompareByCount(CosmeticSetCountPair other)
    {
        // Compare counts
        if (this.count > other.count)
        {
            return -1;
        }
        else if (this.count < other.count)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

public class PlayerStatisticsManager : MonoBehaviour
{
    // cosmetic set database
    [SerializeField] CosmeticSetDatabase csdb;

    // Ranking Background
    [SerializeField] GameObject rankingBackground;

    // Player set stat manager
    SetStatisticManager setStatisticManager;

    // Count rankings
    List<CosmeticSetCountPair> fullCosmeticSetCounts = new List<CosmeticSetCountPair>();
    List<CosmeticSetCountPair> headCosmeticSetCounts = new List<CosmeticSetCountPair>();
    List<CosmeticSetCountPair> torsoCosmeticSetCounts = new List<CosmeticSetCountPair>();
    List<CosmeticSetCountPair> legsCosmeticSetCounts = new List<CosmeticSetCountPair>();
    List<CosmeticSetCountPair> shoesCosmeticSetCounts = new List<CosmeticSetCountPair>();
    List<CosmeticSetCountPair> backPieceCosmeticSetCounts = new List<CosmeticSetCountPair>();

    // Displayed GameObjects
    Dictionary<CosmeticSet, GameObject> setsDisplayed = new Dictionary<CosmeticSet, GameObject>();

    // Ranking Prefab
    [SerializeField] GameObject setRankingSlot;

    // Ranking Parent GameObjects
    [SerializeField] GameObject rankingSlots;
    [SerializeField] GameObject rankingHeader;

    // Cosmetic Set Icons
    [SerializeField] Sprite offensiveIcon;
    [SerializeField] Sprite contrabandIcon;
    [SerializeField] Sprite criminalIcon;
    [SerializeField] Sprite ohMyGoopIcon;

    // Generic Set Type Icons
    [SerializeField] Sprite allPieceIcon;
    [SerializeField] Sprite headIcon;
    [SerializeField] Sprite torsoIcon;
    [SerializeField] Sprite legsIcon;
    [SerializeField] Sprite shoesIcon;
    [SerializeField] Sprite backPieceIcon;

    // ranking Backgrounds
    [SerializeField] Sprite allRankingBackground;
    [SerializeField] Sprite headRankingBackground;
    [SerializeField] Sprite torsoRankingBackground;
    [SerializeField] Sprite legsRankingBackground;
    [SerializeField] Sprite shoesRankingBackground;
    [SerializeField] Sprite backPieceRankingBackground;

    // Grem Display From Ranking Game Objects
    [SerializeField] GameObject GremDisplayObject;
    [SerializeField] GameObject mainGremModel;
    [SerializeField] GameObject gremNameCardDisplay;
    [SerializeField] GameObject gremName;
    [SerializeField] GameObject gremSetIcon;
    [SerializeField] GameObject gremPointsDisplay;
    [SerializeField] GameObject gremInfo;

    void Start()
    {
        // Find Set Statistic Manager
        setStatisticManager = GameObject.FindWithTag("Player").GetComponent<SetStatisticManager>();

        // Link Counts with Cosmetic Sets
        foreach (CosmeticSet set in csdb.cosmeticSets)
        {
            // Get Set counter
            SetPieceCounter setPieceCounter = setStatisticManager.setRollStatistics[set.setName];

            // LOG TOTAL ROLLS FROM SET
            // Compute total rolls of any piece from given set + Store entry in fullCosmeticSetCounts

            fullCosmeticSetCounts.Add(new CosmeticSetCountPair(set, setPieceCounter.HeadCount + setPieceCounter.TorsoCount + setPieceCounter.LegsCount + setPieceCounter.ShoesCount + setPieceCounter.BackPieceCount));

            // LOG ROLL COUNT OF HEAD PIECE (if available)
            if (set.hasHead)
            {
                headCosmeticSetCounts.Add(new CosmeticSetCountPair(set, setPieceCounter.HeadCount));
            }

            // LOG ROLL COUNT OF TORSO PIECE (if available)
            if (set.hasTorso)
            {
                torsoCosmeticSetCounts.Add(new CosmeticSetCountPair(set, setPieceCounter.TorsoCount)); 
            }

            // LOG ROLL COUNT OF LEG PIECE (if available)
            if (set.hasLegs)
            {
                legsCosmeticSetCounts.Add(new CosmeticSetCountPair(set, setPieceCounter.LegsCount));
            }

            // LOG ROLL COUNT OF SHOE PIECE (if available)
            if (set.hasShoes)
            {
                shoesCosmeticSetCounts.Add(new CosmeticSetCountPair(set, setPieceCounter.ShoesCount));
            }

            // LOG ROLL COUNT OF BACK PIECE (if available)
            if (set.hasBackPiece)
            {
                backPieceCosmeticSetCounts.Add(new CosmeticSetCountPair(set, setPieceCounter.BackPieceCount));
            }
        }

        // Sort All Lists 
        fullCosmeticSetCounts.Sort();
        headCosmeticSetCounts.Sort();
        torsoCosmeticSetCounts.Sort();
        legsCosmeticSetCounts.Sort();
        shoesCosmeticSetCounts.Sort();
        backPieceCosmeticSetCounts.Sort();

        // Display Total roll counts initially
        displayTotalSetCount();
    }

    void displayRankingBackground(Sprite background)
    {
        // Set ranking background
        rankingBackground.GetComponent<Image>().sprite = background;
    }

    void displayRankingHeader(Sprite icon)
    {
        rankingHeader.transform.GetChild(0).GetComponent<Image>().sprite = icon;
    }

    void displayRanking(List<CosmeticSetCountPair> setCounts)
    {
        // Clear out current display (destroy existing Gameobjects and clear our the dictionary setsDisplayed
        foreach (KeyValuePair<CosmeticSet, GameObject> set in setsDisplayed)
        {
            Destroy(set.Value);
        }
        setsDisplayed.Clear();

        // Update ranking Display with new setRankingSlots
        foreach (CosmeticSetCountPair pair in setCounts)
        {
            // Get Cosmetic Set
            CosmeticSet set = pair.set;

            // If set is not yet displayed or doesnt exists on display
            if (!setsDisplayed.ContainsKey(set))
            {
                // Instantiate new ranking slot in scene
                GameObject obj = Instantiate(setRankingSlot, Vector3.zero, Quaternion.identity, transform);

                // Set Object Parent to rankingSlots
                obj.transform.SetParent(rankingSlots.transform);

                // Set Scale of Object
                obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                // Set Sprites in Object
                short rarity = set.getRarity();
                if (rarity == 3) { obj.transform.GetChild(0).GetComponent<Image>().sprite = offensiveIcon; }
                else if (rarity == 2) { obj.transform.GetChild(0).GetComponent<Image>().sprite = contrabandIcon; }
                else if (rarity == 1) { obj.transform.GetChild(0).GetComponent<Image>().sprite = criminalIcon; }
                else { obj.transform.GetChild(0).GetComponent<Image>().sprite = ohMyGoopIcon; }

                // Set setIcon
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = set.setIcon;

                // Set setName
                obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = set.setName;

                // Set Roll Count
                obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pair.count.ToString();

                // Add Button Function to Display Grem details
                obj.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(() => displayGremFromRanking(set));

                // Track new Game object in setsDisplayed
                setsDisplayed.Add(set, obj);
            }

        }
    }

    public void displayTotalSetCount()
    {
        // Set Ranking Header
        displayRankingHeader(allPieceIcon);

        // Display Ranking entries
        displayRanking(fullCosmeticSetCounts);

        // Display Ranking Background
        displayRankingBackground(allRankingBackground);
    }

    public void displayHeadSetCount()
    {
        displayRankingHeader(headIcon);
        displayRanking(headCosmeticSetCounts);
        displayRankingBackground(headRankingBackground);
    }

    public void displayTorsoSetCount()
    {
        displayRankingHeader(torsoIcon);
        displayRanking(torsoCosmeticSetCounts);
        displayRankingBackground(torsoRankingBackground);
    }

    public void displayLegsSetCount()
    {
        displayRankingHeader(legsIcon);
        displayRanking(legsCosmeticSetCounts);
        displayRankingBackground(legsRankingBackground);
    }

    public void displayShoesSetCount()
    {
        displayRankingHeader(shoesIcon);
        displayRanking(shoesCosmeticSetCounts);
        displayRankingBackground(shoesRankingBackground);
    }

    public void displayBackPieceSetCount()
    {
        displayRankingHeader(backPieceIcon);
        displayRanking(backPieceCosmeticSetCounts);
        displayRankingBackground(backPieceRankingBackground);
    }

    public void displayGremFromRanking(CosmeticSet set)
    {
        // Display Grem on Main model
        // Head
        mainGremModel.transform.GetChild(16).GetComponentInChildren<Image>().sprite = set.setHeadSuperFront;
        mainGremModel.transform.GetChild(13).GetComponentInChildren<Image>().sprite = set.setHeadFront;
        mainGremModel.transform.GetChild(1).GetComponentInChildren<Image>().sprite = set.setHeadBack;

        // Torso
        mainGremModel.transform.GetChild(15).GetComponentInChildren<Image>().sprite = set.setTorsoSuperFront;
        mainGremModel.transform.GetChild(12).GetComponentInChildren<Image>().sprite = set.setTorsoFront;
        mainGremModel.transform.GetChild(9).GetComponentInChildren<Image>().sprite = set.setTorsoMiddle;
        mainGremModel.transform.GetChild(6).GetComponentInChildren<Image>().sprite = set.setTorsoBack;
        mainGremModel.transform.GetChild(3).GetComponentInChildren<Image>().sprite = set.setTorsoSuperBack;

        // Legs
        mainGremModel.transform.GetChild(14).GetComponentInChildren<Image>().sprite = set.setLegsSuperFront;
        mainGremModel.transform.GetChild(10).GetComponentInChildren<Image>().sprite = set.setLegsFront;
        mainGremModel.transform.GetChild(7).GetComponentInChildren<Image>().sprite = set.setLegsMiddle;
        mainGremModel.transform.GetChild(4).GetComponentInChildren<Image>().sprite = set.setLegsBack;

        // Shoes
        mainGremModel.transform.GetChild(11).GetComponentInChildren<Image>().sprite = set.setShoesFront;
        mainGremModel.transform.GetChild(8).GetComponentInChildren<Image>().sprite = set.setShoesMiddle;
        mainGremModel.transform.GetChild(5).GetComponentInChildren<Image>().sprite = set.setShoesBack;

        // BackPiece
        mainGremModel.transform.GetChild(17).GetComponentInChildren<Image>().sprite = set.setBackPieceFront;
        mainGremModel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = set.setBackPieceMiddle;
        mainGremModel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = set.setBackPieceBack;

        // Set Name Card
        // If set has head, display entire grem. If no head, just display Icon
        if (set.hasHead)
        {
            // Disable Set Icon
            gremNameCardDisplay.transform.GetChild(1).gameObject.SetActive(false);

            // Enable Set Model
            gremNameCardDisplay.transform.GetChild(0).gameObject.SetActive(true);

            // Display Set
            // Head
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(16).GetComponentInChildren<Image>().sprite = set.setHeadSuperFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(13).GetComponentInChildren<Image>().sprite = set.setHeadFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<Image>().sprite = set.setHeadBack;

            // Torso
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(15).GetComponentInChildren<Image>().sprite = set.setTorsoSuperFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(12).GetComponentInChildren<Image>().sprite = set.setTorsoFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(9).GetComponentInChildren<Image>().sprite = set.setTorsoMiddle;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(6).GetComponentInChildren<Image>().sprite = set.setTorsoBack;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Image>().sprite = set.setTorsoSuperBack;

            // Legs
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(14).GetComponentInChildren<Image>().sprite = set.setLegsSuperFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(10).GetComponentInChildren<Image>().sprite = set.setLegsFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(7).GetComponentInChildren<Image>().sprite = set.setLegsMiddle;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(4).GetComponentInChildren<Image>().sprite = set.setLegsBack;

            // Shoes
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(11).GetComponentInChildren<Image>().sprite = set.setShoesFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(8).GetComponentInChildren<Image>().sprite = set.setShoesMiddle;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(5).GetComponentInChildren<Image>().sprite = set.setShoesBack;

            // BackPiece
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(17).GetComponentInChildren<Image>().sprite = set.setBackPieceFront;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<Image>().sprite = set.setBackPieceMiddle;
            gremNameCardDisplay.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = set.setBackPieceBack;

        } else {
            // Disable Set Model
            gremNameCardDisplay.transform.GetChild(0).gameObject.SetActive(false);

            // Enable Set Icon
            gremNameCardDisplay.transform.GetChild(1).gameObject.SetActive(true);

            // Display Set Icon
            gremNameCardDisplay.transform.GetChild(1).GetComponentInChildren<Image>().sprite = set.setIcon;
        }

        // Display Set Name
        gremName.GetComponent<TextMeshProUGUI>().text = set.setName;

        // Display Set Icon
        gremSetIcon.transform.GetChild(0).GetComponent<Image>().sprite = set.setIcon;

        // Display Points ( displya N/A for pieces that the set does not have)
        if (set.hasHead)
        {
            gremPointsDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = set.setHeadStats.ToString("N0") + " pts";
        } else
        {
            gremPointsDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if (set.hasTorso)
        {
            gremPointsDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = set.setTorsoStats.ToString("N0") + " pts";
        }
        else
        {
            gremPointsDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if (set.hasLegs)
        {
            gremPointsDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = set.setLegsStats.ToString("N0") + " pts";
        }
        else
        {
            gremPointsDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if (set.hasShoes)
        {
            gremPointsDisplay.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = set.setShoesStats.ToString("N0") + " pts";
        }
        else
        {
            gremPointsDisplay.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if (set.hasBackPiece)
        {
            gremPointsDisplay.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = set.setBackPieceStats.ToString("N0") + " pts";
        }
        else
        {
            gremPointsDisplay.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "N/A";
        }
        

        // Display Rarity
        switch (set.setRarity)
        {
            case rarity.Offensive:
                gremInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RARITY: OFFENSIVE";
                break;
            case rarity.Contraband:
                gremInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RARITY: CONTRABAND";
                break;
            case rarity.Criminal:
                gremInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RARITY: CRIMINAL";
                break;
            default:
                gremInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "RARITY: OH MY GOOP";
                break;

        }

        // Display Description
        gremInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = set.setDescription;

        // Enable Grem Display
        GremDisplayObject.SetActive(true);

    }
}
