using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerShield : MonoBehaviour
{
    [Header("Shield Settings")]
    public float shieldDuration = 5f;          // How long the shield lasts
    public GameObject shieldVisual;            // Optional visual effect for shield
    public AudioClip shieldOnSound;            // Sound when shield activates
    public AudioClip shieldOffSound;           // Sound when shield ends

    private AudioSource audioSource;
    private bool isShieldActive = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (shieldVisual != null)
            shieldVisual.SetActive(false);      // Hide shield at start
    }

    // Call this to activate the shield
    public void ActivateShield()
    {
        if (isShieldActive)
            StopAllCoroutines();                // Reset duration if already active
        StartCoroutine(ShieldRoutine());
    }

    IEnumerator ShieldRoutine()
    {
        isShieldActive = true;

        // Show shield visual
        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        // Play shield-on sound
        if (audioSource != null && shieldOnSound != null)
            audioSource.PlayOneShot(shieldOnSound);

        // Wait for duration
        yield return new WaitForSeconds(shieldDuration);

        // End shield
        isShieldActive = false;

        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        // Play shield-off sound
        if (audioSource != null && shieldOffSound != null)
            audioSource.PlayOneShot(shieldOffSound);
    }

    // Can be used by other scripts to check if player is invulnerable
    public bool IsInvulnerable()
    {
        return isShieldActive;
    }
}
