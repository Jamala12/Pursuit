using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDeck : MonoBehaviour
{
    public UpgradeCard[] availableUpgrades;
    private List<UpgradeCard> currentSelection = new List<UpgradeCard>();

    public UpgradeCard[] selectedCards = new UpgradeCard[3]; // Array to store the chosen cards
    public bool canUseMagic = true; // This can be set according to the player's abilities

    void Start()
    {
        SelectCards();
    }

    void SelectCards()
    {
        for (int i = 0; i < 3; i++)
        {
            UpgradeCard chosenCard = ChooseCardByRarity();
            if (chosenCard != null)
            {
                selectedCards[i] = chosenCard;
                Debug.Log($"Card Selected for slot {i + 1}: {chosenCard.cardName}, Rarity: {chosenCard.rarity}");
                if (!chosenCard.isRepeatable)
                {
                    currentSelection.Remove(chosenCard);
                }
            }
            else
            {
                Debug.Log($"No card available for slot {i + 1}, check rarity settings or filters.");
            }
        }
    }

    UpgradeCard ChooseCardByRarity()
    {
        if (availableUpgrades == null || availableUpgrades.Length == 0)
        {
            Debug.LogError("availableUpgrades array is not initialized!");
            return null;
        }

        List<UpgradeCard> tempPool;
        do
        {
            tempPool = new List<UpgradeCard>(availableUpgrades);
            int rand = Random.Range(1, 101); // Random number between 1 and 100
            UpgradeCard.RarityType rarity;

            if (rand <= 70) rarity = UpgradeCard.RarityType.Common;
            else if (rand <= 90) rarity = UpgradeCard.RarityType.Rare;
            else if (rand <= 99) rarity = UpgradeCard.RarityType.Epic;
            else rarity = UpgradeCard.RarityType.Legendary;

            tempPool.RemoveAll(card => card.rarity != rarity || (card.isMagic && !canUseMagic));
            Debug.Log($"Trying rarity {rarity}: {tempPool.Count} cards available after filter.");

            if (tempPool.Count == 0)
            {
                Debug.Log($"No cards available for rarity {rarity}. Downgrading rarity...");
                if (rarity == UpgradeCard.RarityType.Legendary) rarity = UpgradeCard.RarityType.Epic;
                else if (rarity == UpgradeCard.RarityType.Epic) rarity = UpgradeCard.RarityType.Rare;
                else if (rarity == UpgradeCard.RarityType.Rare) rarity = UpgradeCard.RarityType.Common;
                else break; // If it reaches Common and still no cards, exit loop
            }
        } while (tempPool.Count == 0);

        if (tempPool.Count > 0)
        {
            UpgradeCard selectedCard = tempPool[Random.Range(0, tempPool.Count)]; // Randomly select a card from the filtered list
            Debug.Log($"Card chosen: {selectedCard.cardName} with Rarity: {selectedCard.rarity}");
            return selectedCard;
        }

        return null; // If no cards are available after filtering, return null
    }

}

