using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability[] abilities; // Array of abilities
    public Transform firePoint; // Assign this in the inspector
    public PlayerMana playerMana; // Reference to the player's mana script
    public float[] cooldownTimes;
    private float[] activeTimes;
    public AbilityUI abilityUI;

    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    private AbilityState[] states;

    public KeyCode[] activationKeys; // Array of keys for activating abilities

    void Start()
    {
        cooldownTimes = new float[abilities.Length];
        activeTimes = new float[abilities.Length];
        states = new AbilityState[abilities.Length];

        for (int i = 0; i < abilities.Length; i++)
        {
            states[i] = AbilityState.Ready;
        }

        abilityUI.UpdateAbilityUI();
    }

    void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] == null)
            {
                continue;  // Skip this iteration if the ability is not assigned
            }

            switch (states[i])
            {
                case AbilityState.Ready:
                    if (Input.GetKeyDown(activationKeys[i]) && firePoint != null)
                    {
                        if (abilities[i].Activate(gameObject, firePoint, playerMana))
                        {
                            states[i] = AbilityState.Active;
                            activeTimes[i] = abilities[i].activeTime;
                        }
                        else
                        {
                            // Handle failed activation (e.g., not enough mana or other conditions)
                        }
                    }
                    break;
                case AbilityState.Active:
                    if (activeTimes[i] > 0)
                    {
                        activeTimes[i] -= Time.deltaTime;
                    }
                    else
                    {
                        states[i] = AbilityState.Cooldown;
                        cooldownTimes[i] = abilities[i].cooldownTime;
                    }
                    break;
                case AbilityState.Cooldown:
                    if (cooldownTimes[i] > 0)
                    {
                        cooldownTimes[i] -= Time.deltaTime;
                    }
                    else
                    {
                        states[i] = AbilityState.Ready;
                    }
                    break;
            }
        }
    }

}
