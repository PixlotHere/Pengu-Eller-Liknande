using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    private Color Opacity;
    // Update is called once per frame

    //private void Start()
    //{
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Opacity = new Color(1, 1, 1, 0.5f);
            this.GetComponent<SpriteRenderer>().color = Opacity;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Opacity = new Color(1, 1, 1, 1f);
            this.GetComponent<SpriteRenderer>().color = Opacity;
        }
    }
}
