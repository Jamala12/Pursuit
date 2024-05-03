using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public bool canUseMagic;
    public bool canUsePhysical;
    public bool canDualWield;

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