using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    private float healthRegenRate;
    private LoadCharacter loadCharacter;
    private HealthUI healthUI;

    private void Awake()
    {
        loadCharacter = GetComponent<LoadCharacter>();
        healthUI = FindObjectOfType<HealthUI>();
        if (loadCharacter == null)
        {
            Debug.LogError("LoadCharacter component not found on the GameObject.");
            return;
        }

        InitializeHealth();
    }
    private void Start()
    {
        if (healthUI != null)
        {
            healthUI.InitializeHealthUI(maxHealth);
        }
        else
        {
            Debug.LogError("HealthUI component not found in the scene.");
        }
        StartCoroutine(RegenerateHealth());
    }

    private void InitializeHealth()
    {
        CharacterData characterData = loadCharacter.GetSelectedCharacterData();
        if (characterData != null)
        {
            maxHealth = characterData.baseHealth;
            currentHealth = maxHealth;
            healthRegenRate = characterData.baseHealthRegen;
        }
        else
        {
            Debug.LogError("Failed to load character data for health initialization.");
        }
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += Mathf.FloorToInt(healthRegenRate);
                currentHealth = Mathf.Min(currentHealth, maxHealth);
                healthUI.UpdateHealthUI(currentHealth);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage Taken");
        healthUI.UpdateHealthUI(currentHealth);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Trigger damage feedback
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}

