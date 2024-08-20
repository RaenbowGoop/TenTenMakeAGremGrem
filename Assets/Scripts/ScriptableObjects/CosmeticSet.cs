using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Possible set rarity
public enum rarity { Offensive, BarelyLegal, Criminal, OhMyGOOP };

[CreateAssetMenu(fileName = "New Cosmetic Set", menuName = "Cosmetic Sets/New Cosmetic Set")]
public class CosmeticSet : ScriptableObject, System.IComparable<CosmeticSet>, System.IEquatable<CosmeticSet>
{
    // Details
    public string setName;
    public string setDescription;
    public rarity setRarity;
    private int rarityValue;

    // Sprites
    [SerializeField] public Sprite setHead;
    [SerializeField] public Sprite setFace;
    [SerializeField] public Sprite setTorso;
    [SerializeField] public Sprite setLegs;
    [SerializeField] public Sprite setFeet;

    public void Awake()
    {
        // Set rarity value based on set rarity
        if (setRarity == rarity.Offensive) {
            rarityValue = 0;

        } else if (setRarity == rarity.BarelyLegal) {
            rarityValue = 1;

        } else if (setRarity == rarity.Criminal) {
            rarityValue = 2;
        } else
        {
            rarityValue = 3;
        }
    }

    // Comparators

    // Overridden Default Comparators
    public int CompareTo(CosmeticSet other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return this.CompareByRarity(other);
        }
    }

    public bool Equals(CosmeticSet other)
    {
        return this.setName.Equals(other.setName) && this.setRarity.Equals(other.setRarity);
    }

    // Custom Comparators
    public int CompareByRarity(CosmeticSet other)
    {
        return this.rarityValue.CompareTo(other.rarityValue);
    }

    public int CompareByName(CosmeticSet other)
    {
       return this.setName.CompareTo(other.setName);
    }


    public int CompareCosmeticObjectDefault(CosmeticSet other)
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


}
