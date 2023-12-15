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
    private Collider2D Collide; //Check of the collision.
    // Start is called before the first frame update
    // Update is called once per frame

    public void Update()
    {
        Debug.Log(attacking);
        if (swingTimer < useSpeed)
        {
            swingTimer += Time.fixedDeltaTime;
        }   
        if (!attacking)
        {
            Collide = null;
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

        if (swingTimer >= useSpeed)
        {
            attacking = true;
            swingTimer = 0;
            if (weaponClass == "Mining")
            {
                if (Collide.GetComponent<Mining>())
                {
                    Collide.GetComponent<Mining>().health -= CalculateDMG();
                }
            }
            else
            {

            }
            CalculateDMG();
            StartCoroutine(Animate());
        }



    }

    private int CalculateDMG()
    {
        if (Collide != null)
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
        else
        {
            return 0;
        }

    }

    public IEnumerator Animate()
    {
        heldHand.GetComponent<Animator>().speed = ((useSpeed - 1) * -1) + 1;
        heldHand.GetComponent<Animator>().SetInteger("useStyle", useStyle);
        yield return new WaitForSeconds(useSpeed);
        attacking = false;
        heldHand.GetComponent<Animator>().SetInteger("useStyle", 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacking)
        {
            Collide = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Collide = null;
    }


}
