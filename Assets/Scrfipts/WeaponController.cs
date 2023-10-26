using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Info")]
    public Transform heldHand;
    public string weaponName;
    public string weaponClass;
    public string weaponInfo;
    [Header("Weapon Stats")]
    public float dmg;
    public float critChance;
    public float useSpeed;
    public float swingSpeed;
    private float swingTimer;
    public float knockBack;
    [Header("Weapon Resource")]
    public bool resource;
    public int ammo;
    [Header("Weapon projectile")]
    public bool projectile;
    public GameObject projectileOb;
    // Start is called before the first frame update
    // Update is called once per frame

    public void Update()
    {
        if (swingTimer < useSpeed)
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

        if (swingTimer >= useSpeed)
        {
            swingTimer = 0;
            CalculateDMG();
            Debug.Log(CalculateDMG());
        }



    }

    private int CalculateDMG()
    {

        if (Random.Range(1, 100) <= critChance * 100)
        {
            return Mathf.RoundToInt(dmg * 1.5f * Random.Range(0.95f, 1.05f));
        }
        else
        {
            return Mathf.RoundToInt(dmg *  Random.Range(0.90f, 1.10f));
        }
        
    }

}
