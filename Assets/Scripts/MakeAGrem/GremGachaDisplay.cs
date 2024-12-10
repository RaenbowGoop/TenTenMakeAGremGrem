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
            Transform gremModelTransform = this.transform;

            TransformCosmeticSetPair headPair = new TransformCosmeticSetPair(gremGachaManager.head, gremModelTransform);
            TransformCosmeticSetPair torsoPair = new TransformCosmeticSetPair(gremGachaManager.torso, gremModelTransform);
            TransformCosmeticSetPair legsPair = new TransformCosmeticSetPair(gremGachaManager.legs, gremModelTransform);
            TransformCosmeticSetPair shoesPair = new TransformCosmeticSetPair(gremGachaManager.shoes, gremModelTransform);
            TransformCosmeticSetPair backPiecePair = new TransformCosmeticSetPair(gremGachaManager.backPiece, gremModelTransform);

            CosmeticSet.setGremDisplay(headPair, torsoPair, legsPair, shoesPair, backPiecePair);

            isDisplayed = true;
        }
    }
}
