using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterSword : Weapon
{
    public GameObject slashPrefab;

    public override void Attack()
    {
        if (slashPrefab != null && firePoint != null)
        {
            Quaternion rotationOffset = Quaternion.Euler(0, 0, -90);
            GameObject projectile = Instantiate(slashPrefab, firePoint.position, firePoint.rotation * rotationOffset);
            Attack attackComponent = projectile.GetComponent<Attack>();
            if (attackComponent != null)
            {
                attackComponent.InitializeDamage(finalDamage);  // Make sure finalDamage is correctly calculated
            }
            else
            {
                Debug.LogError("Attack component missing on instantiated slash prefab.");
            }
        }
    }
}
