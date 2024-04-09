using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public int playerCount = 0;
    private Rigidbody2D rb;
    private Collider2D MouseCollide;
    public worldGen terrainGenerator;
    private GameObject player;
    public GameObject boatItem;
    private float boatSpeed;
    public float maxSpeed = 7;
    private Vector2 oldDirection;

    private Collider2D tileCollide;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        terrainGenerator = FindAnyObjectByType<worldGen>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        while (transform.position.x > terrainGenerator.worldWidth - 3)
        {
            transform.position += Vector3.left * Time.deltaTime;
        }
        while (transform.position.y > terrainGenerator.worldHeight - 3)
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        while (transform.position.x < 0)
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
        while (transform.position.y < 0)
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
            if (player != null)
            {

                Vector3 MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);

                Vector2 direction = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);





                if (Input.GetMouseButton(0) && MouseCollide == null)
                {
                    oldDirection = direction.normalized;
                    boatSpeed = maxSpeed;

                }
                else
                {
                    if (boatSpeed > 0)
                    {
                        boatSpeed -= Time.deltaTime * Mathf.Sqrt(maxSpeed);
                    }
                    else if (boatSpeed < 0)
                    {
                        boatSpeed = 0;
                    }
                }

            }
            else
            {
                if (boatSpeed > 0)
                {
                    boatSpeed -= Time.deltaTime * Mathf.Sqrt(maxSpeed);
                }
                else if (boatSpeed < 0)
                {
                    boatSpeed = 0;
                }

            }
        
        if (tileCollide)
        {
            //båten ska gå sönder
            Instantiate(boatItem, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

        if (playerCount >= 1)
        {
            player = transform.GetChild(0).gameObject;
        }
        else
        {
            player = null;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Mouse")
        {
            MouseCollide = collision;
        }
        if (collision.tag == "tile")
        {
            if (collision.name != "Water")
            {
                boatSpeed = -boatSpeed;
                tileCollide = collision;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Mouse")
        {
            MouseCollide = null;
        }
        if (collision.tag == "tile")
        {
            if (collision.name != "Water")
            {
                tileCollide = null;
            }
        }
    }

    private void Movement()
    {
        rb.velocity = transform.right * boatSpeed;
        float angle = Mathf.Atan2(oldDirection.y, oldDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), boatSpeed / maxSpeed * Time.deltaTime);

    }
}
