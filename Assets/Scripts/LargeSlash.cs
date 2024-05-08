using System.Collections;
using UnityEngine;

public class LargeSlash : Slash
{
    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * moveSpeed;
        startTime = Time.time;
        startPosition = transform.position;
    }
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("Enemy component not found on collided object.");
            }
            Destroy(gameObject);
        }
        if (hitInfo.gameObject.CompareTag("Enemy Attack"))
        {
            EnemyProjectile projectile = hitInfo.gameObject.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                projectile.Die();
            }
        }
    }
}