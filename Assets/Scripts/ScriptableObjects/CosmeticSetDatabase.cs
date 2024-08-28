using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "New Cosmetic Set Database", menuName = "Cosmetic Sets/New Cosmetic Set Database")]

public class CosmeticSetDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public List<CosmeticSet> cosmeticSets;
    public List<CosmeticSet> cosmeticSetsWithHeads;
    public List<CosmeticSet> cosmeticSetsWithTorso;
    public List<CosmeticSet> cosmeticSetsWithLegs;
    public List<CosmeticSet> cosmeticSetsWithShoes;
    public List<CosmeticSet> cosmeticSetsWithBackPiece;


    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        // Sort Cosmetic Set
        cosmeticSets.Sort();

        // Clear lists
        cosmeticSetsWithHeads.Clear();
        cosmeticSetsWithTorso.Clear();
        cosmeticSetsWithLegs.Clear();
        cosmeticSetsWithShoes.Clear();
        cosmeticSetsWithBackPiece.Clear();

        // sort out cosmetic sets that have certain pieces
        foreach (CosmeticSet set in cosmeticSets)
        {
            // Filter with Head
            if (set.hasHead)
            {
                cosmeticSetsWithHeads.Add(set);
            }
            // Filter with Torso
            if (set.hasTorso)
            {
                cosmeticSetsWithTorso.Add(set);
            }
            // Filter with Legs
            if (set.hasLegs)
            {
                cosmeticSetsWithLegs.Add(set);
            }
            // Filter with Shoes
            if (set.hasShoes)
            {
                cosmeticSetsWithShoes.Add(set);
            }
            // Filter with Back Piece
            if (set.hasBackPiece)
            {
                cosmeticSetsWithBackPiece.Add(set);
            }
        }
    }
}
