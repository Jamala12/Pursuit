using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public float shootingDistance = 10.0f;  // Ideal shooting distance
    public GameObject projectilePrefab;     // Projectile that the enemy shoots
    public float shootingCooldown = 2.0f;   // Time between shots
    private float lastShotTime = 0;         // Last time the enemy shot
    public Transform firePoint;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();  // Calls the base class FixedUpdate to handle movement and chasing

        if (player != null && firePoint != null)
        {
            float playerDistance = Vector2.Distance(transform.position, player.position);

            if (playerDistance <= detectionRadius)
            {
                MaintainDistance(playerDistance);  // Adjust position relative to player

                AimAtPlayer();
                if (playerDistance <= shootingDistance && Time.time > lastShotTime + shootingCooldown)
                {
                    Shoot();
                    lastShotTime = Time.time;
                }
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop moving when player is out of detection range
            }
        }
    }

    void AimAtPlayer()
    {
        Vector2 direction = player.position - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MaintainDistance(float distance)
    {
        float bufferZone = 1.0f;  // Buffer to prevent constant oscillation when at the edge of shootingDistance
        Vector2 direction = (player.position - transform.position).normalized;

        if (distance < shootingDistance - bufferZone) // Enemy too close
        {
            rb.velocity = -direction * speed; // Move away from the player
        }
        else if (distance > shootingDistance + bufferZone) // Enemy too far
        {
            rb.velocity = direction * speed; // Move towards the player
        }
        else
        {
            rb.velocity = Vector2.zero; // Maintain current position when in optimal range
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
