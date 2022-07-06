using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyXControl : MonoBehaviour
{
    AudioSource ads;

    void Start()
    {
        ads = GetComponent<AudioSource>();
    }


    void Update()
    {
        ads.volume = AllSceneSetting.instance.EffectSound;
    }
}
