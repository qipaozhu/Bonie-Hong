using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHelper : MonoBehaviour
{
    public static SoundHelper instance { get; private set; }

    public AudioSource backgroudPlay;

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

    void Awake()
    {
        instance = this;
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
    }
    void Start()
    {
        ads.volume = AllSceneSetting.instance.EffectSound;
    }
    void Update()
    {
        ads.volume = AllSceneSetting.instance.EffectSound;
        if(backgroudPlay != null) backgroudPlay.volume = AllSceneSetting.instance.BackGSound;
    }
    //====设置背景音乐值====
    public void SetSliderValue()
    {
        GameObject.Find("BackGSlider").GetComponent<Slider>().value = AllSceneSetting.instance.BackGSound;
        GameObject.Find("EffectSlider").GetComponent<Slider>().value = AllSceneSetting.instance.EffectSound;
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
    public static void CompeleGame()
    {
        ads.PlayOneShot(compele); //完成游戏
    }
    public static void Click()
    {
        ads.PlayOneShot(click);
    }
}
