using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AndroidSupport : MonoBehaviour
{
    public void AndroidJiaohu()
    {
        PlayerCollect.instance.Echicken();
    }
    public void AndroidMap()
    {
        UIManager.main.OnMap();
    }

    private void Start()
    {
#if UNITY_EDITOR
        return;
#endif
        if(Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }
}
