using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float detectionRadius = 5.0f;
    public int damage = 5;
    public int health = 15;
    protected Transform player;
    protected Rigidbody2D rb;
    private Coroutine damageCoroutine; // To keep track of the damage coroutine
    protected bool isChase = true;
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer component

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    protected virtual void FixedUpdate()
    {
        Chase();
    }

    protected virtual void Chase()
    {
        if (player != null && isChase == true)
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
            isChase = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopDamageCoroutine();
            isChase = true;
        }
    }

    private IEnumerator ApplyDamage()
    {
        while (true)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            yield return new WaitForSeconds(0.5f);
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

    public virtual void TakeDamage(int damage)
    {
        health -= damage;  // Reduce health by the damage amount
        if (health <= 0)
        {
            Die();  // Call the die function if health falls below or equals zero
        }
        else
        {
            StartCoroutine(FlashColor(Color.red));  // Flash color when taking damage
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");  // Log message or handle as needed
        Destroy(gameObject);  // Remove enemy from game
    }

    private IEnumerator FlashColor(Color color)
    {
        spriteRenderer.color = color;  // Change the color to red
        yield return new WaitForSeconds(0.1f);  // Wait for 0.1 seconds
        spriteRenderer.color = Color.white;  // Reset the color to white
    }
}


