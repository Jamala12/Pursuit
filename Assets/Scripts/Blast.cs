using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : Attack
{
    public float blastRadius = 5f;
    public float explosionForce = 1000f;
    protected override void Start()
    {
        Collider2D[] affectedColliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D affectedCollider in affectedColliders)
        {
            if (affectedCollider.CompareTag("Player"))
            {
                continue;
            }
            Rigidbody2D rb = affectedCollider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = affectedCollider.transform.position - transform.position;
                rb.AddForce(forceDirection.normalized * explosionForce);
            }

            Enemy enemy = affectedCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Mathf.RoundToInt(damage * 0.75f));
            }

            Prop prop = affectedCollider.GetComponent<Prop>();
            if (prop != null)
            {
                prop.Die();
            }
        }
    }

    protected override void Update()
    {

    }
    protected virtual void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
    }
}
