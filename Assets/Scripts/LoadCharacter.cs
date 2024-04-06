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
        Debug.LogError("Character not found: " + selectedCharacter);
        return null; // Consider how to handle this appropriately.
    }
}


