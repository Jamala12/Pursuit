using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability[] abilities;  // Ensure this is set with at least one 'NoneAbility' for initial states
    public Transform firePoint;
    public PlayerMana playerMana;
    public float[] cooldownTimes;
    private float[] activeTimes;
    public AbilityUI abilityUI;
    private int abilityRewriteIndex = 0;

    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    private AbilityState[] states;

    public KeyCode[] activationKeys;

    void Start()
    {
        // Initialize the abilities array if it's not already set up in the Inspector
        if (abilities == null || abilities.Length == 0)
        {
            abilities = new Ability[3];  // Example: default to 3 abilities
        }
        states = new AbilityState[abilities.Length];
        cooldownTimes = new float[abilities.Length];
        activeTimes = new float[abilities.Length];

        // Initialize with NoneAbility if the slot is empty
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] == null || abilities[i] is NoneAbility)
            {
                abilities[i] = ScriptableObject.CreateInstance<NoneAbility>();
            }
            states[i] = AbilityState.Ready;
        }

        if (abilityUI != null)
        {
            abilityUI.UpdateAbilityUI();
        }
        else
        {
            Debug.LogError("AbilityUI component not found, ensure it's assigned.");
        }
    }

    public void AddOrReplaceAbility(Ability newAbility)
    {
        int indexToReplace = -1;
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] is NoneAbility)
            {
                abilities[i] = newAbility;
                Debug.Log($"Ability {newAbility.GetType().Name} added to slot {i + 1}");
                abilityUI.UpdateAbilityUI(); // Update UI each time an ability is added or changed
                return;
            }
            if (indexToReplace == -1 || abilities[i] is NoneAbility)
            {
                indexToReplace = i;
            }
        }

        // Replace the first found NoneAbility or overwrite the oldest ability if no slots are free
        if (indexToReplace != -1)
        {
            abilities[indexToReplace] = newAbility;
            Debug.Log($"Ability {newAbility.GetType().Name} overwritten in slot {indexToReplace + 1}");
            abilityRewriteIndex = (indexToReplace + 1) % abilities.Length;
            abilityUI.UpdateAbilityUI(); // Update UI after overwriting an ability
        }
    }


    void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] == null || states.Length <= i || cooldownTimes.Length <= i)
            {
                Debug.LogWarning($"Skipping iteration {i} due to null or invalid setup.");
                continue;
            }

            switch (states[i])
            {
                case AbilityState.Ready:
                    if (Input.GetKeyDown(activationKeys[i]) && firePoint != null && playerMana != null)
                    {
                        if (abilities[i].Activate(gameObject, firePoint, playerMana))
                        {
                            states[i] = AbilityState.Active;
                            activeTimes[i] = abilities[i].activeTime;
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
