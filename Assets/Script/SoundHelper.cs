using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    static AudioSource ads;
    //====素材====
    static AudioClip warnHL;
    static AudioClip normalbg;
    static AudioClip tpSound;
    static AudioClip enterToilet;
    static AudioClip dead;
    static AudioClip beep;
    static AudioClip ok;
    static AudioClip get;
    static AudioClip destory;

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
        ads.PlayOneShot(warnHL); //hl警报声
    }
    public static void telePort()
    {
        ads.PlayOneShot(tpSound); //传送声
    }
    public static void EnterToilet()
    {
        ads.PlayOneShot(enterToilet); //进入厕所
    }
    public static void Dead()
    {
        ads.PlayOneShot(dead); //死亡
    }
    public static void Beep()
    {
        ads.PlayOneShot(beep); //（Windows）鸣叫
    }
    public static void OK()
    {
        ads.PlayOneShot(ok); //可以
    }
    public static void Get()
    {
        ads.PlayOneShot(get); //拾取物品
    }
    public static void DestroyTree()
    {
        ads.PlayOneShot(destory); //破坏树
    }
}
