using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Possible set rarity
public enum rarity { Offensive, BarelyLegal, Criminal, OhMyGOOP };

[CreateAssetMenu(fileName = "New Cosmetic Set", menuName = "Cosmetic Sets")]
public class CosmeticSet : ScriptableObject
{
    // Details
    public string setName;
    public string setDescription;
    public rarity setRarity;

    // Sprites
    [SerializeField] public Sprite setHead;
    [SerializeField] public Sprite setFace;
    [SerializeField] public Sprite setTorso;
    [SerializeField] public Sprite setLegs;
    [SerializeField] public Sprite setFeet;
}
