using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSceneUICtrl : MonoBehaviour
{
    public static AllSceneUICtrl instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetSliderFirstValue();
    }
    public void SoundClick()
    {
        SoundHelper.Click();
    }

    void SetSliderFirstValue()
    {
        GameObject.Find("BackGSlider").GetComponent<Slider>().value = AllSceneSetting.instance.BackGSound;
        GameObject.Find("EffectSlider").GetComponent<Slider>().value = AllSceneSetting.instance.EffectSound;
    }
}
