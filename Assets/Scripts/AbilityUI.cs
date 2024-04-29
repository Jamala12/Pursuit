using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public AbilityHolder abilityHolder; // Reference to the AbilityHolder script
    public Slider[] cooldownSliders; // Array of sliders matching the abilities order
    public Image[] abilityIcons; // Array of Images for each ability's icon

    public void UpdateAbilityUI()
    {
        if (abilityHolder.abilities.Length <= 2)
        {
            cooldownSliders[cooldownSliders.Length - 1].gameObject.SetActive(false);
        }
        UpdateAbilityIcon();
    }

    public void UpdateAbilityIcon()
    {
        // Check the shortest array length to prevent out of bounds errors
        int count = Mathf.Min(abilityHolder.abilities.Length, abilityIcons.Length, cooldownSliders.Length);

        for (int i = 0; i < count; i++)
        {
            if (abilityHolder.abilities[i] != null)
            {
                if (abilityHolder.abilities[i].iconPrefab != null)
                {
                    abilityIcons[i].sprite = abilityHolder.abilities[i].iconPrefab; // Set the icon sprite
                    abilityIcons[i].color = new Color(1, 1, 1, 1); // Set alpha to 1 (opaque)
                }
                else
                {
                    abilityIcons[i].color = new Color(1, 1, 1, 0); // Set alpha to 0 (transparent) if no icon prefab
                }
            }
            else
            {
                abilityIcons[i].color = new Color(1, 1, 1, 0); // Set alpha to 0 (transparent) if no ability
            }
        }
    }


    void Update()
    {
        // Update sliders only for active abilities
        for (int i = 0; i < abilityHolder.abilities.Length; i++)
        {
            if (i < cooldownSliders.Length && abilityHolder.abilities[i] != null)
            {
                cooldownSliders[i].maxValue = abilityHolder.abilities[i].cooldownTime;
                cooldownSliders[i].value = abilityHolder.cooldownTimes[i];
            }
        }
    }
}