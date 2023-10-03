using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector2 direction = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);
        Cursor.visible = false;
        transform.position += (Vector3)direction;
    }
}
