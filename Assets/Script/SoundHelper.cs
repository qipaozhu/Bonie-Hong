using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    public static AudioSource ads;
    //====ËØ²Ä====
    public static AudioClip warnHL;
    public static AudioClip normalbg;
    public static AudioClip tpSound;
    public static AudioClip enterToilet;
    public static AudioClip dead;
    public static AudioClip beep;
    public static AudioClip ok;
    public static AudioClip get;
    public static AudioClip destory;

    void Start()
    {
        ads = GetComponent<AudioSource>();
        warnHL = Resources.Load<AudioClip>("Hcome");
        tpSound = Resources.Load<AudioClip>("Teleport");
        enterToilet = Resources.Load<AudioClip>("EnterToilet");
        dead = Resources.Load<AudioClip>("Dead");
        beep = Resources.Load<AudioClip>("Beep");
        ok = Resources.Load<AudioClip>("OK");
        get = Resources.Load<AudioClip>("Get");
        destory = Resources.Load<AudioClip>("Dest");
    }

    public static void hCome()
    {
        ads.PlayOneShot(warnHL); //hl¾¯±¨Éù
    }
    public static void telePort()
    {
        ads.PlayOneShot(tpSound); //´«ËÍÉù
    }
    public static void EnterToilet()
    {
        ads.PlayOneShot(enterToilet); //½øÈë²ÞËù
    }
    public static void Dead()
    {
        ads.PlayOneShot(dead); //ËÀÍö
    }
    public static void Beep()
    {
        ads.PlayOneShot(beep);
    }
    public static void OK()
    {
        ads.PlayOneShot(ok);
    }
    public static void Get()
    {
        ads.PlayOneShot(get);
    }
    public static void DestroyTree()
    {
        ads.PlayOneShot(destory);
    }
}
