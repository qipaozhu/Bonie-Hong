using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestThing : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        if(Keyboard.current.nKey.wasPressedThisFrame)
        {
            CenterCtrl.instance.SetTree(-1);
        }
    }
#endif
}
