using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThing : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EnderSky.instance.TreeOver();
        }
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    Toast.instance.InfoBox("≤‚ ‘≤‚ ‘≤‚ ‘≤‚ ‘");
        //}
    }
}
