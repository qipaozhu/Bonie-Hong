using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    public static AudioSource ads;
    //====ËØ²Ä====
    public static AudioClip warnHL;
    public static AudioClip normalbg;

    void Start()
    {
        ads = GetComponent<AudioSource>();
        warnHL = Resources.Load<AudioClip>("Hcome");
    }

    public static void hCome()
    {
        ads.PlayOneShot(warnHL);
    }
}
