using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the player's mana including initialization, regeneration, and consumption.
public class PlayerMana : MonoBehaviour
{
    private int currentMana; // Current amount of mana available to the player.
    private int maxMana; // Maximum mana capacity based on character data.
    private float manaRegenRate; // Rate at which mana regenerates over time.
    private LoadCharacter loadCharacter; // Component to load character data.
    private ManaUI manaUI; // Reference to the Mana UI manager.

    private void Awake()
    {
        loadCharacter = GetComponent<LoadCharacter>(); // Retrieves LoadCharacter from the GameObject.
        manaUI = FindObjectOfType<ManaUI>(); // Finds the ManaUI in the scene.
        if (loadCharacter == null)
        {
            Debug.LogError("LoadCharacter component not found on the GameObject."); // Error handling if LoadCharacter is missing.
            return;
        }
        InitializeMana(); // Initialize mana based on character data.
    }

    private void Start()
    {
        if (manaUI != null)
        {
            manaUI.InitializeManaUI(maxMana); // Initialize the UI with the maximum mana.
        }
        else
        {
            Debug.LogError("ManaUI component not found in the scene."); // Error handling if ManaUI is missing.
        }
        StartCoroutine(RegenerateMana()); // Start the mana regeneration process.
    }

    // Initializes the mana values based on the selected character data.
    private void InitializeMana()
    {
        CharacterData characterData = loadCharacter.GetSelectedCharacterData(); // Get character data.
        if (characterData != null)
        {
            maxMana = characterData.baseMana; // Set maximum mana from character data.
            currentMana = maxMana; // Set current mana to maximum at start.
            manaRegenRate = characterData.baseManaRegen; // Set mana regeneration rate.
        }
        else
        {
            Debug.LogError("Failed to load character data for mana initialization."); // Error handling if data is not loaded.
        }
    }

    // Coroutine that manages the regeneration of mana over time.
    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana += Mathf.FloorToInt(manaRegenRate); // Increment current mana based on regen rate.
                currentMana = Mathf.Min(currentMana, maxMana); // Ensure current mana does not exceed max mana.
                manaUI.UpdateManaUI(currentMana); // Update the mana UI.
            }
            yield return new WaitForSeconds(1); // Wait for 1 second before the next increment.
        }
    }

    // Method to handle mana consumption.
    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            UpdateManaUI(); // Make sure to update any UI elements or state dependent on mana.
            return true;
        }
        else
        {
            Debug.Log("Not enough mana");
            return false;
        }
    }

    private void UpdateManaUI()
    {
        manaUI.UpdateManaUI(currentMana);
    }
}
