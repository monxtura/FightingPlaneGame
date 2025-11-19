using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;
    private int weaponType;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;
    

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5f;
        weaponType = 1;
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

        //Do I have a shield? If yes: do not lose a life, but instead deactivate the shield's visibility
        //If not: lose a life
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }




  IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }




    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }


    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {

        if(whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
              switch (whichPowerup)
            {
                case 1:
                    speed = 10f;
                    //start coroutine 
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1); 
                    break;
                case 2:
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    //Picked up shield
                    //Do I already have a shield?
                    //If yes: do nothing
                    //If not: activate the shield's visibility
                    gameManager.ManagePowerupText(4);
                    break;
                case 4:
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
            }
        }
    }





    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
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
