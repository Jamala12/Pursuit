using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class manages the Mana UI functionality in the game.
public class XpUI : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;

    public void InitializeXpUI(int currentXp, int maxXp)
    {
        slider.maxValue = maxXp;
        slider.value = currentXp; 
    }

  
    public void UpdateXpUI(int xp)
    {
        slider.value = xp;
    }

    public void UpdateLevelUI(int level)
    {
        text.text = level.ToString();
    }
}
