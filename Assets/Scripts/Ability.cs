using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;
    public int manaCost;  // Mana cost to activate this ability
    public Sprite iconPrefab;

    public abstract bool Activate(GameObject owner, Transform firePoint, PlayerMana playerMana);
}
