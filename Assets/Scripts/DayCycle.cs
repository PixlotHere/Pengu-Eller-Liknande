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

    }
}
