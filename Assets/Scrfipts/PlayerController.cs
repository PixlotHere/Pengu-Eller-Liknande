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
        if (Health >= 0)
        {

            #region playerMovement
            #region rotation

            Vector3 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            Vector2 direction = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);

            transform.up = direction * -1;

            #endregion
            #region Movement
            // Player Dive
            if (Input.GetKeyDown(KeyCode.Space))
            {
                slideTowards = direction.normalized;
                slide = slideLength;
            }
            if (slide >= 0)
            {
                slide -= Time.deltaTime;
            }

            // Player Movement
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            #region Vehicle
            if (Transport != null && Transport.tag == "Vehicle" && Input.GetKeyDown(KeyCode.E) && !onVehicle)
            {
                Camera.main.GetComponent<Cam>().camSpeed = 2;
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
