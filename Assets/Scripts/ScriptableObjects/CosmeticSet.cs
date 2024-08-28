using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Possible set rarity
public enum rarity { Offensive, BarelyLegal, Criminal, OhMyGOOP };

[CreateAssetMenu(fileName = "New Cosmetic Set", menuName = "Cosmetic Sets/New Cosmetic Set")]
public class CosmeticSet : ScriptableObject, System.IComparable<CosmeticSet>, System.IEquatable<CosmeticSet>
{
    // Details
    [SerializeField] public string setName;
    [SerializeField] public string setDescription;
    [SerializeField] public rarity setRarity;

    // Set Occupancy
    [SerializeField] public bool hasHead;
    [SerializeField] public bool hasTorso;
    [SerializeField] public bool hasLegs;
    [SerializeField] public bool hasShoes;
    [SerializeField] public bool hasBackPiece;
    // Set Stats
    [SerializeField] public int setHeadStats;
    [SerializeField] public int setTorsoStats;
    [SerializeField] public int setLegsStats;
    [SerializeField] public int setShoesStats;
    [SerializeField] public int setBackPieceStats;

    // Sprites
    [SerializeField] public Sprite setHeadFront;
    [SerializeField] public Sprite setHeadBack;

    [SerializeField] public Sprite setTorsoFront;
    [SerializeField] public Sprite setTorsoMiddle;
    [SerializeField] public Sprite setTorsoBack;

    [SerializeField] public Sprite setLegsFront;
    [SerializeField] public Sprite setLegsMiddle;
    [SerializeField] public Sprite setLegsBack;

    [SerializeField] public Sprite setShoesFront;
    [SerializeField] public Sprite setShoesBack;

    [SerializeField] public Sprite setBackPiece;

    [SerializeField] public Sprite setIcon;

    void Awake()
    {
        hasHead = true;
        hasTorso = true;
        hasLegs = true;
        hasShoes = true;
        hasBackPiece = true;
    }

    public short getRarity()
    {
        if (setRarity == rarity.Offensive)
        {
            return 3;

        }
        else if (setRarity == rarity.BarelyLegal)
        {
            return 2;

        }
        else if (setRarity == rarity.Criminal)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // Comparators
    public int CompareTo(CosmeticSet other)
    {
        // A null value means that this object is greater.
        if (other == null)
        {
            return 1;
        }
        // First Compare by rarity, then compare by name if rarity is matching
        else
        {
            int currentComparison = CompareByRarity(other);
            if (currentComparison == 0)
            {
                return CompareByName(other);
            }
            else
            {
                return currentComparison;
            }
        }
    }

    public bool Equals(CosmeticSet other)
    {
        return this.setName.Equals(other.setName) && this.setRarity.Equals(other.setRarity);
    }

    // Custom Comparators
    public int CompareByRarity(CosmeticSet other)
    {
        short thisRarity = this.getRarity();
        short otherRarity = other.getRarity();

        if (other == null)
        {
            return 1;
        }
        else
        {
            if (thisRarity > otherRarity)
            {
                return 1;
            }
            else if (thisRarity == otherRarity)
            {
                return 0;
            }
            return -1;
        }
    }

    public int CompareByName(CosmeticSet other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return string.Compare(this.setName, other.setName);
        }
    }
}
