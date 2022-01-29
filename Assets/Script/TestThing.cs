using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThing : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SoundHelper.hCome();
        }
    }
}
