using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private int currentMana;
    private int maxMana;
    private float manaRegenRate;
    private LoadCharacter loadCharacter;
    private ManaUI manaUI;

    private void Awake()
    {
        loadCharacter = GetComponent<LoadCharacter>();
        manaUI = FindObjectOfType<ManaUI>();
        if (loadCharacter == null)
        {
            Debug.LogError("LoadCharacter component not found on the GameObject.");
            return;
        }

        InitializeMana();
    }

    private void Start()
    {
        if (manaUI != null)
        {
            manaUI.InitializeManaUI(maxMana);
        }
        else
        {
            Debug.LogError("ManaUI component not found in the scene.");
        }

        StartCoroutine(RegenerateMana());
    }

    private void InitializeMana()
    {
        CharacterData characterData = loadCharacter.GetSelectedCharacterData();
        if (characterData != null)
        {
            maxMana = characterData.baseMana;
            currentMana = maxMana;
            manaRegenRate = characterData.baseManaRegen;

        }
        else
        {
            Debug.LogError("Failed to load character data for mana initialization.");
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            if (currentMana < maxMana)
            {
                currentMana += Mathf.FloorToInt(manaRegenRate);
                currentMana = Mathf.Min(currentMana, maxMana);
                manaUI.UpdateManaUI(currentMana);
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void UseMana(int amount)
    {
        if (currentMana - amount <= 0)
        {
            Debug.Log("not enough mana");
        }
        else
        {
            currentMana -= amount;
            manaUI.UpdateManaUI(currentMana);
        }
    }
}

