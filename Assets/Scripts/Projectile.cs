using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxRange = 5f; // Maximum range the projectile can travel

    private Vector3 startPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Record the starting position of the projectile
        rb.velocity = transform.right * speed; // Assumes the projectile faces right by default
    }

    void Update()
    {
        // Check the distance traveled by comparing the current position with the start position
        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            // If the projectile has traveled its maximum range, destroy it
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Optionally, use tags or layers to differentiate between wall, enemy, and other collidable objects
        if (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Wall"))
        {
            // Here you can add additional logic for what happens upon hitting an enemy or wall
            // For example, dealing damage to an enemy

            Destroy(gameObject); // Destroy the projectile on collision
        }
    }
}

