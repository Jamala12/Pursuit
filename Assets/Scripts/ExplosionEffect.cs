using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float blastRadius = 5f;
    public float explosionForce = 1000f;
    public int explosionDamage = 50;

    void Start()
    {
        Collider2D[] affectedColliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D affectedCollider in affectedColliders)
        {
            // Apply force to any rigidbody within the blast radius
            Rigidbody2D rb = affectedCollider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = affectedCollider.transform.position - transform.position;
                rb.AddForce(forceDirection.normalized * explosionForce);
            }

            // Apply damage to Player or Enemy if within blast radius
            PlayerHealth playerHealth = affectedCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);
            }

            Enemy enemy = affectedCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }

            Prop prop = affectedCollider.GetComponent<Prop>();
            if (prop != null)
            {
                prop.Die();
            }
        }
        Destroy(gameObject, 1f);  // Delay the destruction to ensure the physics response is calculated
    }
}
