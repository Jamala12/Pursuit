using UnityEngine;

public class Projectile : Attack
{
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * moveSpeed;
    }

    protected void Update()
    {
        CheckRange(); // Check if the projectile has traveled beyond its maximum range
    }

    void  OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}


