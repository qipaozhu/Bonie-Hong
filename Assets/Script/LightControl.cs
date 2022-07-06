using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    public Light2D golgalLight;
    public float golgalIntensity;

    void Update()
    {
        if (CenterCtrl.instance.isHCM && !EnderSky.instance.WillOver)
        {
            golgalLight.intensity = golgalIntensity;
        }
        else if(EnderSky.instance.WillOver)
        {
            golgalLight.intensity = .7f;
        }
        else
        {
            golgalLight.intensity = 1;
        }
    }
}
