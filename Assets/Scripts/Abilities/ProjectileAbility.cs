using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Projectile Ability")]
public class projectileAbility : Ability
{
    public GameObject projectilePrefab;

    public override bool Activate(GameObject owner, Transform firePoint, PlayerMana playerMana)
    {
        // First check and consume mana
        if (playerMana.UseMana(manaCost))
        {
            if (projectilePrefab != null && firePoint != null)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
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

