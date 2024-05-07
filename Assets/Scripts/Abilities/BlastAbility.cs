using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Blast Ability")]
public class BlastAbility : Ability
{
    public GameObject blastPrefab;

    public override bool Activate(GameObject owner, Transform firePoint, PlayerMana playerMana)
    {
        // First check and consume mana
        if (playerMana.UseMana(manaCost))
        {
            if (blastPrefab != null && firePoint != null)
            {
                Instantiate(blastPrefab, owner.transform.position, owner.transform.rotation);
                return true;
            }
            else
            {
                Debug.LogError("blast prefab or fire point is null.");
                return false;
            }
        }
        return false;  // Mana check failed, do not activate ability
    }
}
