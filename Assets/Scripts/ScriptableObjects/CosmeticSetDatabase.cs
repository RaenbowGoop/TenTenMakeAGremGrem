using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Cosmetic Set Database", menuName = "Cosmetic Set/New Cosmetic Set Database")]

public class CosmeticSetDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public CosmeticSet[] cosmeticSets;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
    }

}
