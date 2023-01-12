using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHelper : MonoBehaviour
{
    //public static SoundHelper instance { get; private set; }

    static AudioSource ads;
    //====�ز�====
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

    //����
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
            ads.PlayOneShot(warnHL); //hl������
        }
    }
    public static void telePort()
    {
        ads.PlayOneShot(tpSound); //������
    }
    public static void EnterToilet()
    {
        ads.PlayOneShot(enterToilet); //�������
    }
    public static void Dead()
    {
        ads.PlayOneShot(dead); //����
    }
    public static void Beep()
    {
        ads.PlayOneShot(beep); //��Windows������
    }
    public static void OK()
    {
        ads.PlayOneShot(ok); //����
    }
    public static void Get()
    {
        ads.PlayOneShot(get); //ʰȡ��Ʒ
    }
    public static void DestroyTree()
    {
        ads.PlayOneShot(destory); //�ƻ���
    }
    public static void CompeleGame()
    {
        ads.PlayOneShot(compele); //�����Ϸ
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
