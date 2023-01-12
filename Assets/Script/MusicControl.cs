using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl instance { get; private set; }
    AudioSource ads;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            ads = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        ads.volume = AllSceneSetting.instance.BackGSound;
    }
}
