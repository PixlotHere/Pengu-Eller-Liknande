using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    private float time = 0;
    private Light2D lights;
    public float timeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        lights = this.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        lights.intensity = 0.5f + (Mathf.Sin(time*timeSpeed))/2 ;

    }
}
