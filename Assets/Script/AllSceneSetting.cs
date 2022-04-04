using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSceneSetting : MonoBehaviour
{
    public static AllSceneSetting instance { get; private set; }
    public float EffectSound { get => v_EffectSound; set => v_EffectSound = value; }
    public float BackGSound { get => v_BackGSound; set => v_BackGSound = value; }
    public int TreeCount { get => v_TreeCount; set => v_TreeCount = value; }
    float v_EffectSound = 1;
    float v_BackGSound = 0.5f;
    int v_TreeCount = 5;

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
