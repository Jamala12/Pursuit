using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelProp : Prop
{
    public GameObject explosionPrefab;
    public SpriteRenderer spriteRenderer;

    public override void Die()
    {
        StartCoroutine(FlashAndExplode());
    }

    private IEnumerator FlashAndExplode()
    {
        Color originalColor = spriteRenderer.color;  // Store the original color
        int flashCount = 3;  // Number of flashes
        float flashDuration = 1f;  // Total duration to flash

        for (int i = 0; i < flashCount * 2; i++)
        {
            // Toggle color between red and original
            spriteRenderer.color = spriteRenderer.color == Color.red ? originalColor : Color.red;

            // Wait for half the interval
            yield return new WaitForSeconds(flashDuration / (flashCount * 2));
        }

        // color is reset
        spriteRenderer.color = originalColor;

        // Instantiate the explosion prefab
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Call base Die to destroy the barrel
        base.Die();
    }


}

