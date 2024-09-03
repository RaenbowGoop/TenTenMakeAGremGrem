using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GremCardRarityDisplay : MonoBehaviour
{
    [SerializeField] Sprite ohMyGoopCard;
    [SerializeField] Sprite criminalCard;
    [SerializeField] Sprite barelyLegalCard;
    [SerializeField] Sprite offensiveCard;

    [SerializeField] GremGachaManager gremGachaManager;

    [SerializeField] GameObject headCard;
    [SerializeField] GameObject torsoCard;
    [SerializeField] GameObject bodyCard;
    [SerializeField] GameObject shoesCard;
    [SerializeField] GameObject backPieceCard;

    bool isCardsSet;

    void Start()
    {
        isCardsSet = false;
    }

    void Update()
    {
        if (!isCardsSet)
        { // Set Head Card Display
            setCardDisplay(headCard, gremGachaManager.head.setRarity);

            // Set Torso Card Display
            setCardDisplay(torsoCard, gremGachaManager.torso.setRarity);

            // Set Legs Card Display
            setCardDisplay(bodyCard, gremGachaManager.legs.setRarity);

            // Set Shoes Card Display
            setCardDisplay(shoesCard, gremGachaManager.shoes.setRarity);

            // Set Back Piece Card Display
            setCardDisplay(backPieceCard, gremGachaManager.backPiece.setRarity);

            isCardsSet = true;
        }
    }

    void setCardDisplay(GameObject card, rarity setRarity)
    {
        // Disable etching for non OhMyGOOP cards
        if (setRarity != rarity.OhMyGOOP)
        {
            card.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }

        // Set card background based on rarity
        switch (setRarity)
        {
            case rarity.Offensive:
                card.transform.GetChild(0).GetComponent<Image>().sprite = offensiveCard;
                break;
            case rarity.BarelyLegal:
                card.transform.GetChild(0).GetComponent<Image>().sprite = barelyLegalCard;
                break;
            case rarity.Criminal:
                card.transform.GetChild(0).GetComponent<Image>().sprite = criminalCard;
                break;
            default:
                card.transform.GetChild(0).GetComponent<Image>().sprite = ohMyGoopCard;
                break;
        }
    }
}
