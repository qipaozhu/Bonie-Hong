using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropsAndItems : MonoBehaviour
{
    [Header("游戏对象")]
    public GameObject chair;
    [Header("道具栏")]
    public int prop1AddHealth;
    [Header("物品栏")]
    public Button bigWCWater;
    //用遗照
    public void UseDP()
    {
        //PlayDisable.instance.playIsDisable
        if (PlayerCollect.instance.Prop1Conut < 1 || PlayerCollect.instance.PnowHealth >= PlayerCollect.instance.PmaxHealth)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        PlayerCollect.instance.ChangeHealth(prop1AddHealth);
        PlayerCollect.instance.SetProp(1, -1);
    }

    //用个人信息
    public void UseIF()
    {
        if (!CenterCtrl.instance.isHCM || PlayerCollect.instance.Prop2Conut <= 0)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        HlinControl.instance.SetSpeed();
        PlayerCollect.instance.SetProp(2, -1);
    }

    //用秋键
    public void UseDJR()
    {
        if (PlayerCollect.instance.Prop3Conut <= 0)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.DjrSay();
        SoundHelper.OK();
        PlayerCollect.instance.AddSpeed();
        PlayerCollect.instance.SetProp(3, -1);
    }
    //用表情
    public void UseEmoji()
    {
        if (PlayerCollect.instance.Prop4Conut <= 0)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        PlayerCollect.instance.OpenGun();
        PlayerCollect.instance.SetProp(4, -1);
    }

    //大厕头水物品
    public void ItemUseBigWCWater()
    {
        if (PlayerCollect.instance.Item1Conut > 0)
        {
            Instantiate(chair, PlayerCollect.instance.gameObject.transform.position, Quaternion.identity);
            SoundHelper.OK();
            PlayerCollect.instance.SetProp(21, -1);
        }
        else
        {
            SoundHelper.Beep();
            return;
        }
    }

    private void Update()
    {
        if (bigWCWater != null)
        {
            if (PlayerCollect.instance.Item1Conut > 0)
            {
                bigWCWater.interactable = true;
            }
            else
            {
                bigWCWater.interactable = false;
            }
        }
    }
}
