using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThing : MonoBehaviour
{
    int va;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SoundHelper.hCome();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerCollect.instance.ChangeHealth(-5);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            va++;
            if (va == 3) va = 1;
            GetComponent<HlinControl>();
            HlinControl.instance.SetTarget(va);
        }
    }
}
