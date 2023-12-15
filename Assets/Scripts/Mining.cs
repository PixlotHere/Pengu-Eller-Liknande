using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 40;
    public int health;
    void Start()
    {
        maxHealth = Mathf.RoundToInt(maxHealth * Random.Range(0.9f, 1.1f));
        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
