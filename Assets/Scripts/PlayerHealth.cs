using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the health functionality of the player in the game.
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] 
    private int maxHealth;
    [SerializeField] 
    private int currentHealth;
    private float healthRegenRate; // Rate at which the player's health regenerates over time.
    private LoadCharacter loadCharacter; // Component to load character data.
    private HealthUI healthUI; // Reference to the Health UI manager.
    private Coroutine regenCoroutine;
    private float delayAfterDamage = 3f;

    public GameObject gameOverMenu;
    public GameObject pauseButton;

    [SerializeField]
    public AudioClip damageSound;

    public int MaxHealth
    {
        get => maxHealth;
        set 
        { 
            maxHealth = value;
            healthUI.UpdateMaxHealthUI(maxHealth);
        }
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;  // Direct assignment, no validation
    }

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
        gameOverMenu.SetActive(false);

        if (healthUI != null)
        {
            healthUI.InitializeHealthUI(maxHealth); // Initialize the health UI to the player's max health at the start.
        }
        else
        {
            Debug.LogError("HealthUI component not found in the scene."); // Error log if HealthUI is not found.
        }
        regenCoroutine = StartCoroutine(RegenerateHealth()); // Start the health regeneration coroutine.
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

    private IEnumerator DelayAndRegenerateHealth()
    {
        yield return new WaitForSeconds(delayAfterDamage); // Wait for a few seconds after taking damage
        regenCoroutine = StartCoroutine(RegenerateHealth());
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
        StopCoroutine(regenCoroutine);
        currentHealth -= damage;
        healthUI.UpdateHealthUI(currentHealth);
        CheckHealth();
        regenCoroutine = StartCoroutine(DelayAndRegenerateHealth());
        AudioManager.Instance.PlaySound(damageSound, AudioManager.Instance.damageSource);
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

    private void Die()
    {
        Debug.Log("Player has died.");
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverMenu not assigned in the inspector.");
        }
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }
        else
        {
            Debug.LogError("PauseButton not assigned in the inspector.");
        }
        Time.timeScale = 0;
    }

    public IEnumerator BoostHealthRegeneration(float duration, float multiplier)
    {
        float originalRegenRate = healthRegenRate;
        healthRegenRate *= multiplier;
        yield return new WaitForSeconds(duration);
        healthRegenRate = originalRegenRate;
    }
}