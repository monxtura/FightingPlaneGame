using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{

    public bool goingUp;
    public float speed;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()

   {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    // Example using tags
    if (gameObject.tag == "Enemy1")
    {
        speed = Random.Range(3f, 5f); // normal speed
    }
    else if (gameObject.tag == "Enemy2")
    {
        speed = Random.Range(1f, 2f); // slower speed
    }
}


    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        } else if (goingUp == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }
}
