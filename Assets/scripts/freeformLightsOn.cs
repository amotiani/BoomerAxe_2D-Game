using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class freeformLightsOn : MonoBehaviour
{
    public Light2D freeform;

    void Awake()
    {
        freeform = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //light yourself on fire:
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (freeform.intensity == 0f)
            {
                freeform.intensity = 3.21f;
            }
            else
            {
                freeform.intensity = 0f;
            }
        }
        
    }
}
