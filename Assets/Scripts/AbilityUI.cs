using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public AbilityHolder abilityHolder; // Reference to the AbilityHolder script
    public Slider[] cooldownSliders; // Array of sliders matching the abilities order
    public Image[] abilityIcons; // Array of Images for each ability's icon

    public void UpdateAbilityUI()
    {
        for (int i = 0; i < abilityHolder.abilities.Length; i++)
        {
            Ability ability = abilityHolder.abilities[i];
            if (ability != null && !(ability is NoneAbility))
            {
                abilityIcons[i].sprite = ability.iconPrefab;
                abilityIcons[i].color = Color.white; // Ensure icon is visible
            }
            else
            {
                abilityIcons[i].sprite = null;
                abilityIcons[i].color = Color.clear; // Make icon transparent if NoneAbility
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
