using UnityEngine;

public class Slash : Attack
{
    public float fadeOutTime = 0.2f;
    protected SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * moveSpeed;
        startTime = Time.time;
        startPosition = transform.position;
    }

    protected override void Update()
    {
        UpdateFadeOut();
    }

    private void UpdateFadeOut()
    {
        float timeElapsed = Time.time - startTime;
        float fadeAmount = Mathf.Clamp01(timeElapsed / fadeOutTime);
        Color color = spriteRenderer.color;
        color.a = 1 - fadeAmount;
        spriteRenderer.color = color;

        if (fadeAmount >= 1)
        {
            Destroy(gameObject);
        }
    }
}