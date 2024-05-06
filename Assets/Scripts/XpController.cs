using UnityEngine;

public class XpController : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXp = 0;
    public int nextLevelXp = 20;  // XP needed for the next level
    public float growthFactor = 1.25f;  // Growth factor for exponential growth
    public int maxLevelXp = 200;  // Maximum XP required for any level up
    public XpUI xpUI;  // Reference to the XpUI script

    void Start()
    {
        // Initialize the UI at the start with the maximum XP for the first level
        if (xpUI != null)
        {
            xpUI.InitializeXpUI(currentXp,nextLevelXp);
            xpUI.UpdateLevelUI(currentLevel);
        }
    }

    public void GainXP(int amount)
    {
        currentXp += amount;
        CheckLevelUp();
        // Update the UI whenever XP is gained
        if (xpUI != null)
            xpUI.UpdateXpUI(currentXp);  // Show remaining XP to next level
    }

    private void CheckLevelUp()
    {
        while (currentXp >= nextLevelXp)
        {
            currentXp -= nextLevelXp;
            currentLevel++;
            LevelUp();
            UpdateNextLevelXp();
        }
    }

    private void LevelUp()
    {
        Debug.Log("Level up! New level: " + currentLevel);
        // Initialize the UI for the next level with the new XP threshold
        if (xpUI != null)
        {
            xpUI.InitializeXpUI(currentXp, nextLevelXp);
            xpUI.UpdateLevelUI(currentLevel);
        }
    }

    private void UpdateNextLevelXp()
    {
        nextLevelXp = (int)Mathf.Round(nextLevelXp * growthFactor);
        if (nextLevelXp > maxLevelXp)
        {
            nextLevelXp = maxLevelXp;  // Cap the XP requirement at 200
        }
    }
}

