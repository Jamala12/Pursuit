using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/None Ability")]
public class NoneAbility : Ability
{
    public override bool Activate(GameObject owner, Transform firePoint, PlayerMana playerMana)
    {
        // Do nothing
        Debug.Log("Placeholder ability activated, no effect.");
        return false;
    }
}
