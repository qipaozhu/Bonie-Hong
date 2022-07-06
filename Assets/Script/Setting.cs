using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public static Setting instance { get; private set; }
    Animator ani;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            ani = GetComponent<Animator>();
        }
        else Destroy(gameObject);
    }

    public void ShowSetting()
    {
        Debug.Log("Show…Ë÷√");
        if (ani.GetBool("isShow"))
        {
            ani.SetBool("isShow", false);
        }
        else ani.SetBool("isShow", true);
    }

    //====±≥æ∞“Ù¿÷…Ë÷√====
    public void SetBGSoundValue(float sound)
    {
        AllSceneSetting.instance.BackGSound = sound;
    }
    //====–ßπ˚“Ù¿÷…Ë÷√====
    public void SetEFSoundValue(float sound)
    {
        AllSceneSetting.instance.EffectSound = sound;
    }

}
