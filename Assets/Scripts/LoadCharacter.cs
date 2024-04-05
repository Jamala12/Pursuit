using System;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public CharacterData wizardData;
    public CharacterData knightData;
    public CharacterData warriorData;
    private SpriteRenderer spriteRenderer;

    // Basic stats, initialized from CharacterData
    protected string characterName;
    protected bool canUseMagic;
    protected bool canUsePhysical;
    protected bool canDualWield;
    protected bool hasReducedDamage;
    protected int spellSlots;
    protected int health;
    protected int mana;
    protected int attackDamage;
    protected int magicDamage;
    protected float healthRegen;
    protected float manaRegen;
    protected float attackSpeed;
    protected float movementSpeed;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSelectedCharacter();
    }

    private void LoadSelectedCharacter()
    {
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "DefaultCharacterName");
        switch (selectedCharacter)
        {
            case "Wizard":
                ApplyCharacterData(wizardData);
                break;
            case "Knight":
                ApplyCharacterData(knightData);
                break;
            case "Warrior":
                ApplyCharacterData(warriorData);
                break;
            default:
                Debug.LogError("No character selected or character not found.");
                break;
        }
    }

    private void ApplyCharacterData(CharacterData characterData)
    {
        spriteRenderer.sprite = characterData.characterSprite;
        health = characterData.baseHealth;
        movementSpeed = characterData.baseMovementSpeed;
        healthRegen = characterData.baseHealthRegen;
        mana = characterData.baseMana;
        manaRegen = characterData.baseManaRegen;
        attackDamage = characterData.baseAttackDamage;
        magicDamage = characterData.baseMagicDamage;
        attackSpeed = characterData.baseAttackSpeed;

        characterName = characterData.characterName;
        canUseMagic = characterData.canUseMagic;
        canDualWield = characterData.canDualWield;
        hasReducedDamage = characterData.hasReducedDamage;
        spellSlots = characterData.spellSlots;
    }
}

