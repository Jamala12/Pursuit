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
            attackComponent.InitializeDamage(finalDamage);
        }
    }
}
