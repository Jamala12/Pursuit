using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWand : Weapon
{
    public GameObject projectilePrefab;

    public override void Attack()
    {

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
