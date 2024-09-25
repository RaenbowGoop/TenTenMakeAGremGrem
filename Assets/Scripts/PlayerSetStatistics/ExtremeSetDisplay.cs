using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtremeSetDisplay : MonoBehaviour
{
    // Cosmetic Set Database
    [SerializeField] CosmeticSetDatabase csdb;

    // Dummy Gooper for placeholding
    [SerializeField] CosmeticSet dummyGooper;

    // Set Statistics
    SetStatisticManager setStatisticManager;

    // Grem Display Game Objects
    [SerializeField] GameObject gremModel;
    [SerializeField] GameObject gremDetails;

    // Cosmetic Sets for Highest Score
    GremCapsule highestGrem;
    CosmeticSet headHigh;
    CosmeticSet torsoHigh;
    CosmeticSet legsHigh;
    CosmeticSet shoesHigh;
    CosmeticSet backPieceHigh;

    // Cosmetic Sets for Lowest Score
    GremCapsule lowestGrem;
    CosmeticSet headLow;
    CosmeticSet torsoLow;
    CosmeticSet legsLow;
    CosmeticSet shoesLow;
    CosmeticSet backPieceLow;

    // Start is called before the first frame update
    void Start()
    {
        // Find Set StatisticManager
        setStatisticManager = GameObject.FindWithTag("Player").GetComponent<SetStatisticManager>();

        // Substitute dummy grem grem if not grems have been rolled before
        if (!setStatisticManager.highestScoreGrem.HasGrem || !setStatisticManager.lowestScoreGrem.HasGrem)
        {
            // Set Grem Capsules for Scores + multiplier
            highestGrem = new GremCapsule(0, 0, 0.0f, "DUMMY GOOPER", "DUMMY GOOPER", "DUMMY GOOPER", "DUMMY GOOPER", "DUMMY GOOPER", true);
            lowestGrem = highestGrem;

            // Set Cosmetic Sets for highest grem
            headHigh = dummyGooper;
            torsoHigh = dummyGooper;
            legsHigh = dummyGooper;
            shoesHigh = dummyGooper;
            backPieceHigh = dummyGooper;

            // Set Cosmetic Sets for lowest grem
            headLow = dummyGooper;
            torsoLow = dummyGooper;
            legsLow = dummyGooper;
            shoesLow = dummyGooper;
            backPieceLow = dummyGooper;

        } else {
            // Create dictionary for cosmetic set for searching
            Dictionary<string, CosmeticSet> cosmeticSetDictionary = new Dictionary<string, CosmeticSet>();
            foreach (CosmeticSet set in csdb.cosmeticSets)
            {
                cosmeticSetDictionary[set.setName] = set;
            }

            // Set Cosmetic Sets for highest grem
            highestGrem = setStatisticManager.highestScoreGrem;
            headHigh = cosmeticSetDictionary[highestGrem.Head];
            torsoHigh = cosmeticSetDictionary[highestGrem.Torso];
            legsHigh = cosmeticSetDictionary[highestGrem.Legs];
            shoesHigh = cosmeticSetDictionary[highestGrem.Shoes];
            backPieceHigh = cosmeticSetDictionary[highestGrem.BackPiece];

            // Set Cosmetic Sets for lowest grem
            lowestGrem = setStatisticManager.lowestScoreGrem;
            headLow = cosmeticSetDictionary[lowestGrem.Head];
            torsoLow = cosmeticSetDictionary[lowestGrem.Torso];
            legsLow = cosmeticSetDictionary[lowestGrem.Legs];
            shoesLow = cosmeticSetDictionary[lowestGrem.Shoes];
            backPieceLow = cosmeticSetDictionary[lowestGrem.BackPiece];

        }

        // display Highest grem initially
        displayHighestScoreGrem();
    }


    void displayGrem(CosmeticSet head, CosmeticSet torso, CosmeticSet legs, CosmeticSet shoes, CosmeticSet backPiece, GremCapsule gremCapsule)
    {
        // Display Assets in Model
        // Head
        gremModel.transform.GetChild(16).GetComponentInChildren<Image>().sprite = head.setHeadSuperFront;
        gremModel.transform.GetChild(13).GetComponentInChildren<Image>().sprite = head.setHeadFront;
        gremModel.transform.GetChild(1).GetComponentInChildren<Image>().sprite = head.setHeadBack;

        // Torso
        gremModel.transform.GetChild(15).GetComponentInChildren<Image>().sprite = torso.setTorsoSuperFront;
        gremModel.transform.GetChild(12).GetComponentInChildren<Image>().sprite = torso.setTorsoFront;
        gremModel.transform.GetChild(9).GetComponentInChildren<Image>().sprite = torso.setTorsoMiddle;
        gremModel.transform.GetChild(6).GetComponentInChildren<Image>().sprite = torso.setTorsoBack;
        gremModel.transform.GetChild(3).GetComponentInChildren<Image>().sprite = torso.setTorsoSuperBack;

        // Legs
        gremModel.transform.GetChild(14).GetComponentInChildren<Image>().sprite = legs.setLegsSuperFront;
        gremModel.transform.GetChild(10).GetComponentInChildren<Image>().sprite = legs.setLegsFront;
        gremModel.transform.GetChild(7).GetComponentInChildren<Image>().sprite = legs.setLegsMiddle;
        gremModel.transform.GetChild(4).GetComponentInChildren<Image>().sprite = legs.setLegsBack;

        // Shoes
        gremModel.transform.GetChild(11).GetComponentInChildren<Image>().sprite = shoes.setShoesFront;
        gremModel.transform.GetChild(8).GetComponentInChildren<Image>().sprite = shoes.setShoesMiddle;
        gremModel.transform.GetChild(5).GetComponentInChildren<Image>().sprite = shoes.setShoesBack;

        // BackPiece
        gremModel.transform.GetChild(17).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceFront;
        gremModel.transform.GetChild(2).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceMiddle;
        gremModel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceBack;

        // DISPLAY SET POINTS AND DETAILS
        gremDetails.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gremCapsule.Score.ToString("N0") + " pts";
        gremDetails.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gremCapsule.BasePoints.ToString("N0") + " pts";
        gremDetails.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + gremCapsule.Multiplier.ToString();
    }

    public void displayHighestScoreGrem()
    {
        displayGrem(headHigh, torsoHigh, legsHigh, shoesHigh, backPieceHigh, highestGrem);
    }

    public void displayLowestScoreGrem()
    {
        displayGrem(headLow, torsoLow, legsLow, shoesLow, backPieceLow, lowestGrem);
    }
}
