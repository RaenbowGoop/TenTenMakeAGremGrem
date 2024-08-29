using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointDisplay : MonoBehaviour
{

    [SerializeField] GameObject basePointTextGroup;
    [SerializeField] GameObject multiplierTextGroup;
    [SerializeField] GameObject totalPointTextGroup;
    [SerializeField] List<GameObject> multiplierSlots;
    [SerializeField] GameObject stamps;

    [SerializeField] GremGachaManager gremGachaManager;

    [SerializeField] Sprite offensiveFrame;
    [SerializeField] Sprite barelyLegalFrame;
    [SerializeField] Sprite criminalFrame;
    [SerializeField] Sprite ohMyGoopFrame;

    // Points Stats
    int totalBasePoints;
    int totalMultiplier;

    IEnumerator Start()
    {
        // Wait for GremGachaManager to calculate results
        yield return new WaitForSeconds(1f);

        // Display Base Point Stats
        basePointDisplay();

        // Display Multiplier
        multiplierDisplay();

        // Display Total Points
        totalPointTextGroup.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gremGachaManager.totalPoints.ToString();

        // Display Icons of Pieces ROLLED
        displaySetPieceIcons();
    }

    // Returns frame base on rarity
    Sprite setStampFrame(rarity setRarity)
    {
        // set frame
        switch (setRarity)
        {
            case rarity.Offensive:
                return offensiveFrame;
            case rarity.BarelyLegal:
                return barelyLegalFrame;
            case rarity.Criminal:
                return criminalFrame;
            default:
                // rarity.OhMyGOOP Rarity by default
                return ohMyGoopFrame;
        }
    }

    void basePointDisplay()
    {
        // Set Base Points Total
        basePointTextGroup.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = gremGachaManager.basePointTotal.ToString() + " pts";
        // Set Base Points for Head
        basePointTextGroup.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = gremGachaManager.head.setHeadStats.ToString() + " pts";
        // Set Base Points for Torso
        basePointTextGroup.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gremGachaManager.torso.setTorsoStats.ToString() + " pts";
        // Set Base Points for Legs
        basePointTextGroup.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gremGachaManager.legs.setLegsStats.ToString() + " pts";
        // Set Base Points for Shoes
        basePointTextGroup.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gremGachaManager.shoes.setShoesStats.ToString() + " pts";
        // Set Base Points for Back Piece
        basePointTextGroup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gremGachaManager.backPiece.setBackPieceStats.ToString() + " pts";
    }

    void multiplierDisplay()
    {
        // Set Total Multiplier 
        multiplierTextGroup.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + gremGachaManager.multiplierTotal.ToString();

        GameObject currentSlot;
        int currentSlotIndex = 0;

        // set all combos on multiplier display
        foreach (MultiplierCombo entry in gremGachaManager.multipierCombos)
        {
            // Get current display slot
            currentSlot = multiplierSlots[currentSlotIndex];

            // Set Frame
            currentSlot.GetComponent<Image>().sprite = setStampFrame(entry.set.setRarity);

            // Set icon
            currentSlot.transform.GetChild(0).GetComponent<Image>().sprite = entry.set.setIcon;

            // Set multiplier
            currentSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + entry.multiplier.ToString();

            // increment slot index
            currentSlotIndex++;
        }
    }

    void displaySetPieceIcons()
    {
        // Set Stamp Icons and Frames
        stamps.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = gremGachaManager.head.setIcon;
        stamps.transform.GetChild(0).GetComponent<Image>().sprite = setStampFrame(gremGachaManager.head.setRarity);

        stamps.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = gremGachaManager.torso.setIcon;
        stamps.transform.GetChild(1).GetComponent<Image>().sprite = setStampFrame(gremGachaManager.torso.setRarity);

        stamps.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = gremGachaManager.legs.setIcon;
        stamps.transform.GetChild(2).GetComponent<Image>().sprite = setStampFrame(gremGachaManager.legs.setRarity);

        stamps.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = gremGachaManager.shoes.setIcon;
        stamps.transform.GetChild(3).GetComponent<Image>().sprite = setStampFrame(gremGachaManager.shoes.setRarity);

        stamps.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = gremGachaManager.backPiece.setIcon;
        stamps.transform.GetChild(4).GetComponent<Image>().sprite = setStampFrame(gremGachaManager.backPiece.setRarity);

    }
}
