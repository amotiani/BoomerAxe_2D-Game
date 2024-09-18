using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightsOn : MonoBehaviour
{
    public Light2D light2D;

    void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //light yourself on fire:
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (light2D.intensity == 0f)
            {
                light2D.intensity = 1.36f;
            }
            else
            {
                light2D.intensity = 0f;
            }
        }        
    }
}
