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
    public List<CosmeticSet> newestCosmeticSets;

    [SerializeField] public bool hideNewSets;
    [SerializeField] bool includeNewSets;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        // Sort Cosmetic Set
        cosmeticSets.Sort();
        newestCosmeticSets.Sort();

        // Clear lists
        cosmeticSetsWithHeads.Clear();
        cosmeticSetsWithTorso.Clear();
        cosmeticSetsWithLegs.Clear();
        cosmeticSetsWithShoes.Clear();
        cosmeticSetsWithBackPiece.Clear();

        // sort out cosmetic sets that have certain pieces
        foreach (CosmeticSet set in cosmeticSets)
        {
            // if we don't want to include new sets, skip this set if it's a new set
            if(!includeNewSets)
            {
                if (newestCosmeticSets.Contains(set))
                {
                    continue;
                }
            }

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
