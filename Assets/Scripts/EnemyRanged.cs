using UnityEngine;
using System.Collections;

public class EnemyRanged : Enemy
{
    public float shootingDistance = 10.0f;  // Preferred shooting distance
    public GameObject projectilePrefab;     // Projectile that the enemy shoots
    public float shootingCooldown = 2.0f;   // Time between shots
    private float lastShotTime = 0;         // Last time the enemy shot
    public Transform firePoint;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        MaintainDistance();  // Custom method to maintain distance from player
        if (player != null && firePoint != null)
        {
            AimAtPlayer();
            if (Time.time > lastShotTime + shootingCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
    }

    void AimAtPlayer()
    {
        Vector2 direction = player.position - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MaintainDistance()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            Vector2 direction = (player.position - transform.position).normalized;
            if (distance < shootingDistance - 1) // Enemy too close
            {
                rb.velocity = -direction * speed; // Move away from the player
            }
            else if (distance > shootingDistance + 1) // Enemy too far
            {
                rb.velocity = direction * speed; // Move towards the player
            }
            else
            {
                rb.velocity = Vector2.zero; // Stay at current position
            }
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
