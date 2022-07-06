using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropsAndItems : MonoBehaviour
{
    [Header("游戏对象")]
    public GameObject chair;
    public GameObject computer;
    [Header("道具栏")]
    public int prop1AddHealth;
    [Header("物品栏")]
    public Button bigWCWater;
    public Button recycleBin;
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
        PlayerCollect.instance.Prop1Conut--;
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
        PlayerCollect.instance.Prop2Conut--;
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
        PlayerCollect.instance.Prop3Conut--;
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
        PlayerCollect.instance.Prop4Conut--;
    }

    //大厕头水物品
    public void ItemUseBigWCWater()
    {
        if (PlayerCollect.instance.Item1Conut > 0)
        {
            Instantiate(chair, PlayerCollect.instance.gameObject.transform.position, Quaternion.identity);
            SoundHelper.OK();
            PlayerCollect.instance.Item1Conut--;
        }
        else
        {
            SoundHelper.Beep();
            return;
        }
    }

    //回收站物品
    public void ItemUseReCycleBin()
    {
        if (PlayerCollect.instance.Item2Conut > 0)
        {
            Instantiate(computer, PlayerCollect.instance.gameObject.transform.position, Quaternion.identity);
            SoundHelper.OK();
            PlayerCollect.instance.Item2Conut --;
        }
        else
        {
            SoundHelper.Beep();
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(SetGUI());
    }

    private IEnumerator SetGUI()
    {
        //如果是空（在道具栏上），就返回
        if(bigWCWater == null) { yield break; }

        //道具1
        while (true)
        {
            yield return null;
            if (PlayerCollect.instance.Item1Conut > 0)
            {
                bigWCWater.interactable = true;
            }
            else
            {
                bigWCWater.interactable = false;
            }
            //道具2
            if (PlayerCollect.instance.Item2Conut > 0)
            {
                recycleBin.interactable = true;
            }
            else
            {
                recycleBin.interactable = false;
            }
        }
    }
}
