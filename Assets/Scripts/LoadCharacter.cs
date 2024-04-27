using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public CharacterData wizardData;
    public CharacterData knightData;
    public CharacterData warriorData;
    private Dictionary<string, CharacterData> characterDataDictionary;
    private SpriteRenderer spriteRenderer;
    public Transform weaponSlot1; // Assign this in the Inspector
    public Transform firePointAngle;
    public AbilityHolder abilityHolder;

    private void Awake()
    {
        characterDataDictionary = new Dictionary<string, CharacterData>
        {
            { "Wizard", wizardData },
            { "Knight", knightData },
            { "Warrior", warriorData }
        };
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplyCharacterSprite();
        LoadEquipment();
        InitializeAbilities();
    }

    private void InitializeAbilities()
    {
        CharacterData characterData = GetSelectedCharacterData();
        if (characterData != null && characterData.starterAbilities != null && abilityHolder != null)
        {
            abilityHolder.abilities = characterData.starterAbilities;
        }
        else
        {
            Debug.LogError("Failed to initialize abilities. Make sure character data and abilities are set and AbilityHolder is assigned.");
        }
    }

    public Sprite GetCharacterSprite()
    {
        CharacterData characterData = GetSelectedCharacterData();
        return characterData != null ? characterData.characterSprite : null;
    }

    private void ApplyCharacterSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = GetCharacterSprite();
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on the GameObject.");
        }
    }

    public float GetMovementSpeed()
    {
        CharacterData characterData = GetSelectedCharacterData();
        return characterData != null ? characterData.baseMovementSpeed : 0f;
    }

    public CharacterData GetSelectedCharacterData()
    {
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "DefaultCharacterName");
        if (characterDataDictionary.TryGetValue(selectedCharacter, out CharacterData data))
        {
            return data;
        }
        Debug.LogError("Character data not found: " + selectedCharacter);
        return null; // handle this
    }

    public float GetAttackSpeed()
    {
        CharacterData characterData = GetSelectedCharacterData();
        return characterData != null ? characterData.baseAttackSpeed : 1f; // Default to 1 attack per second
    }

    private void LoadEquipment()
    {
        // Clear existing equipment
        foreach (Transform child in weaponSlot1)
        {
            Destroy(child.gameObject);
        }

        CharacterData characterData = GetSelectedCharacterData();
        if (characterData != null && characterData.equipmentPrefab != null)
        {
            // Instantiate the equipment prefab and parent it to the equipmentSlot
            GameObject equipmentInstance = Instantiate(characterData.equipmentPrefab, weaponSlot1.position, weaponSlot1.rotation, weaponSlot1);
            
            Weapon weapon = equipmentInstance.GetComponent<Weapon>();
            if (weapon != null)
            {
                Transform firePoint = firePointAngle.GetChild(0);
                weapon.SetFirePoint(firePoint);
            }
            else
            {
                Debug.LogWarning("No equipment prefab found for selected character");
            }
        }
    }
}

