using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    public GameObject holdingRight;
    public GameObject holdingLeft;
    public Collider2D item;
    public Transform leftHand;
    public Transform rightHand;
    float useLeft;
    float useRight;
    public float useToolTime = 2;
    private float pickupDelay;
    public float WaitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pickupDelay > 0)
        {
            pickupDelay -= Time.deltaTime;
        }
        //Tool In Use Timer --
        if (useRight >= 0)
        {
            useRight -= Time.deltaTime;
            if (holdingRight != null)
            {

            }
        }
        if (useLeft >= 0)
        {
            useLeft -= Time.deltaTime;
        }

        //Pickup
        if (holdingLeft == null && Input.GetMouseButtonDown(0) && (item != holdingRight) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            if (item != null)
            {
                pickupDelay = WaitTime;
                holdingLeft = item.gameObject;
                holdingLeft.transform.position = leftHand.position;
                holdingLeft.transform.rotation = leftHand.rotation;
                item.gameObject.transform.parent = transform;
            }
        }
        if (holdingRight == null && Input.GetMouseButtonDown(1) && (item != holdingLeft) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            if (item != null)
            {
                pickupDelay = WaitTime;
                holdingRight = item.gameObject;
                holdingRight.transform.position = rightHand.position;
                holdingRight.transform.rotation = rightHand.rotation;
                item.gameObject.transform.parent = transform;
            }
        }
        //Dropp
        if (holdingLeft != null && Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            pickupDelay = WaitTime;
            holdingLeft.transform.parent = null;
            holdingLeft = null;
        }
        if (holdingRight != null && Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            pickupDelay = WaitTime;
            holdingRight.transform.parent = null;
            holdingRight = null;
        }
        //Use Tool
        if (holdingLeft != null && Input.GetMouseButtonDown(0) && useLeft <= 0)
        {
            useLeft = useToolTime;
        }
        if (holdingRight != null && Input.GetMouseButtonDown(1) && useRight <= 0)
        {
            useRight = useToolTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tool")
            item = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tool")
            item = null;
    }
}
