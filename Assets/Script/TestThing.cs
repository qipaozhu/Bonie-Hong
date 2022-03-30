using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThing : MonoBehaviour
{
    int va;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CenterCtrl.instance.SetTree(-100);
        }
    }
}
