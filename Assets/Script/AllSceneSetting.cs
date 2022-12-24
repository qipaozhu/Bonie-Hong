using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSceneSetting : MonoBehaviour
{
    public static AllSceneSetting instance { get; private set; }

    //====音乐大小数值====
    public float EffectSound { get => v_EffectSound; set => v_EffectSound = value; }
    public float BackGSound { get => v_BackGSound; set => v_BackGSound = value; }
    
    float v_EffectSound = 1;
    float v_BackGSound = 0.5f;

    [Header("树的数量")]
    int v_TreeCount = 5;
    public int TreeCount { get => v_TreeCount; set => v_TreeCount = value; }

    [Header("道具生成冷却")]
    float v_PropSpawnCold = 10;
    public float PropSpawnCold { get => v_PropSpawnCold; set => v_PropSpawnCold = value; }

    [Header("林泓下一次生成")]
    float v_lhNextSpawn = 10;
    public float lhNextSpawn { get => v_lhNextSpawn; set => v_lhNextSpawn = value; }

    [Header("出没时间")]
    public float hlBonieTime = 60;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
