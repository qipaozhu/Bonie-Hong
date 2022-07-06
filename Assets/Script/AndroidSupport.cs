using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidSupport : MonoBehaviour
{
    public void AndroidJiaohu()
    {
        PlayerCollect.instance.Echicken();
    }
    public void AndroidItem()
    {
        UIManager.main.OnItem();
    }
    public void AndroidProp()
    {
        UIManager.main.OnProp();
    }
    public void AndroidMap()
    {
        UIManager.main.OnMap();
    }
    public void AndroidPause()
    {
        MenuControl.instance.Pause();
    }
}
