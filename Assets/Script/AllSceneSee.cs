using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSceneSee : MonoBehaviour
{
    public static AllSceneSee instance { get; private set; }

    void Start()
    {
        instance = this;
    }

    public void CallSettingMenu()
    {
        Setting.instance.ShowSetting();
    }
}
