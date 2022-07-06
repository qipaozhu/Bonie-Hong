using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl instance { get; private set; }
    Animator anm;
    AudioSource ads;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            anm = GetComponent<Animator>();
            ads = GetComponent<AudioSource>();
            //DontDestroyOnLoad(gameObject);
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

    public void ChangeShow()
    {
        if (anm.GetBool("isShow")) 
        {
            SoundHelper.Click();
            anm.SetBool("isShow", false);
        }
        else
        {
            SoundHelper.Click();
            anm.SetBool("isShow", true);
        }
    }
}
