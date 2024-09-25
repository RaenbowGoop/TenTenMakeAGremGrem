using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MultiplierCombo
{
    public CosmeticSet set;
    public float multiplier;
}

public class GremGachaManager : MonoBehaviour
{
    // Player Statistic Objects
    SetStatisticManager setStatisticManager;

    // ColorTimeManager
    [SerializeField] ColorTimeManager colorTimeManager;

    // Gacha Results
    [HideInInspector] public CosmeticSet head;
    [HideInInspector] public CosmeticSet torso;
    [HideInInspector] public CosmeticSet legs;
    [HideInInspector] public CosmeticSet shoes;
    [HideInInspector] public CosmeticSet backPiece;

    // Stats
    [HideInInspector] public int basePointTotal;
    [HideInInspector] public float multiplierTotal;
    [HideInInspector] public int totalPoints;
    [HideInInspector] public List<MultiplierCombo> multipierCombos;

    // Cosmetic Set Database
    [SerializeField] private CosmeticSetDatabase csDatabase;
    private Dictionary<CosmeticSet, int> csDict;

    // Rarity Rates (Percentage)
    [SerializeField] private int rateOHMYGOOP;
    [SerializeField] private int rateCriminal;
    [SerializeField] private int rateContraband;
    [SerializeField] private int rateOffensive;
    
    // Multipliers
    [SerializeField] private float flatOMGoopBonus;

    [SerializeField] private float twoPcSetBonus;
    [SerializeField] private float threePcSetBonus;
    [SerializeField] private float fourPcSetBonus;
    [SerializeField] private float fivePcSetBonus;

    [SerializeField] private float offensiveBonusAmplifier;
    [SerializeField] private float contrabandBonusAmplifier;
    [SerializeField] private float criminalBonusAmplifier;

    // Random Num Generator
    private System.Random randNumGen;

    // Start is called before the first frame update
    void Start()
    {
        // Find setStatisticManager
        setStatisticManager = GameObject.FindWithTag("Player").GetComponent<SetStatisticManager>();

        // Instantiate csDict
        csDict = new Dictionary<CosmeticSet, int>();

        // Set up Random Number Generator
        randNumGen = new System.Random();

        // Roll Set Pieces
        generateSet();

        // Calculate Base Points
        basePointTotal = head.setHeadStats + torso.setTorsoStats + legs.setLegsStats + shoes.setShoesStats + backPiece.setBackPieceStats;

        // Evaluate Combos
        multipierCombos = new List<MultiplierCombo>();
        multiplierTotal = evaluatePossibleCombos();

        // Calculate Total Points
        totalPoints = (int)(basePointTotal * multiplierTotal);

        // record set rolls (increment set piece counters)
        setStatisticManager.incrementSetPieceCount(head.setName, setPieceType.HEAD);
        setStatisticManager.incrementSetPieceCount(torso.setName, setPieceType.TORSO);
        setStatisticManager.incrementSetPieceCount(legs.setName, setPieceType.LEGS);
        setStatisticManager.incrementSetPieceCount(shoes.setName, setPieceType.SHOES);
        setStatisticManager.incrementSetPieceCount(backPiece.setName, setPieceType.BACKPIECE);

        // if the roll was made during the 10:10 time frame, increment counter in setStatisticManager
        if (!colorTimeManager.isDark)
        {
            // increment legal grem count
            setStatisticManager.incrementLegalGremCounter();
        }
        else
        {
            // increment illegal grem count
            setStatisticManager.incrementIllegalGremCounter();
        }

        // Check if rolled grem grem contest the highest and lowest scores
        setStatisticManager.contestExtremeScores(totalPoints, head.setName, torso.setName, legs.setName, shoes.setName, backPiece.setName);

        // Save and Load results
        setStatisticManager.SerializeJson();
    }

    // rolls a random rarity
    rarity getRarityRoll()
    {
        // Generates number from 1 to 100
        int result = randNumGen.Next(1, 101);

        if (result <= rateOffensive) { return rarity.Offensive; }
        else if (result <= rateOffensive + rateContraband) { return rarity.Contraband; }
        else if (result <= rateOffensive + rateContraband + rateCriminal) { return rarity.Criminal;  }
        return rarity.OhMyGOOP;
    }

    // rolls random item with random rarity
    CosmeticSet getRandomItem(List<CosmeticSet> cosmeticItems)
    {
        // roll rarity
        rarity rarityRoll = getRarityRoll();

        // filter items in cosmeticItems based on rarity
        List<CosmeticSet> filteredCosmeticItems = new List<CosmeticSet>();
        foreach (CosmeticSet set in cosmeticItems)
        {
            if (set.setRarity == rarityRoll) 
            {
                filteredCosmeticItems.Add(set); 
            }
        }

        // select item in filteredCosmeticItems
        int result = randNumGen.Next(0, filteredCosmeticItems.Count);
        return filteredCosmeticItems[result];
    }

    void logItemIntoDict(CosmeticSet cosmeticSet) 
    {
        // If cosmetic set is in the dictionary, increment item count
        if (csDict.ContainsKey(cosmeticSet)) { ++csDict[cosmeticSet]; }
        // Otherwise, add cosmeticSet to dict and set item count to 1
        else { csDict[cosmeticSet] = 1; }
    }

    // Randomly generate Set pieces
    void generateSet()
    {
        // Generate Head
        head = getRandomItem(csDatabase.cosmeticSetsWithHeads);
        logItemIntoDict(head);

        // Generate Torso
        torso = getRandomItem(csDatabase.cosmeticSetsWithTorso);
        logItemIntoDict(torso);

        // Generate Legs
        legs = getRandomItem(csDatabase.cosmeticSetsWithLegs);
        logItemIntoDict(legs);

        // Generate Shoes
        shoes = getRandomItem(csDatabase.cosmeticSetsWithShoes);
        logItemIntoDict(shoes);

        // Generate BackPiece
        backPiece = getRandomItem(csDatabase.cosmeticSetsWithBackPiece);
        logItemIntoDict(backPiece);

    }

    // Evalute all combos and multipliers. Returns total multiplier caclulater
     float evaluatePossibleCombos()
    {
        float totalMultiplier = 1;
        foreach (KeyValuePair<CosmeticSet, int> entry in csDict)
        {
            // Always take OMGOOP pieces as combos
            if (entry.Key.setRarity == rarity.OhMyGOOP)
            {
                for (int i = 0; i < entry.Value; i++)
                {
                    // OhMyGOOP Pieces have a static flat bonus
                    multipierCombos.Add(new MultiplierCombo() { set = entry.Key, multiplier = flatOMGoopBonus });

                    // Update total Multiplier
                    totalMultiplier += flatOMGoopBonus;
                }
            } else {
                // Check if count of set pieces is at least 2
                if (entry.Value >= 2)
                {
                    float multiplier;

                    // Determine set mutiplier based on num of pieces
                    switch (entry.Value)
                    {
                        case 2:
                            multiplier = twoPcSetBonus;
                            break;
                        case 3:
                            multiplier = threePcSetBonus;
                            break;
                        case 4:
                            multiplier = fourPcSetBonus;
                            break;
                        default:
                            // 5 piece set is default
                            multiplier = fivePcSetBonus;
                            break;
                    }

                    // Apply Set rarity bonus
                    switch (entry.Key.setRarity)
                    {
                        case rarity.Criminal:
                            multiplier *= criminalBonusAmplifier;
                            break;
                        case rarity.Contraband:
                            multiplier *= contrabandBonusAmplifier;
                            break;
                        default:
                            // rarity.Offensive Rarity by default
                            multiplier *= offensiveBonusAmplifier;
                            break;
                    }
                    // Store multiplier
                    multipierCombos.Add(new MultiplierCombo() { set = entry.Key, multiplier = multiplier });

                    // Update totalMultiplier
                    totalMultiplier += multiplier;
                }
            }
        }

        return totalMultiplier;
    }
}
