using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Cosmetic Set Database", menuName = "Cosmetic Sets/New Cosmetic Set Database")]

public class CosmeticSetDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public List<CosmeticSet> cosmeticSets;
    public List<int> cosmeticSetsWithHeads;
    public List<int> cosmeticSetsWithTorso;
    public List<int> cosmeticSetsWithLegs;
    public List<int> cosmeticSetsWithShoes;
    public List<int> cosmeticSetsWithBackPiece;


    public void OnAfterDeserialize()
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
        int length = cosmeticSets.Count;
        CosmeticSet set;
        for (int index = 0; index < length; index++)
        {
            // get current Cosmetic Set
            set = cosmeticSets[index];

            // Filter with Head
            if (set.hasHead) {
                cosmeticSetsWithHeads.Add(index);
            }
            // Filter with Torso
            if (set.hasTorso)
            {
                cosmeticSetsWithTorso.Add(index);
            }
            // Filter with Legs
            if (set.hasLegs)
            {
                cosmeticSetsWithLegs.Add(index);
            }
            // Filter with Shoes
            if (set.hasShoes)
            {
                cosmeticSetsWithShoes.Add(index);
            }
            // Filter with Back Piece
            if (set.hasBackPiece)
            {
                cosmeticSetsWithBackPiece.Add(index);
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
