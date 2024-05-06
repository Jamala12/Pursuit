using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradeCard", menuName = "Upgrade System/Upgrade Card")]
public class UpgradeCard : ScriptableObject
{
    public string cardName;
    public RarityType rarity;
    public bool isMagic;
    public bool isRepeatable;

    public enum RarityType
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}