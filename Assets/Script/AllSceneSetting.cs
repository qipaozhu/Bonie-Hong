using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSceneSetting : MonoBehaviour
{
    public static AllSceneSetting instance { get; private set; }

    //====���ִ�С��ֵ====
    public float EffectSound { get => v_EffectSound; set => v_EffectSound = value; }
    public float BackGSound { get => v_BackGSound; set => v_BackGSound = value; }
    
    float v_EffectSound = 1;
    float v_BackGSound = 0.5f;

    //====��������====
    int v_TreeCount = 5;
    public int TreeCount { get => v_TreeCount; set => v_TreeCount = value; }

    //====����������ȴ====
    float v_PropSpawnCold = 10;
    public float PropSpawnCold { get => v_PropSpawnCold; set => v_PropSpawnCold = value; }

    //====������һ������====
    float v_lhNextSpawn = 10;
    public float lhNextSpawn { get => v_lhNextSpawn; set => v_lhNextSpawn = value; }

    //====������֤====
    [SerializeField]
    bool v_isPlayer = false;
    public bool isRealPlayer { get => v_isPlayer; set => v_isPlayer = value; }

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
