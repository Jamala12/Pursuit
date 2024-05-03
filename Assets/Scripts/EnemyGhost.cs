using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    public float percentage = 0.5f;
    public GameObject missEffectPrefab;
    public override void TakeDamage(int damage)
    {
        percentage = Mathf.Clamp(percentage, 0f, 1f); // Ensure the percentage is within range

        if (Random.value < percentage)
        {
            base.TakeDamage(damage);  // Assuming base.TakeDamage() handles damage application correctly
        }
        else
        {
            ShowMissEffect();
        }
    }

    private void ShowMissEffect()
    {
        if (missEffectPrefab != null)
        {
            GameObject effect = Instantiate(missEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);  // Ensure effect is visible and gets cleaned up
        }
        else
        {
            Debug.LogError("Miss effect prefab not assigned.");
        }
    }
}
