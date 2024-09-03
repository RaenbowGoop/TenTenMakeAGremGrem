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
            // Head
            this.transform.GetChild(10).GetComponent<Image>().sprite = gremGachaManager.head.setHeadFront;
            this.transform.GetChild(1).GetComponent<Image>().sprite = gremGachaManager.head.setHeadBack;

            // Torso
            this.transform.GetChild(9).GetComponent<Image>().sprite = gremGachaManager.torso.setTorsoFront;
            this.transform.GetChild(7).GetComponent<Image>().sprite = gremGachaManager.torso.setTorsoMiddle;
            this.transform.GetChild(4).GetComponent<Image>().sprite = gremGachaManager.torso.setTorsoBack;

            // Legs
            this.transform.GetChild(8).GetComponent<Image>().sprite = gremGachaManager.legs.setLegsFront;
            this.transform.GetChild(6).GetComponent<Image>().sprite = gremGachaManager.legs.setLegsMiddle;
            this.transform.GetChild(2).GetComponent<Image>().sprite = gremGachaManager.legs.setLegsBack;

            // Shoes
            this.transform.GetChild(5).GetComponent<Image>().sprite = gremGachaManager.shoes.setShoesFront;
            this.transform.GetChild(3).GetComponent<Image>().sprite = gremGachaManager.shoes.setShoesBack;

            // Back Piece
            this.transform.GetChild(0).GetComponent<Image>().sprite = gremGachaManager.backPiece.setBackPiece;

            isDisplayed = true;
        }
    }
}
