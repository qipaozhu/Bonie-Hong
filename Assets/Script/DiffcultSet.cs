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
        AllSceneSetting.instance.lhNextSpawn = 60;
        AllSceneSetting.instance.PropSpawnCold = 120;
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
    public void Normal()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(45, 50);
        AllSceneSetting.instance.lhNextSpawn = 45;
        AllSceneSetting.instance.PropSpawnCold = 160;
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
    public void Hard()
    {
        Debug.Log(AllSceneSetting.instance.TreeCount);
        AllSceneSetting.instance.TreeCount = Random.Range(70, 75);
        AllSceneSetting.instance.lhNextSpawn = 20;
        AllSceneSetting.instance.PropSpawnCold = 240;
        Debug.Log(AllSceneSetting.instance.TreeCount);
    }
}
