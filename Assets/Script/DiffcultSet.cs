using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffcultSet : MonoBehaviour
{

    //====ƒ—∂»…Ë÷√====
    public void Easy()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(15, 20);
        AllSceneSetting.instance.lhNextSpawn = 40;
        AllSceneSetting.instance.PropSpawnCold = 230;
        AllSceneSetting.instance.hlBonieTime = 70;
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
    public void Normal()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(45, 50);
        AllSceneSetting.instance.lhNextSpawn = 25;
        AllSceneSetting.instance.PropSpawnCold = 310;
        AllSceneSetting.instance.hlBonieTime = 110;
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
    public void Hard()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(70, 75);
        AllSceneSetting.instance.lhNextSpawn = 9;
        AllSceneSetting.instance.PropSpawnCold = 480;
        AllSceneSetting.instance.hlBonieTime = 180;
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
}
