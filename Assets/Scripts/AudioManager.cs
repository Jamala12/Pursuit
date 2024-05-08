using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Define separate AudioSource children for different sound types
    public AudioSource footstepSource;
    public AudioSource damageSource;

    private void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize AudioSource references from child components
        footstepSource = transform.Find("FootstepSource").GetComponent<AudioSource>();
        damageSource = transform.Find("DamageSource").GetComponent<AudioSource>();
    }

    // General method to play sounds from a specific source
    public void PlaySound(AudioClip clip, AudioSource source)
    {
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip or source is not assigned.");
        }
    }
}
