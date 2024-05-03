using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int finalDamage;
    public Transform firePoint;
    public int damageMultiplier = 1;
    public virtual void Attack()
    {
        Debug.Log("Default Attack");
    }

    public void SetFirePoint(Transform newFirePoint)
    {
        firePoint = newFirePoint;
    }

    public void InitializeAttack(int baseDamage)
    {
        finalDamage = baseDamage * damageMultiplier;
    }
}