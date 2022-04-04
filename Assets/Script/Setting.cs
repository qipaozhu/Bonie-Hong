using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{

    //====背景音乐设置====
    public void SetBGSoundValue(float value)
    {
        AllSceneSetting.instance.BackGSound = value;
    }
    //====效果音乐设置====
    public void SetEFSoundValue(float value)
    {
        AllSceneSetting.instance.EffectSound = value;
    }

    //====难度设置
    public void Easy()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(15, 20);
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
    public void Normal()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(45, 50);
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
    public void Hard()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(70, 75);
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
}
