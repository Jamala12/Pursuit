using UnityEngine;
using static UpgradeCard;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public bool canDualWield;
    public Constrint type;
    public enum Constrint
    {
        MagicOnly,
        PhysicalOnly,
        Any
    }

    public Sprite characterSprite;

    public int baseHealth;
    public int baseMana;
    public int baseAttackDamage;
    public float baseHealthRegen;
    public float baseManaRegen;
    public float baseAttackSpeed;
    public float baseMovementSpeed;

    public GameObject equipmentPrefab;
    public Ability[] starterAbilities; // Array of starter abilities
}