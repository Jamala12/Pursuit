using UnityEngine;

public class Attack : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxRange = 5f;
    protected float startTime;
    protected Vector3 startPosition;
    private int damage; // Store the damage

    protected virtual void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        Move();
        CheckRange();
    }

    protected virtual void Move()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
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

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            // Assume the enemy has a component called 'EnemyHealth' that includes a TakeDamage method
            Enemy enemyHealth = hitInfo.gameObject.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("EnemyHealth component not found on the enemy.");
            }

            // Optionally destroy the attack after applying damage
            Destroy(gameObject);
        }
    }
}

