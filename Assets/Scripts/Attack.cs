using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxRange = 5f;
    protected float startTime;
    protected Vector3 startPosition;
    public int damage; // Store the damage
    protected Rigidbody2D rb; // Moved to protected to allow access in derived classes

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * moveSpeed;
        startTime = Time.time;
        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        CheckRange();
    }

    protected virtual void CheckRange()
    {
        if (Vector3.Distance(startPosition, transform.position) >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeDamage(int damage)
    {
        this.damage = damage; // Initialize the damage
    }

    protected virtual void OnTriggerEnter2D(Collider2D hitInfo)
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
                Debug.LogError("Enemy component script not found on collided object.");
            }
        }
        else if (hitInfo.gameObject.CompareTag("Prop"))
        {
            Prop prop = hitInfo.gameObject.GetComponent<Prop>();
            if (prop != null)
            {
                prop.TakeDamage();
            }
            else
            {
                Debug.LogError("Prop component script not found on collided object.");
            }
        }
    }

    public void ApplyDamageBoost(float multiplier, float boostDuration)
    {
        StartCoroutine(BoostDamage(multiplier, boostDuration));
    }

    private IEnumerator BoostDamage(float multiplier, float duration)
    {
        damage = (int)(damage * multiplier);
        yield return new WaitForSeconds(duration);
        damage = (int)(damage / multiplier);
    }

}