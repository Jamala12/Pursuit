using System.Collections;
using UnityEngine;

public class LargeSlash : MonoBehaviour
{
    public float moveSpeed = 2f;         // Speed at which the slash will move forward
    public float fadeOutTime = 0.2f;     // Time in seconds over which the slash will fade out
    private SpriteRenderer spriteRenderer; // Renderer for the slash sprite
    private float startTime;             // To track the fading time

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the slash prefab.");
            Destroy(gameObject); // Destroy if no SpriteRenderer is attached to prevent errors
            return;
        }

        // Set the start time
        startTime = Time.time;
    }

    void Update()
    {
        // Move the slash forward
        transform.position += transform.right * moveSpeed * Time.deltaTime * 0.5f;

        // Calculate the fraction of the fade out time that has passed
        float timeElapsed = Time.time - startTime;
        float fadeAmount = Mathf.Clamp01(timeElapsed / fadeOutTime);

        // Update the alpha of the sprite based on the time
        Color color = spriteRenderer.color;
        color.a = 1 - fadeAmount; // Linear fade out from opaque to transparent
        spriteRenderer.color = color;

        // Destroy the object when it's completely faded out
        if (fadeAmount >= 1)
        {
            Destroy(gameObject);
        }
    }
}
