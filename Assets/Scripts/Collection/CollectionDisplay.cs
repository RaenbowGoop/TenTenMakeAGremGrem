using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectionDisplay : MonoBehaviour
{
    [SerializeField] CosmeticSetDatabase csdb;
    Dictionary<CosmeticSet, GameObject> setsDisplayed = new Dictionary<CosmeticSet, GameObject>();
    int indexOfCurrentSet = -1;

    // Prefab(s) for display
    [SerializeField] GameObject setFrame;
    [SerializeField] GameObject setModel;

    // Game Object Display
    [SerializeField] GameObject setDisplay;
    GameObject currentSet;

    // Cosmetic Set 
    [SerializeField] Sprite offensiveIcon;
    [SerializeField] Sprite barelyLegalIcon;
    [SerializeField] Sprite criminalIcon;
    [SerializeField] Sprite ohMyGoopIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        // Update Set Display in Collection
        foreach (CosmeticSet set in csdb.cosmeticSets)
        {
            // If set is not yet displayed or doesnt exists on display
            if (!setsDisplayed.ContainsKey(set))
            {
                // Instantiate new frame in scene
                GameObject obj = Instantiate(setFrame, Vector3.zero, Quaternion.identity, transform);

                // Set Scale of Object
                obj.transform.localScale = new Vector3(1f, 1f, 1f);

                // Set Sprites in Object
                short rarity = set.getRarity();
                if (rarity == 3) { obj.transform.GetChild(0).GetComponent<Image>().sprite = offensiveIcon; }
                else if (rarity == 2) { obj.transform.GetChild(0).GetComponent<Image>().sprite = barelyLegalIcon; }
                else if (rarity == 1) { obj.transform.GetChild(0).GetComponent<Image>().sprite = criminalIcon; }
                else { obj.transform.GetChild(0).GetComponent<Image>().sprite = ohMyGoopIcon; }

                // Assign Display function to button in obj
                obj.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(() => displaySet(set));

                // Set Set Icon 
                obj.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = set.setIcon;

                // Track new Game object in setsDisplayed
                setsDisplayed.Add(set, obj);
                }
        }

        // Setting Up Set display
        if (indexOfCurrentSet == -1 && setsDisplayed.Count > 0)
        {
            indexOfCurrentSet = 0;
            displaySet(csdb.cosmeticSets[indexOfCurrentSet]);
        }
    }

    private void displaySet(CosmeticSet newSetObj)
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

                // Set parent
                currentSet.transform.SetParent(setDisplay.transform);

                // Set Scale of Object
                currentSet.transform.localScale = new Vector3(1f, 1f, 1f);

                // Set Set Title
                currentSet.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = newSetObj.setName.ToUpper();

                // Set Set Description
                currentSet.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = newSetObj.setDescription;

                // Set Set Stats
                currentSet.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newSetObj.setHeadStats.ToString();
                currentSet.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = newSetObj.setTorsoStats.ToString();
                currentSet.transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = newSetObj.setLegsStats.ToString();
                currentSet.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = newSetObj.setShoesStats.ToString();
                currentSet.transform.GetChild(3).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = newSetObj.setBackPieceStats.ToString();

                // Display Assets in Model
                // Head
                currentSet.transform.GetChild(0).transform.GetChild(10).GetComponentInChildren<Image>().sprite = newSetObj.setHeadFront;
                currentSet.transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<Image>().sprite = newSetObj.setHeadBack;

                // Torso
                currentSet.transform.GetChild(0).transform.GetChild(9).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoFront;
                currentSet.transform.GetChild(0).transform.GetChild(7).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoMiddle;
                currentSet.transform.GetChild(0).transform.GetChild(4).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoBack;

                // Legs
                currentSet.transform.GetChild(0).transform.GetChild(8).GetComponentInChildren<Image>().sprite = newSetObj.setLegsFront;
                currentSet.transform.GetChild(0).transform.GetChild(5).GetComponentInChildren<Image>().sprite = newSetObj.setLegsMiddle;
                currentSet.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<Image>().sprite = newSetObj.setLegsBack;

                // Shoes
                currentSet.transform.GetChild(0).transform.GetChild(6).GetComponentInChildren<Image>().sprite = newSetObj.setShoesFront;
                currentSet.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Image>().sprite = newSetObj.setShoesBack;

                // Back Piece
                currentSet.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = newSetObj.setBackPiece;
            }
        }
        else
        {
            //creates obj with Set prefab if there is no obj being displayed currently
            currentSet = Instantiate(setModel, setDisplay.transform.position, Quaternion.identity);

            // Set parent
            currentSet.transform.SetParent(setDisplay.transform);

            // Set Scale of Object
            currentSet.transform.localScale = new Vector3(1f, 1f, 1f);

            // Set Set Title
            currentSet.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = newSetObj.setName.ToUpper();

            // Set Set Description
            currentSet.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = newSetObj.setDescription;

            // Set Set Stats
            currentSet.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newSetObj.setHeadStats.ToString();
            currentSet.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = newSetObj.setTorsoStats.ToString();
            currentSet.transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = newSetObj.setLegsStats.ToString();
            currentSet.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = newSetObj.setShoesStats.ToString();
            currentSet.transform.GetChild(3).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = newSetObj.setBackPieceStats.ToString();

            // Display Assets in Model
            // Head
            currentSet.transform.GetChild(0).transform.GetChild(10).GetComponentInChildren<Image>().sprite = newSetObj.setHeadFront;
            currentSet.transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<Image>().sprite = newSetObj.setHeadBack;

            // Torso
            currentSet.transform.GetChild(0).transform.GetChild(9).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoFront;
            currentSet.transform.GetChild(0).transform.GetChild(7).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoMiddle;
            currentSet.transform.GetChild(0).transform.GetChild(4).GetComponentInChildren<Image>().sprite = newSetObj.setTorsoBack;

            // Legs
            currentSet.transform.GetChild(0).transform.GetChild(8).GetComponentInChildren<Image>().sprite = newSetObj.setLegsFront;
            currentSet.transform.GetChild(0).transform.GetChild(5).GetComponentInChildren<Image>().sprite = newSetObj.setLegsMiddle;
            currentSet.transform.GetChild(0).transform.GetChild(2).GetComponentInChildren<Image>().sprite = newSetObj.setLegsBack;

            // Shoes
            currentSet.transform.GetChild(0).transform.GetChild(6).GetComponentInChildren<Image>().sprite = newSetObj.setShoesFront;
            currentSet.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Image>().sprite = newSetObj.setShoesBack;

            // Back Piece
            currentSet.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>().sprite = newSetObj.setBackPiece;
        }
    }
}
