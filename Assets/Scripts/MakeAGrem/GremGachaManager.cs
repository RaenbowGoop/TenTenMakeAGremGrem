using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GremGachaManager : MonoBehaviour
{
    // Gacha Results
    [HideInInspector] public CosmeticSet head;
    [HideInInspector] public CosmeticSet torso;
    [HideInInspector] public CosmeticSet legs;
    [HideInInspector] public CosmeticSet shoes;
    [HideInInspector] public CosmeticSet backPiece;

    // Cosmetic Set Database
    [SerializeField] private CosmeticSetDatabase csDatabase;
    public Dictionary<CosmeticSet, int> csDict;

    // Rarity Rates (Percentage)
    [SerializeField] private int rateOHMYGOOP;
    [SerializeField] private int rateCriminal;
    [SerializeField] private int rateBarelyLegal;
    [SerializeField] private int rateOffensive;

    // Random Num Generator
    private System.Random randNumGen;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate csDict
        csDict = new Dictionary<CosmeticSet, int>();

        // Set up Random Number Generator
        randNumGen = new System.Random();

        // Roll Set Pieces
        generateSet();
    }

    // rolls a random rarity
    rarity getRarityRoll()
    {
        // Generates number from 1 to 100
        int result = randNumGen.Next(1, 101);
        Debug.Log("Rarity Roll: " + result.ToString());

        if (result <= rateOffensive) { return rarity.Offensive; }
        else if (result <= rateOffensive + rateBarelyLegal) { return rarity.BarelyLegal; }
        else if (result <= rateOffensive + rateBarelyLegal + rateCriminal) { return rarity.Criminal;  }
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
        Debug.Log(result);
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
    private void generateSet()
    {
        // Generate Head
        head = getRandomItem(csDatabase.cosmeticSetsWithHeads);
        logItemIntoDict(head);
        Debug.Log(head.setName);

        // Generate Torso
        torso = getRandomItem(csDatabase.cosmeticSetsWithTorso);
        logItemIntoDict(torso);
        Debug.Log(torso.setName);

        // Generate Legs
        legs = getRandomItem(csDatabase.cosmeticSetsWithLegs);
        logItemIntoDict(legs);
        Debug.Log(legs.setName);

        // Generate Shoes
        shoes = getRandomItem(csDatabase.cosmeticSetsWithShoes);
        logItemIntoDict(shoes);
        Debug.Log(shoes.setName);

        // Generate BackPiece
        backPiece = getRandomItem(csDatabase.cosmeticSetsWithBackPiece);
        logItemIntoDict(backPiece);
        Debug.Log(backPiece.setName);

    }

}
