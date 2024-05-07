using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider slider;
    public void InitializeHealthUI(int health)
    {
        slider.maxValue= health;
        slider.value= health;
    }
    public void UpdateHealthUI(int health)
    {
        slider.value= health;
    }
    public void UpdateMaxHealthUI(int health)
    {
        slider.maxValue = health;
    }
}
