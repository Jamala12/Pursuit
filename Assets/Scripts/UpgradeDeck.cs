using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDeck : MonoBehaviour
{
    public UpgradeCard[] availableUpgrades;
    public UpgradeCard[] selectedCards = new UpgradeCard[3];
    public UpgradeCard.ConstrintType characterConstraintType;
    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI rarityText;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI descriptionText;
        public Button selectButton;
        public Image icon;
    }

    public UpgradeUI[] upgradeUIs;
    public GameObject upgradeMenu;

    private void Awake()
    {
        upgradeMenu.SetActive(false);
    }

    private void Update()
    {
        if (upgradeMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void SelectCards()
    {
        ResetSelectedCards();  // Clear previous selections
        for (int i = 0; i < 3; i++)  // Loop exactly 3 times to select three cards
        {
            List<UpgradeCard> currentSelection = FilterCardsByRarity();
            if (currentSelection == null || currentSelection.Count == 0)
            {
                Debug.Log("No cards available after rarity filtering.");
                continue;  // Skip this iteration if no cards are available after rarity filtering
            }

            currentSelection = FilterCardsByConstraintType(currentSelection);
            if (currentSelection.Count == 0)
            {
                Debug.Log("No cards available after type filtering.");
                continue;  // Skip this iteration if no cards are available after type filtering
            }

            // Randomly choose a card from currentSelection
            int randomIndex = Random.Range(0, currentSelection.Count);
            UpgradeCard chosenCard = currentSelection[randomIndex];
            selectedCards[i] = chosenCard;

            // If the card is not repeatable, remove it from availableUpgrades
            if (!chosenCard.isRepeatable)
            {
                availableUpgrades = RemoveCardFromAvailableUpgrades(chosenCard, availableUpgrades);
            }
        }

        // Setup UI for each selected card
        for (int i = 0; i < upgradeUIs.Length; i++)
        {
            if (i < selectedCards.Length && selectedCards[i] != null)
            {
                SetupUpgradeUI(upgradeUIs[i], selectedCards[i]);
            }
        }
        upgradeMenu.SetActive(true);  // Activate the upgrade menu after setting up UIs
    }

    public void ResetSelectedCards()
    {
        for (int i = 0; i < selectedCards.Length; i++)
        {
            selectedCards[i] = null;
        }
    }

    // Utility method to remove a card from the available upgrades
    private UpgradeCard[] RemoveCardFromAvailableUpgrades(UpgradeCard cardToRemove, UpgradeCard[] upgrades)
    {
        List<UpgradeCard> tempList = new List<UpgradeCard>(upgrades);
        tempList.Remove(cardToRemove);
        return tempList.ToArray();
    }


    private List<UpgradeCard> FilterCardsByRarity()
    {
        List<UpgradeCard> filteredByRarity = new List<UpgradeCard>();
        int rand = Random.Range(1, 101); // Random number between 1 and 100
        UpgradeCard.RarityType selectedRarity;

        if (rand <= 70) selectedRarity = UpgradeCard.RarityType.Common;
        else if (rand <= 90) selectedRarity = UpgradeCard.RarityType.Rare;
        else if (rand <= 99) selectedRarity = UpgradeCard.RarityType.Epic;
        else selectedRarity = UpgradeCard.RarityType.Legendary;

        foreach (var card in availableUpgrades)
        {
            if (card.rarity == selectedRarity)
            {
                filteredByRarity.Add(card);
            }
        }

        return filteredByRarity;
    }


    private List<UpgradeCard> FilterCardsByConstraintType(List<UpgradeCard> cards)
    {
        List<UpgradeCard> filteredByType = new List<UpgradeCard>();


        foreach (var card in cards)
        {
            if (characterConstraintType == UpgradeCard.ConstrintType.Any)
            {
                // If character can use any type, add all cards
                filteredByType.Add(card);
            }
            else if (characterConstraintType == UpgradeCard.ConstrintType.MagicOnly &&
                     (card.type == UpgradeCard.ConstrintType.MagicOnly || card.type == UpgradeCard.ConstrintType.Any))
            {
                // If character is MagicOnly, add MagicOnly and Any type cards
                filteredByType.Add(card);
            }
            else if (characterConstraintType == UpgradeCard.ConstrintType.PhysicalOnly &&
                     (card.type == UpgradeCard.ConstrintType.PhysicalOnly || card.type == UpgradeCard.ConstrintType.Any))
            {
                // If character is PhysicalOnly, add PhysicalOnly and Any type cards
                filteredByType.Add(card);
            }
        }
        return filteredByType;
    }

    public void SetCharacterConstraintType(UpgradeCard.ConstrintType type)
    {
        characterConstraintType = type;
    }


    void SetupUpgradeUI(UpgradeUI ui, UpgradeCard card)
    {
        ui.rarityText.text = card.rarity.ToString();
        ui.rarityText.color = GetColorByRarity(card.rarity);
        ui.titleText.text = card.cardName;
        ui.descriptionText.text = card.description; // Assume cardName holds the description
        ui.icon.sprite = card.icon; // Assuming card has a Sprite property 'icon'

        ui.selectButton.onClick.RemoveAllListeners();
        ui.selectButton.onClick.AddListener(() => ApplyUpgrade(card));
    }

    Color GetColorByRarity(UpgradeCard.RarityType rarity)
    {
        switch (rarity)
        {
            case UpgradeCard.RarityType.Rare:
                return Color.blue;
            case UpgradeCard.RarityType.Epic:
                return Color.magenta;
            case UpgradeCard.RarityType.Legendary:
                return Color.yellow;
            default:
                return Color.gray;
        }
    }

    public void ApplyUpgrade(UpgradeCard card)
    {
        Debug.Log("Applied Upgrade: " + card.cardName);
        AbilityHolder abilityHolder = FindObjectOfType<AbilityHolder>();

        switch (card.cardName.ToLower())
        {
            case "more health":
                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.MaxHealth += 10;
                    playerHealth.CurrentHealth += 10;  // Optionally increase current health too
                }
                break;

            case "more mana":
                PlayerMana playerMana = FindObjectOfType<PlayerMana>();
                if (playerMana != null)
                {
                    playerMana.MaxMana += 10;
                    playerMana.CurrentMana += 10;  // Optionally increase current mana too
                }
                break;

            case "more damage":
                Attack playerAttack = FindObjectOfType<Attack>();
                if (playerAttack != null)
                {
                    playerAttack.damage += 5;
                }
                break;

            case "more attack speed":
                PlayerInput playerInput = FindObjectOfType<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.AttackSpeed += 0.1f;  // Assuming attackSpeed is a multiplier
                }
                break;

            case "more movement":
                PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.MovementSpeed += 0.5f;
                }
                break;

            case "more health+":
                playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.MaxHealth += 30;
                    playerHealth.CurrentHealth += 30;  // Optionally increase current health too
                }
                break;

            case "more mana+":
                playerMana = FindObjectOfType<PlayerMana>();
                if (playerMana != null)
                {
                    playerMana.MaxMana += 30;
                    playerMana.CurrentMana += 30;  // Optionally increase current mana too
                }
                break;

            case "more mana regen":
                playerMana = FindObjectOfType<PlayerMana>();
                if (playerMana != null)
                {
                    playerMana.ManaRegenRate += 5;
                }
                break;

            case "flameburst":
                // Assuming you have a reference to the Flameburst ability instance
                if (abilityHolder != null && card.ability != null)
                {
                    abilityHolder.AddOrReplaceAbility(card.ability);
                }
                break;
            case "berserk":
                // If the card has an ability, add or replace it in the AbilityHolder
                if (abilityHolder != null && card.ability != null)
                {
                    abilityHolder.AddOrReplaceAbility(card.ability);
                }
                break;

            default:
                Debug.LogError("Unknown upgrade option.");
                break;
        }

        upgradeMenu.SetActive(false);  // Hide the upgrade menu after applying the upgrade
    }


}
