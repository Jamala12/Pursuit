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
        Debug.Log("Initializing mana UI"); // Log message to indicate that the mana UI is being initialized.
        slider.maxValue = mana; // Set the maximum value of the slider to the provided mana value.
        slider.value = mana; // Set the current value of the slider to the provided mana value, filling it up fully.
    }

    // Update the mana UI to reflect the current amount of mana.
    public void UpdateManaUI(int mana)
    {
        Debug.Log($"Updating mana UI: {mana}"); // Log message to indicate the new mana value being displayed.
        slider.value = mana; // Adjust the slider value to match the current mana amount.
    }
}
