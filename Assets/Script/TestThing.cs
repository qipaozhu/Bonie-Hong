using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThing : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CenterCtrl.instance.Bonie();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CenterCtrl.instance.SetTree(-99999);
            CenterCtrl.instance.GamePlayTimeS = -99999;
        }
    }
}
