using System.Collections;
using UnityEngine;

public class LargeSlash : Slash
{
    protected override void Start()
    {
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
                Debug.Log("Damage applied: " + damage);
            }
            else
            {
                Debug.LogError("Enemy component not found on collided object.");
            }
            Destroy(gameObject);
        }
    }
}