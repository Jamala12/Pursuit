using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public virtual void Attack()
    {
        Debug.Log("Default Attack");
    }

    public void SetFirePoint(Transform newFirePoint)
    {
        firePoint = newFirePoint;
    }
}