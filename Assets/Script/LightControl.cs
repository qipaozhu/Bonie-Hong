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
        if (CenterCtrl.instance.isHCM)
        {
            golgalLight.intensity = golgalIntensity;
        }
        else
        {
            golgalLight.intensity = 1;
        }
    }
}
