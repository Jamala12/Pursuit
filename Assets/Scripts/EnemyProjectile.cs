using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = hitInfo.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("Player component not found on collided object.");
            }
        }
        if (hitInfo.CompareTag("Wall") || hitInfo.CompareTag("Player") || hitInfo.CompareTag("Prop"))
        {
            Destroy(gameObject);
        }
    }
}
