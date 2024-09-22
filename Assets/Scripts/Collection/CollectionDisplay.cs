using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum displaySetting { ALL, HEAD, TORSO, LEGS, SHOES, BACKPIECE, NEW };

public class CollectionDisplay : MonoBehaviour
{
    [SerializeField] CosmeticSetDatabase csdb;
    Dictionary<CosmeticSet, GameObject> setsDisplayed = new Dictionary<CosmeticSet, GameObject>();

    // Prefab(s) for display
    [SerializeField] GameObject setFrame;
    [SerializeField] GameObject setModel;

    // Game Object Display
    [SerializeField] GameObject setDisplay;
    GameObject currentSet;

    // Cosmetic Set Icons
    [SerializeField] Sprite offensiveIcon;
    [SerializeField] Sprite contrabandIcon;
    [SerializeField] Sprite criminalIcon;
    [SerializeField] Sprite ohMyGoopIcon;

    // Filler Display Pieces
    [SerializeField] Sprite dummyHead;
    [SerializeField] Sprite plainTorso;
    [SerializeField] Sprite plainLegs;
    [SerializeField] Sprite transparentSprite;


    // Start is called before the first frame update
    void Start()
    {
        displayAllSets();
    }

    public void displayAllSets()
    {
        UpdateDisplay(displaySetting.ALL);
    }

    public void displaySetsWithHead()
    {
        UpdateDisplay(displaySetting.HEAD);
    }
    public void displaySetsWithTorso()
    {
        UpdateDisplay(displaySetting.TORSO);
    }
    public void displaySetsWithLegs()
    {
        UpdateDisplay(displaySetting.LEGS);
    }
    public void displaySetsWithShoes()
    {
        UpdateDisplay(displaySetting.SHOES);
    }
    public void displaySetsWithBackPiece()
    {
        UpdateDisplay(displaySetting.BACKPIECE);
    }
    public void displayNewSets()
    {
        UpdateDisplay(displaySetting.NEW);
    }

    private void UpdateDisplay(displaySetting displaySetting)
    {
        // Select which sets to display
        List<CosmeticSet> cosmeticSets;
        switch (displaySetting)
        {
            case displaySetting.ALL:
                cosmeticSets = csdb.cosmeticSets;
                break;
            case displaySetting.HEAD:
                cosmeticSets = csdb.cosmeticSetsWithHeads;
                break;
            case displaySetting.TORSO:
                cosmeticSets = csdb.cosmeticSetsWithTorso;
                break;
            case displaySetting.LEGS:
                cosmeticSets = csdb.cosmeticSetsWithLegs;
                break;
            case displaySetting.SHOES:
                cosmeticSets = csdb.cosmeticSetsWithShoes;
                break;
            case displaySetting.BACKPIECE:
                cosmeticSets = csdb.cosmeticSetsWithBackPiece;
                break;
            default:
                cosmeticSets = csdb.newestCosmeticSets;
                break; 
        }

        // Clear out current display (destroy existing Gameobjects and clear our the dictionary setsDisplayed
        foreach (KeyValuePair<CosmeticSet, GameObject> set in setsDisplayed)
        {
            Destroy(set.Value);
        }
        setsDisplayed.Clear();

        // Update Set Display in Collection
        foreach (CosmeticSet set in cosmeticSets)
        {
            // If set is not yet displayed or doesnt exists on display
            if (!setsDisplayed.ContainsKey(set))
            {
                // Instantiate new frame in scene
                GameObject obj = Instantiate(setFrame, Vector3.zero, Quaternion.identity, transform);

                // Set Scale of Object
                obj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                // Set Sprites in Object
                short rarity = set.getRarity();
                if (rarity == 3) { obj.transform.GetChild(0).GetComponent<Image>().sprite = offensiveIcon; }
                else if (rarity == 2) { obj.transform.GetChild(0).GetComponent<Image>().sprite = contrabandIcon; }
                else if (rarity == 1) { obj.transform.GetChild(0).GetComponent<Image>().sprite = criminalIcon; }
                else { obj.transform.GetChild(0).GetComponent<Image>().sprite = ohMyGoopIcon; }

                // Assign Display function to button in obj
                obj.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(() => displaySet(set, displaySetting));

                // Set Set Icon 
                obj.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = set.setIcon;

                // Track new Game object in setsDisplayed
                setsDisplayed.Add(set, obj);
                }
        }

        // Setting Up Set display
        if (setsDisplayed.Count > 0)
        {
            displaySet(cosmeticSets[0], displaySetting);
        }
    }


    private void displaySet(CosmeticSet newSetObj, displaySetting displaySetting)
    {
        // If there is not yet a set beign displayed
        if (currentSet != null)
        {
            //replaces unit if the selected unit is not currently being displayed
            if (!currentSet.Equals(newSetObj))
            {
                // Get rid of old display
                Destroy(currentSet);

                // Create new display
                currentSet = Instantiate(setModel, setDisplay.transform.position, Quaternion.identity);
                displayPiecesOnModel(newSetObj, displaySetting);
            }
        }
        else
        {
            //creates obj with Set prefab if there is no obj being displayed currently
            currentSet = Instantiate(setModel, setDisplay.transform.position, Quaternion.identity);

            displayPiecesOnModel(newSetObj, displaySetting);
        }
    }

    private void displayPiecesOnModel(CosmeticSet newSetObj, displaySetting displaySetting)
    {
        // Set parent
        currentSet.transform.SetParent(setDisplay.transform);

        // Set Scale of Object
        currentSet.transform.localScale = new Vector3(1f, 1f, 1f);

        // Set Set Title
        currentSet.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = newSetObj.setName.ToUpper();

        // Set Set Description and Rarity
        currentSet.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = newSetObj.setDescription;
        currentSet.transform.GetChild(2).transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "rarity: " + newSetObj.setRarity.ToString().ToUpper();

        // Set Set Stats
        // Display number only if head points is not 0
        if (newSetObj.hasHead)
        {
            currentSet.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newSetObj.setHeadStats.ToString();
        }
        else
        {
            currentSet.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "N/A";
            currentSet.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }

        // Display number only if torso points is not 0
        if (newSetObj.hasTorso)
        {
            currentSet.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = newSetObj.setTorsoStats.ToString();
        }
        else
        {
            currentSet.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "N/A";
            currentSet.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        }

        // Display number only if legs points is not 0
        if (newSetObj.hasLegs)
        {
            currentSet.transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = newSetObj.setLegsStats.ToString();
        }
        else
        {
            currentSet.transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "N/A";
            currentSet.transform.GetChild(3).transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        }

        // Display number only if shoes points is not 0
        if (newSetObj.hasShoes)
        {
            currentSet.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = newSetObj.setShoesStats.ToString();
        }
        else
        {
            currentSet.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "N/A";
            currentSet.transform.GetChild(3).transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
        }

        // Display number only if back piece points is not 0
        if (newSetObj.hasBackPiece)
        {
            currentSet.transform.GetChild(3).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = newSetObj.setBackPieceStats.ToString();
        }
        else
        {
            currentSet.transform.GetChild(3).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "N/A";
            currentSet.transform.GetChild(3).transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
        }

        // Display Assets in Model
        // Head (only display set's head if display setting is ALL or NEW or HEAD. Otherwise, display dummy head)
        if (displaySetting == displaySetting.ALL || displaySetting == displaySetting.NEW || displaySetting == displaySetting.HEAD) {
            currentSet.transform.GetChild(0).transform.GetChild(13).GetComponentInChildren<Image>().sprite = newSetObj.setHeadFront;
            currentSet.transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<Image>().sprite = newSetObj.setHeadBack;
        } else {
            currentSet.transform.GetChild(0).transform.GetChild(13).GetComponentInChildren<Image>().sprite = dummyHead;
            currentSet.transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<Image>().sprite = transparentSprite;
        }

        // Torso (only display set's torso if display setting is ALL or NEW or TORSO. Otherwise, display plain torso)
        if (displaySetting == displaySetting.ALL || displaySetting == displaySetting.NEW || displaySetting == displaySetting.TORSO) {
            currentSet.transform.GetChild(0).transform.GetChild(15).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoSuperFront;
            currentSet.transform.GetChild(0).transform.GetChild(12).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoFront;
            currentSet.transform.GetChild(0).transform.GetChild(9).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoMiddle;
            currentSet.transform.GetChild(0).transform.GetChild(6).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoBack;
            currentSet.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoSuperBack;
        } else {
            currentSet.transform.GetChild(0).transform.GetChild(15).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(12).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(9).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(6).GetComponentInChildren<Image>().sprite = plainTorso;
            currentSet.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Image>().sprite = transparentSprite;
        }

        // Legs (only display set's legs if display setting is ALL or NEW or LEGS. Otherwise, display plain legs)
        if (displaySetting == displaySetting.ALL || displaySetting == displaySetting.NEW || displaySetting == displaySetting.LEGS) {
            currentSet.transform.GetChild(0).transform.GetChild(14).GetComponentInChildren<Image>().sprite = newSetObj.setLegsSuperFront;
            currentSet.transform.GetChild(0).transform.GetChild(10).GetComponentInChildren<Image>().sprite = newSetObj.setLegsFront;
            currentSet.transform.GetChild(0).transform.GetChild(7).GetComponentInChildren<Image>().sprite = newSetObj.setLegsMiddle;
            currentSet.transform.GetChild(0).transform.GetChild(4).GetComponentInChildren<Image>().sprite = newSetObj.setLegsBack;
        } else {
            currentSet.transform.GetChild(0).transform.GetChild(14).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(10).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(7).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(4).GetComponentInChildren<Image>().sprite = plainLegs;
        }

        // Shoes (only display set's shoes if display setting is ALL or NEW or SHOES. Otherwise, display NOTHING)
        if (displaySetting == displaySetting.ALL || displaySetting == displaySetting.NEW || displaySetting == displaySetting.SHOES) {
            currentSet.transform.GetChild(0).transform.GetChild(11).GetComponentInChildren<Image>().sprite = newSetObj.setShoesFront;
            currentSet.transform.GetChild(0).transform.GetChild(8).GetComponentInChildren<Image>().sprite = newSetObj.setShoesMiddle;
            currentSet.transform.GetChild(0).transform.GetChild(5).GetComponentInChildren<Image>().sprite = newSetObj.setShoesBack;
        } else {
            currentSet.transform.GetChild(0).transform.GetChild(11).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(8).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(5).GetComponentInChildren<Image>().sprite = transparentSprite;
        }

        // Back Piece (only display set's shoes if display setting is ALL or NEW or BACKPIECE. Otherwise, display NOTHING)
        if (displaySetting == displaySetting.ALL || displaySetting == displaySetting.NEW || displaySetting == displaySetting.BACKPIECE)
        {
            currentSet.transform.GetChild(0).transform.GetChild(16).GetComponentInChildren<Image>().sprite = newSetObj.setBackPieceFront;
            currentSet.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<Image>().sprite = newSetObj.setBackPieceMiddle;
            currentSet.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = newSetObj.setBackPieceBack;
        } else {
            currentSet.transform.GetChild(0).transform.GetChild(16).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<Image>().sprite = transparentSprite;
            currentSet.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = transparentSprite;
        }
    }

}

