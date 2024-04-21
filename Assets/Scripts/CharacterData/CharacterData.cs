using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public bool canUseMagic;
    public bool canUsePhysical;
    public bool canDualWield;
    public bool hasReducedDamage;
    public int spellSlots;

    public Sprite characterSprite;

    public int baseHealth;
    public int baseMana;
    public int baseAttackDamage;
    public int baseMagicDamage;
    public float baseHealthRegen;
    public float baseManaRegen;
    public float baseAttackSpeed;
    public float baseMovementSpeed;

    public GameObject equipmentPrefab;
}

