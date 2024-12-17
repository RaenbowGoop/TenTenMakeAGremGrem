using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// Possible set rarity
public enum rarity { Offensive, Contraband, Criminal, OhMyGOOP };

// Set Piece Types
public enum pieceType { Head, Torso, Legs, Shoes, BackPiece };

[CreateAssetMenu(fileName = "New Cosmetic Set", menuName = "Cosmetic Sets/New Cosmetic Set")]
public class CosmeticSet : ScriptableObject, System.IComparable<CosmeticSet>, System.IEquatable<CosmeticSet>
{
    // Details
    [Header("Cosmetic Set General Attributes")]
    [SerializeField] public string setName;
    [SerializeField] public string setDescription;
    [SerializeField] public rarity setRarity;

    // Set Occupancy
    [Header("Cosmetic Set Included Pieces")]
    [SerializeField] public bool hasHead;
    [SerializeField] public bool hasTorso;
    [SerializeField] public bool hasLegs;
    [SerializeField] public bool hasShoes;
    [SerializeField] public bool hasBackPiece;

    // Advanced Settings for Sets (to omit or hide other pieces)
    [Header("Cosmetic Set Advanced Settings")]
    public List<pieceType> headHides;
    public List<pieceType> torsoHides;
    public List<pieceType> legsHides;
    public List<pieceType> shoesHides;
    public List<pieceType> backPieceHides;

    // Set Stats
    [Header("Cosmetic Set Statistics")]
    [SerializeField] public int setHeadStats;
    [SerializeField] public int setTorsoStats;
    [SerializeField] public int setLegsStats;
    [SerializeField] public int setShoesStats;
    [SerializeField] public int setBackPieceStats;

    // Sprites
    [Header("Cosmetic Set Assets")]
    [SerializeField] public Sprite setHeadSuperFront;
    [SerializeField] public Sprite setHeadFront;
    [SerializeField] public Sprite setHeadBack;

    [SerializeField] public Sprite setTorsoSuperFront;
    [SerializeField] public Sprite setTorsoFront;
    [SerializeField] public Sprite setTorsoMiddle;
    [SerializeField] public Sprite setTorsoBack;
    [SerializeField] public Sprite setTorsoSuperBack;

    [SerializeField] public Sprite setLegsSuperFront;
    [SerializeField] public Sprite setLegsFront;
    [SerializeField] public Sprite setLegsMiddle;
    [SerializeField] public Sprite setLegsBack;

    [SerializeField] public Sprite setShoesFront;
    [SerializeField] public Sprite setShoesMiddle;
    [SerializeField] public Sprite setShoesBack;
    [SerializeField] public Sprite setShoesSuperBack;

    [SerializeField] public Sprite setBackPieceFront;
    [SerializeField] public Sprite setBackPieceFrontMiddle;
    [SerializeField] public Sprite setBackPieceBackMiddle;
    [SerializeField] public Sprite setBackPieceBack;

    [SerializeField] public Sprite setIcon;

    // Sprite Indices
    public const int BACK_PIECE_FRONT_INDEX = 19;
    public const int HEAD_SUPER_FRONT_INDEX = 18;
    public const int BACK_PIECE_FRONT_MIDDLE_INDEX = 17;
    public const int TORSO_SUPER_FRONT_INDEX = 16;
    public const int LEGS_SUPER_FRONT_INDEX = 15;
    public const int HEAD_FRONT_INDEX = 14;
    public const int TORSO_FRONT_INDEX = 13;
    public const int SHOES_FRONT_INDEX = 12;
    public const int LEGS_FRONT_INDEX = 11;
    public const int TORSO_MIDDLE_INDEX = 10;
    public const int SHOES_MIDDLE_INDEX = 9;
    public const int LEGS_MIDDLE_INDEX = 8;
    public const int TORSO_BACK_INDEX = 7;
    public const int SHOES_BACK_INDEX = 6;
    public const int LEGS_BACK_INDEX = 5;
    public const int TORSO_SUPER_BACK_INDEX = 4;
    public const int BACK_PIECE_BACK_MIDDLE_INDEX = 3;
    public const int HEAD_BACK_INDEX = 2;
    public const int SHOES_SUPER_BACK_INDEX = 1;
    public const int BACK_PIECE_BACK_INDEX = 0;

    public void OnBeforeSerialize() {
        // Remove Duplicates and Sort
        headHides = headHides.Distinct().ToList();
        headHides.Sort();
        torsoHides = torsoHides.Distinct().ToList();
        torsoHides.Sort();
        legsHides = legsHides.Distinct().ToList();
        legsHides.Sort();
        shoesHides = shoesHides.Distinct().ToList();
        shoesHides.Sort();
        backPieceHides = backPieceHides.Distinct().ToList();
        backPieceHides.Sort();
    }

    public short getRarity()
    {
        if (setRarity == rarity.Offensive)
        {
            return 3;

        }
        else if (setRarity == rarity.Contraband)
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

    public static void setGremDisplay(Transform gremModel, CosmeticSet head, CosmeticSet torso, CosmeticSet legs, CosmeticSet shoes, CosmeticSet backPiece)
    {

        // Head
        if (head != null)
        {
            gremModel.GetChild(HEAD_SUPER_FRONT_INDEX).GetComponentInChildren<Image>().sprite = head.setHeadSuperFront;
            gremModel.GetChild(HEAD_FRONT_INDEX).GetComponentInChildren<Image>().sprite = head.setHeadFront;
            gremModel.GetChild(HEAD_BACK_INDEX).GetComponentInChildren<Image>().sprite = head.setHeadBack;
        }

        // Torso
        if (torso != null)
        {
            gremModel.GetChild(TORSO_SUPER_FRONT_INDEX).GetComponentInChildren<Image>().sprite = torso.setTorsoSuperFront;
            gremModel.GetChild(TORSO_FRONT_INDEX).GetComponentInChildren<Image>().sprite = torso.setTorsoFront;
            gremModel.GetChild(TORSO_MIDDLE_INDEX).GetComponentInChildren<Image>().sprite = torso.setTorsoMiddle;
            gremModel.GetChild(TORSO_BACK_INDEX).GetComponentInChildren<Image>().sprite = torso.setTorsoBack;
            gremModel.GetChild(TORSO_SUPER_BACK_INDEX).GetComponentInChildren<Image>().sprite = torso.setTorsoSuperBack;
        }

        // Legs
        if (legs != null)
        {
            gremModel.GetChild(LEGS_SUPER_FRONT_INDEX).GetComponentInChildren<Image>().sprite = legs.setLegsSuperFront;
            gremModel.GetChild(LEGS_FRONT_INDEX).GetComponentInChildren<Image>().sprite = legs.setLegsFront;
            gremModel.GetChild(LEGS_MIDDLE_INDEX).GetComponentInChildren<Image>().sprite = legs.setLegsMiddle;
            gremModel.GetChild(LEGS_BACK_INDEX).GetComponentInChildren<Image>().sprite = legs.setLegsBack;
        }

        // Shoes
        if (shoes != null)
        {
            gremModel.GetChild(SHOES_FRONT_INDEX).GetComponentInChildren<Image>().sprite = shoes.setShoesFront;
            gremModel.GetChild(SHOES_MIDDLE_INDEX).GetComponentInChildren<Image>().sprite = shoes.setShoesMiddle;
            gremModel.GetChild(SHOES_BACK_INDEX).GetComponentInChildren<Image>().sprite = shoes.setShoesBack;
            gremModel.GetChild(SHOES_SUPER_BACK_INDEX).GetComponentInChildren<Image>().sprite = shoes.setShoesSuperBack;
        }

        // BackPiece
        if (backPiece != null)
        {
            gremModel.GetChild(BACK_PIECE_FRONT_INDEX).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceFront;
            gremModel.GetChild(BACK_PIECE_FRONT_MIDDLE_INDEX).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceFrontMiddle;
            gremModel.GetChild(BACK_PIECE_BACK_MIDDLE_INDEX).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceBackMiddle;
            gremModel.GetChild(BACK_PIECE_BACK_INDEX).GetComponentInChildren<Image>().sprite = backPiece.setBackPieceBack;
        }
    }
}
