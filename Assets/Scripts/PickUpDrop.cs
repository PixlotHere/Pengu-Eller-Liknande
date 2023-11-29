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
    private float pickupDelay;
    public float WaitTime = 0.5f;
    private WeaponController weaponScript;
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

        //Pickup
        if (holdingLeft == null && Input.GetMouseButtonDown(0) && (item != holdingRight) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            if (item != null)
            {
                pickupDelay = WaitTime;
                holdingLeft = item.gameObject;
                holdingLeft.transform.position = leftHand.position;
                holdingLeft.transform.rotation = leftHand.rotation;
                item.gameObject.transform.parent = leftHand;
                item.GetComponent<WeaponController>().heldHand = leftHand;
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
                item.gameObject.transform.parent = rightHand;
                item.GetComponent<WeaponController>().heldHand = rightHand;
            }
        }
        //Dropp
        if (holdingLeft != null && Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            pickupDelay = WaitTime;
            holdingLeft.transform.parent = null;
            holdingLeft.GetComponent<WeaponController>().heldHand = null;
            holdingLeft = null;
        }
        if (holdingRight != null && Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftControl) && pickupDelay <= 0)
        {
            pickupDelay = WaitTime;
            holdingRight.transform.parent = null;
            holdingRight.GetComponent<WeaponController>().heldHand = null;
            holdingRight = null;
        }
        //Use Tool
        if (holdingLeft != null && Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftControl))
        {
                holdingLeft.GetComponent<WeaponController>().Attack();
        }
        if (holdingRight != null && Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftControl))
        {
            holdingRight.GetComponent<WeaponController>().Attack();
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
