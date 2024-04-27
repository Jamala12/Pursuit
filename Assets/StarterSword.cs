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
            GameObject projectile = Instantiate(slashPrefab, firePoint.position, firePoint.rotation);

            Debug.Log("StarterWand Attack");
        }
    }
}
