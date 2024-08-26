using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Cosmetic Set Database", menuName = "Cosmetic Sets/New Cosmetic Set Database")]

public class CosmeticSetDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public List<CosmeticSet> cosmeticSets;

    public void OnAfterDeserialize()
    {
        cosmeticSets.Sort();
    }

    public void OnBeforeSerialize()
    {
    }
}
