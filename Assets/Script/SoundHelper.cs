using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHelper : MonoBehaviour
{
    //public static SoundHelper instance { get; private set; }

    static AudioSource ads;
    //====素材====
    static AudioClip warnHL;
    static AudioClip tpSound;
    static AudioClip enterToilet;
    static AudioClip dead;
    static AudioClip beep;
    static AudioClip ok;
    static AudioClip get;
    static AudioClip destory;
    static AudioClip compele;
    static AudioClip click;
    static AudioClip djrsay;
    static AudioClip fire;
    static AudioClip july5;
    static AudioClip puthead;

    //变量
    static bool isHCMSound = true;

    void Awake()
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
        compele = Resources.Load<AudioClip>("Done");
        click = Resources.Load<AudioClip>("Click");
        djrsay = Resources.Load<AudioClip>("JRsay");
        fire = Resources.Load<AudioClip>("Fire");
        july5 = Resources.Load<AudioClip>("July5");
        puthead = Resources.Load<AudioClip>("PutOutHead");
    }

    void Update()
    {
        ads.volume = AllSceneSetting.instance.EffectSound;
    }
    public void SetWarningActice(bool value)
    {
        isHCMSound = value;
    }

    public static void hCome()
    {
        if (isHCMSound)
        {
            ads.PlayOneShot(warnHL); //hl警报声
        }
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
    public static void CompeleGame()
    {
        ads.PlayOneShot(compele); //完成游戏
    }
    public static void Click()
    {
        ads.PlayOneShot(click);
    }
    public static void DjrSay()
    {
        ads.PlayOneShot(djrsay);
    }
    public static void Fire()
    {
        ads.PlayOneShot(fire);
    }
    public static void July5()
    {
        ads.PlayOneShot(july5);
    }
    public static void PutOutHead()
    {
        ads.PlayOneShot(puthead);
    }
}
