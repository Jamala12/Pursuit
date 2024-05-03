using System.Collections;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public float duration = 2.5f;
    public GameObject ghostPrefab;
    public float shakeDuration = 0.2f;  // Duration of each shake
    public float shakeMagnitude = 0.07f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnGhost();
        }
    }

    public void SpawnGhost()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        // Wait for 0.5 seconds, then shake
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Shake());

        // Wait for another 1 second (total of 1.5 seconds), then shake again
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Shake());

        // Wait for the total duration of 2.5 seconds to spawn the ghost
        yield return new WaitForSeconds(1.0f);  // Since we already waited for 1.5 seconds, wait for another 1.0 second
        SpawnGhostAtGrave();

        // Finally, destroy the grave
        Die();
    }

    void SpawnGhostAtGrave()
    {
        if (ghostPrefab != null)
        {
            Instantiate(ghostPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator Shake()
    {
        // Implement shaking logic here, similar to previous examples
        Vector3 originalPosition = transform.position;

        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;  // Reset position after shaking
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
