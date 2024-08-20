using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void displaySet(CosmeticSet newSet)
    {
        // If there is not yet a set beign displayed
        if (currentSet != null)
        {
            //replaces unit if the selected unit is not currently being displayed
            if (!currentSet.Equals(newSet))
            {
                // Get rid of old display
                Destroy(currentSet);

                // Create new display
                currentSet = Instantiate(setModel, setDisplay.transform.position, Quaternion.identity);

                // Set parent
                currentSet.transform.SetParent(setDisplay.transform);

                // Set Scale of Object
                currentSet.transform.localScale = new Vector3(1f, 1f, 1f);
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
        }
    }
}
