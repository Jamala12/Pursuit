using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the health functionality of the player in the game.
public class PlayerHealth : MonoBehaviour
{
    private int currentHealth; // Current health of the player.
    private int maxHealth; // Maximum health the player can have.
    private float healthRegenRate; // Rate at which the player's health regenerates over time.
    private LoadCharacter loadCharacter; // Component to load character data.
    private HealthUI healthUI; // Reference to the Health UI manager.

    private void Awake()
    {
        loadCharacter = GetComponent<LoadCharacter>(); // Get the LoadCharacter component attached to this GameObject.
        healthUI = FindObjectOfType<HealthUI>(); // Find the HealthUI component in the scene.
        if (loadCharacter == null)
        {
            Debug.LogError("LoadCharacter component not found on the GameObject."); // Error log if LoadCharacter is missing.
            return;
        }

        InitializeHealth(); // Initialize player's health based on the character data.
    }

    private void Start()
    {
        if (healthUI != null)
        {
            healthUI.InitializeHealthUI(maxHealth); // Initialize the health UI to the player's max health at the start.
        }
        else
        {
            Debug.LogError("HealthUI component not found in the scene."); // Error log if HealthUI is not found.
        }
        StartCoroutine(RegenerateHealth()); // Start the health regeneration coroutine.
    }

    // Initialize health values based on the selected character data.
    private void InitializeHealth()
    {
        CharacterData characterData = loadCharacter.GetSelectedCharacterData(); // Retrieve character data.
        if (characterData != null)
        {
            maxHealth = characterData.baseHealth; // Set max health from character data.
            currentHealth = maxHealth; // Initialize current health to max health.
            healthRegenRate = characterData.baseHealthRegen; // Set health regeneration rate.
        }
        else
        {
            Debug.LogError("Failed to load character data for health initialization."); // Error log if character data is not available.
        }
    }

    // Coroutine to regenerate health over time.
    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += Mathf.FloorToInt(healthRegenRate); // Increment health by the regen rate.
                currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health does not exceed max health.
                healthUI.UpdateHealthUI(currentHealth); // Update the health UI.
            }
            yield return new WaitForSeconds(1); // Wait for 1 second before continuing the loop.
        }
    }

    // Method to handle damage received by the player.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract damage from current health.
        Debug.Log("Damage Taken"); // Log damage taken.
        healthUI.UpdateHealthUI(currentHealth); // Update the health UI to reflect the new health value.
        CheckHealth(); // Check if player's health has reached zero or below.
    }

    // Check the player's health status.
    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Die(); // Call Die method if health is zero or below.
        }
        else
        {
            // Trigger damage feedback, such as a flash or sound.
        }
    }

    // Method to handle player's death.
    private void Die()
    {
        Debug.Log("Player has died."); // Log that the player has died.
        // Implement additional death logic here, such as triggering a death animation.
    }
}


