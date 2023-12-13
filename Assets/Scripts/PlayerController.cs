using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    #region varStats
    float slide;
    public float slideLength;
    Vector2 slideTowards;
    public float slideSpeed;
    private bool onVehicle = false;

    private Collider2D Transport;
    public worldGen terrainGenerator;
    #endregion
    #region varPlayer
    private Rigidbody2D rb;
    public float moveSpeed = 5;
    private float Health = 100;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (moveSpeed * 10 != slideSpeed)
        {
            slideSpeed = moveSpeed * 10;
        }
        
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
        float x = 0;
        float y = 0;
        if (Health >= 0)
        {
            

            #region playerMovement
            #region rotation

            Vector3 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            Vector2 direction = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);

            transform.up = direction;

            #endregion
            #region Movement
            // Player Dive
            if (Input.GetKeyDown(KeyCode.Space) && slide <= 0)
            {
                slideTowards = direction.normalized;
                slide = slideLength;
            }
            if (slide >= 0)
            {
                slide -= Time.deltaTime;
            }

            // Player Movement
            
            if (Input.GetKey(KeyCode.W))
            {
                y += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x -= 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                y -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += 1;
            }


            #region Vehicle
            if (Transport != null && Transport.tag == "Vehicle" && Input.GetKeyDown(KeyCode.E) && !onVehicle)
            {
               Camera.main.GetComponent<Cam>().zoom = 2;
                Camera.main.GetComponent<Cam>().camSpeed = 1.5f;

                onVehicle = true;
                transform.SetParent(Transport.transform);
                Transport.GetComponent<BoatScript>().playerCount += 1;
            }
            if (transform.parent != null && transform.parent.GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
            {
                rb.velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
                
            }
            else
            {
                
                rb.velocity = new Vector2(x, y).normalized * moveSpeed + slideTowards * slide * slideSpeed / 10;
                rb.velocity = new Vector2(Mathf.Round(rb.velocity.x/(moveSpeed/10))*(moveSpeed/10), Mathf.Round(rb.velocity.y/ (moveSpeed / 10)) * (moveSpeed / 10));
            }
            if (transform.parent == null)
            {
                Camera.main.GetComponent<Cam>().zoom = 1;
                Camera.main.GetComponent<Cam>().camSpeed = 1;
            }


            #endregion

            #endregion

            #endregion
        }
        else
        {

        }



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vehicle")
        Transport = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Vehicle" && transform.parent != null)
        {
           Camera.main.GetComponent<Cam>().camSpeed = 1;
            transform.parent.GetComponent<BoatScript>().playerCount -= 1;
            transform.parent = null;
            Transport = null;
            onVehicle = false;

        }
    }

}
