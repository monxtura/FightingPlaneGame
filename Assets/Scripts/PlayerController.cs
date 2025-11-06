using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 4.0f;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
    horizontalInput = Input.GetAxis("Horizontal");
    verticalInput = Input.GetAxis("Vertical");
    transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

    float horizontalScreenSize = gameManager.horizontalScreenSize;
    float verticalScreenSize = gameManager.verticalScreenSize;

    // --- Horizontal wrap-around ---
    if (transform.position.x < -horizontalScreenSize)
    {
        transform.position = new Vector3(horizontalScreenSize, transform.position.y, 0);
    }
    else if (transform.position.x > horizontalScreenSize)
    {
        transform.position = new Vector3(-horizontalScreenSize, transform.position.y, 0);
    }

    // --- Vertical movement limits (bottom half only) ---
    float minY = -verticalScreenSize; // bottom of screen
    float maxY = 1;                   // middle of screen
    float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

    transform.position = new Vector3(transform.position.x, clampedY, 0);

    float newX = transform.position.x;
    float halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2; // account for sprite size

    if (transform.position.x + halfWidth < -gameManager.horizontalScreenSize)
   {
    newX = gameManager.horizontalScreenSize + halfWidth;
   }
   else if (transform.position.x - halfWidth > gameManager.horizontalScreenSize)
  {
    newX = -gameManager.horizontalScreenSize - halfWidth;
   }
   

    }
}
