using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Buff Ability")]
public class BerserkAbility : Ability
{
    public float damageMultiplier = 2f;
    public float speedMultiplier = 1.5f;
    public float attackSpeedMultiplier = 1.5f; // New multiplier for increasing attack speed
    public float duration = 10f; // Duration of the Berserk effect in seconds

    public override bool Activate(GameObject owner, Transform firePoint, PlayerMana playerMana)
    {
        if (!playerMana.UseMana(manaCost))
        {
            Debug.Log("Not enough mana to activate Berserk.");
            return false;
        }

        StartBerserk(owner);
        return true;
    }

    private void StartBerserk(GameObject owner)
    {
        PlayerHealth health = owner.GetComponent<PlayerHealth>();
        PlayerMovement movement = owner.GetComponent<PlayerMovement>();
        PlayerInput input = owner.GetComponent<PlayerInput>();
        Attack attack = owner.GetComponentInChildren<Attack>();
        SpriteRenderer spriteRenderer = owner.GetComponent<SpriteRenderer>();

        if (health != null)
        {
            health.StartCoroutine(health.BoostHealthRegeneration(duration, 2f));
        }

        if (movement != null)
        {
            movement.ApplySpeedBoost(speedMultiplier, duration);
        }

        if (input != null)
        {
            input.ApplyAttackSpeedBoost(attackSpeedMultiplier, duration);
        }

        if (attack != null)
        {
            attack.ApplyDamageBoost(damageMultiplier, duration);
        }
        if (spriteRenderer != null)
        {
            health.StartCoroutine(ChangeColorTemporary(spriteRenderer, Color.red, duration));
        }
    }
    private IEnumerator ChangeColorTemporary(SpriteRenderer spriteRenderer, Color newColor, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = newColor; // Change to berserk color
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor; // Revert to original color
    }
}
