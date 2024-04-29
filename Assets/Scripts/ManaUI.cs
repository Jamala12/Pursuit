using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class manages the Mana UI functionality in the game.
public class ManaUI : MonoBehaviour
{
    public Slider slider; // A reference to the UI slider component that represents the mana bar.

    // Initialize the Mana UI with a specified amount of mana.
    public void InitializeManaUI(int mana)
    {
        slider.maxValue = mana; // Set the maximum value of the slider to the provided mana value.
        slider.value = mana; // Set the current value of the slider to the provided mana value, filling it up fully.
    }

    // Update the mana UI to reflect the current amount of mana.
    public void UpdateManaUI(int mana)
    {
        slider.value = mana; // Adjust the slider value to match the current mana amount.
    }
}
