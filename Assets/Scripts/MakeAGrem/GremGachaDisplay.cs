using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GremGachaDisplay : MonoBehaviour
{
    [SerializeField] GremGachaManager gremGachaManager;
    bool isDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        isDisplayed = false;
    }

    void Update()
    {
        if (!isDisplayed)
        {
            // Display Assets in Model
            CosmeticSet.setGremDisplay(this.transform, gremGachaManager.head, gremGachaManager.torso, gremGachaManager.legs, gremGachaManager.shoes, gremGachaManager.backPiece);

            isDisplayed = true;
        }
    }
}
