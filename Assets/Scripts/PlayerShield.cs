using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerShield : MonoBehaviour
{
    [Header("Shield Settings")]
    public float shieldDuration = 5f;          // How long the shield lasts
    public GameObject shieldVisual;            // Optional shield visual (sprite or particle)
    public AudioClip shieldOnSound;            // Sound when shield activates
    public AudioClip shieldOffSound;           // Sound when shield ends

    private AudioSource audioSource;
    private bool isShieldActive = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (shieldVisual != null)
            shieldVisual.SetActive(false); // Hide shield at start
    }

    // Call this to activate the shield
    public void ActivateShield()
    {
        if (isShieldActive)
            StopAllCoroutines(); // Reset timer if shield already active

        StartCoroutine(ShieldRoutine());
    }

    IEnumerator ShieldRoutine()
    {
        isShieldActive = true;

        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        if (shieldOnSound != null)
            audioSource.PlayOneShot(shieldOnSound);

        yield return new WaitForSeconds(shieldDuration);

        isShieldActive = false;

        if (shieldVisual != n
