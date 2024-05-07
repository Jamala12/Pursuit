using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeCard", menuName = "Upgrade System/Upgrade Card")]
public class UpgradeCard : ScriptableObject
{
    public string cardName;
    public Sprite icon;
    public string description;
    public RarityType rarity;
    public ConstrintType type;
    public bool isRepeatable;
    public Ability ability;

    public enum ConstrintType
    {
        MagicOnly,
        PhysicalOnly,
        Any
    }

    public enum RarityType
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}
