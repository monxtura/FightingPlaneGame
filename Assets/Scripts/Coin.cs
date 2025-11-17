using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifetime = 3f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // No movement
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}