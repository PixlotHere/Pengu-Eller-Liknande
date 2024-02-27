using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Info")]
    public Transform heldHand; //What hand the weapon is in
    public string weaponName; //Weapons name
    public string weaponClass; //The dmg class of the weapon
    public string weaponInfo; //Info about the weapon
    [Header("Weapon Stats")]
    public float dmg; //The dmg per swing
    public float critChance; //Chance to do 150% dmg
    public float useSpeed; //How fast you swing
    private float swingTimer; //Delay between swings
    public float knockBack; //How far enemies flies of your weapon
    public int useStyle; //What animation should play
    [Header("Weapon Resource")]
    public bool resource; //If the weapon need some resource to work
    public int ammo; //How much ammo u have for the weapon
    [Header("Weapon projectile")]
    public bool projectile; //If it has a projectile
    public GameObject projectileOb; //The gameObject of the Projectile
    [Header("Other")]
    public bool attacking = false; //If i am attacking
    private List<Collider2D> Colliders = new List<Collider2D>();
    // Start is called before the first frame update
    // Update is called once per frame

    public void Update()
    {
        if (heldHand != null && swingTimer < heldHand.GetComponent<Animator>().speed)
        {
            swingTimer += Time.deltaTime;
        }

    }
    public void Attack()
    {
        if (transform.parent != null)
        {
            Swing();
        }
    }

    private void Swing()
    {

        if (swingTimer >= heldHand.GetComponent<Animator>().speed)
        {
            attacking = true;
            StartCoroutine(Animate());
            swingTimer = 0;
            if (weaponClass == "Mining" && attacking)
            {
                for (int i = 0; i < Colliders.Count; i++)
                {
                    Debug.Log(Colliders[i] + ", " + Colliders.Count);
                    if (Colliders[i] != null && Colliders[i].GetComponent<Mining>())
                    {
                        attacking = false;
                        Colliders[i].GetComponent<Mining>().health -= CalculateDMG();

                    }
                }
                
            }
            else if (weaponClass == "Placable" && attacking)
            {
                attacking = false;
            }
            else if (attacking)
            {
                for (int i = 0; i < Colliders.Count; i++)
                {
                    Debug.Log(Colliders[i] + ", " + Colliders.Count);
                    if (Colliders[i] != null && Colliders[i].GetComponent<Mining>())
                    {
                        attacking = false;
                        Colliders[i].GetComponent<Mining>().health -= CalculateDMG();

                    }
                }
            }
            //transform.parent.gameObject.transform.localRotation = Quaternion.Euler(heldHand.transform.rotation.eulerAngles.x, heldHand.transform.rotation.eulerAngles.y, 45);
            //transform.parent.gameObject.transform.localPosition = new Vector2();
            
        }



    }

    private int CalculateDMG()
    {
        
            if (Random.Range(1, 100) <= critChance * 100) //crit
            {
                return Mathf.RoundToInt(dmg * 1.5f * Random.Range(0.95f, 1.05f));
            }
            else
            {
                return Mathf.RoundToInt(dmg * Random.Range(0.90f, 1.10f));
            }
    }

    public IEnumerator Animate()
    {
        heldHand.GetComponent<Animator>().speed = ((useSpeed - 1) * -1)*2;
        heldHand.GetComponent<Animator>().SetInteger("useStyle", useStyle);
        yield return new WaitForSeconds(heldHand.GetComponent<Animator>().speed);
            attacking = false;
            if (heldHand.name == "RightHand")
            {
                heldHand.localPosition = new Vector3(0.75f, 1.5f, heldHand.parent.transform.position.z);
            }
            else
            {
                heldHand.localPosition = new Vector3(-0.75f, 1.5f, heldHand.parent.transform.position.z);
            }
            heldHand.localRotation = Quaternion.Euler(heldHand.rotation.eulerAngles.x, heldHand.rotation.eulerAngles.y, 45);
            heldHand.GetComponent<Animator>().SetInteger("useStyle", 0);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Colliders.Contains(collision))
        {
            Colliders.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Colliders.Contains(collision))
        {
            Colliders.Remove(collision);
        }
    }


}
