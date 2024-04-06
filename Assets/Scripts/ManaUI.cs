using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    public Slider slider;

    public void InitializeManaUI(int mana)
    {
        Debug.Log("Initializing mana ui");
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void UpdateManaUI(int mana)
    {
        Debug.Log($"Updating mana UI: {mana}");
        slider.value = mana;
    }
}
