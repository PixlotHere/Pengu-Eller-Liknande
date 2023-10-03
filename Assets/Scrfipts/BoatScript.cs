using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public int playerCount = 0;
    private Rigidbody2D rb;
    private Collider2D MouseCollide;
    private GameObject player;
    private float boatSpeed;
    public float maxSpeed = 7;
    private Vector2 oldDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (player != null)
        {
            Vector3 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            Vector2 direction = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);
            




            if (Input.GetMouseButton(0) && MouseCollide == null)
            {
                oldDirection = direction.normalized * -1;
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

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Mouse")
        {
            MouseCollide = null;
        }
    }

    private void Movement()
    {
        rb.velocity = transform.right * -boatSpeed;
        float angle = Mathf.Atan2(oldDirection.y, oldDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), boatSpeed / maxSpeed * Time.deltaTime);

    }
}
