using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightControl_human : MonoBehaviour
{
    public Light2D golgalLight;
    public float golgalIntensity;

    void Update()
    {
        if (!EnderSky_human.instance.WillOver)
        {
            golgalLight.intensity = golgalIntensity;
        }
        else
        {
            golgalLight.intensity = 0.2f;
        }
    }
}
