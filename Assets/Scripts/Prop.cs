using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour
{
    public int health = 2;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.07f; 

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;  // Wait until next frame
        }

        transform.position = originalPosition;  // Reset position after shaking
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
