using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.;

public class Login : MonoBehaviour
{
    public static Login instance { get; private set; }

    private void Start()
    {
        //验证是否为正版
        GameObject peVer = GameObject.Find("PeopleFromNotice");
#if UNITY_ANDROID

        if (peVer != null)
        {
            peVer.GetComponent<Text>().text = "手机版";
        }
        AllSceneSetting.instance.isRealPlayer = true;
        gameObject.SetActive(false);
        return;
        
#else
        

#if UNITY_EDITOR
        string[] people = { "aaa", "在游戏里测试", "VhOA91uIA" };
#else
        string[] people = Environment.GetCommandLineArgs();
#endif
        //1.长度不是3
        if (people.Length != 3)
        {
            AllSceneSetting.instance.isRealPlayer = false;
            if (peVer != null)
            {
                peVer.GetComponent<Text>().text = "试用版";
            }
            return;
        }
        //2.第三位不是密码
        if (people[2] != "VhOA91uIA")
        {
            AllSceneSetting.instance.isRealPlayer = false;
            if(peVer != null)
            {
                peVer.GetComponent<Text>().text = "试用版";
            }
            return;
        }

        //是的话
        AllSceneSetting.instance.isRealPlayer = true;

        //提示
        if (peVer != null)
        {
            peVer.GetComponent<Text>().text = people[1] + "版 - 正版";
        }
        gameObject.SetActive(false);
#endif
    }
}
