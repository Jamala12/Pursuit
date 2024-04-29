using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float detectionRadius = 5.0f;
    public int damage = 5;
    private Transform player;
    private Rigidbody2D rb;
    private Coroutine damageCoroutine; // To keep track of the damage coroutine

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRadius)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
                StopDamageCoroutine();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(ApplyDamage());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopDamageCoroutine();
        }
    }

    private IEnumerator ApplyDamage()
    {
        while (true)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }

    private void StopDamageCoroutine()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null; // Ensure the coroutine is marked as stopped
        }
    }
}

