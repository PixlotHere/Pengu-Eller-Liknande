using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    public static AudioManager audioManage;
    private float time = 0;
    private Light2D lights;
    public float timeSpeed;
    private bool day = false;

    public int minSpawn;
    public int maxSpawn;
    public int spawntime;
    public float spawn;
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        lights = this.GetComponent<Light2D>();
        audioManage = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        spawn -= Time.deltaTime;
        lights.intensity = 0.5f + (Mathf.Sin(time*timeSpeed))/2 ;
        if (lights.intensity < 0.5)
        {
            day = false;
        }
        else
        {
            day = true;
        }

        if (day)
        {
            audioManage.PlayMusic("Day Theme");
        }
        else
        {
            audioManage.PlayMusic("Night Theme");
        }
        if (spawn <= 0)
        {
            spawn = spawntime * Random.Range(0.70f, 1.30f);
            int spawnAmount = Random.Range(minSpawn, maxSpawn);
            if (spawnAmount > 0)
            {
                int randomNumber = Random.Range(0, enemies.Length -1);
                spawnAmount--;
                if (day)
                {
                    if (Random.Range(0,2) == 1)
                    {
                        Instantiate(enemies[randomNumber]);
                    }
                }
                else
                {
                    Instantiate(enemies[randomNumber]);
                }
            }
            
        }

    }
}
