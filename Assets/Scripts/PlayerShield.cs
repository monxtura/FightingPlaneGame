using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerShield : MonoBehaviour
{
    [Header("Shield Settings")]
    public float shieldDuration = 5f;
    public GameObject shieldVisual;
    public AudioClip shieldOnSound;
    public AudioClip shieldOffSound;

    private AudioSource audioSource;
    private bool isShieldActive = false;
    private Coroutine shieldCoroutine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (shieldVisual != null)
            shieldVisual.SetActive(false);
    }

    public void ActivateShield()
    {
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldRoutine());
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

        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        if (shieldOffSound != null)
            audioSource.PlayOneShot(shieldOffSound);

        shieldCoroutine = null;
    }
}

