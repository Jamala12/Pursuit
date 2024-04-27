using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball Ability")]
public class FireballAbility : Ability
{
    public GameObject fireballPrefab;

    public override bool Activate(GameObject owner, Transform firePoint, PlayerMana playerMana)
    {
        // First check and consume mana
        if (playerMana.UseMana(manaCost))
        {
            if (fireballPrefab != null && firePoint != null)
            {
                Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
                return true;
            }
            else
            {
                Debug.LogError("Fireball prefab or fire point is null.");
                return false;
            }
        }
        return false;  // Mana check failed, do not activate ability
    }
}

