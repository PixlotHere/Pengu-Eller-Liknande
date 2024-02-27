using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float zoom = 1;
    public float camSpeed = 1;
    public float camSize;
    // Start is called before the first frame update
    public GameObject player;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,  zoom * camSize, Time.deltaTime);
        Vector2 targetPos = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        rb.velocity = (targetPos * camSpeed * 2.5f);
    }
}
