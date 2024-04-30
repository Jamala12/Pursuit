using UnityEngine;

public class Slash : Attack
{
    public float fadeOutTime = 0.2f;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the slash prefab.");
            Destroy(gameObject);
            return;
        }
    }

    protected void Update()
    {
        Move(); // Call the base move method
        UpdateFadeOut();
    }

    protected override void Move()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
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


