using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseSys : MonoBehaviour
{
    public static ItemUseSys instance;

    public float maxPropCold;
    float usePropCold;
    bool isReadyToUseProp = true;

    [Header("游戏对象")]
    public GameObject chair;
    public GameObject computer;
    public GameObject GunFire;
    public GameObject ball;
    [Header("道具栏")]
    public int prop1AddHealth;
    public Button[] propMenu;
    [Header("物品栏")]
    public Animator itemBar;
    public Button bigWCWater;
    public Button recycleBin;

    #region InputSystem的快捷键
    //InputSystem快捷键对应
    void OnProp1()
    {
        UseDP();
    }
    void OnProp2()
    {
        UseIF();
    }
    void OnProp3()
    {
        UseDJR();
    }
    void OnProp4()
    {
        UseEmoji();
    }
    #endregion
    //用遗照
    public void UseDP()
    {
        if (PlayerCollect.instance.Prop1Conut < 1 || !isReadyToUseProp)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        if (PlayerCollect.instance.PnowHealth < PlayerCollect.instance.PmaxHealth) 
        {
            PlayerCollect.instance.ChangeHealth(prop1AddHealth);
            if (CenterCtrl.instance.isBossWar)
            {
                PlayerCollect.instance.ChangeHealth(prop1AddHealth);
            }
        }

        if (CenterCtrl.instance.isBossWar)
        {
            Instantiate(ball, PlayerCollect.instance.transform.position, Quaternion.identity);
            SoundHelper.Fire();
        }
        PlayerCollect.instance.Prop1Conut--;
        isReadyToUseProp = false;
    }

    //用个人信息
    public void UseIF()
    {
        if (PlayerCollect.instance.Prop2Conut <= 0 || !isReadyToUseProp)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        HlinControl.instance.SetSpeed();
        PlayerCollect.instance.Prop2Conut--;
        isReadyToUseProp= false;
    }

    //用秋键
    public void UseDJR()
    {
        if (PlayerCollect.instance.Prop3Conut <= 0 || !isReadyToUseProp )
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.DjrSay();
        SoundHelper.OK();
        PlayerCollect.instance.AddSpeed();
        PlayerCollect.instance.Prop3Conut--;
        isReadyToUseProp= false;
    }
    //用表情
    public void UseEmoji()
    {
        if (PlayerCollect.instance.Prop4Conut <= 0 || !isReadyToUseProp)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        PlayerCollect.instance.OpenGun();

        if(CenterCtrl.instance.isBossWar)
        {
            Instantiate(GunFire, PlayerCollect.instance.transform.position, Quaternion.identity);
        }
        PlayerCollect.instance.Prop4Conut--;
        isReadyToUseProp= false;
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
        }
        itemBar.SetBool("isShow", false);
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
        }
        itemBar.SetBool("isShow", false);
    }

    private void Start()
    {
        StartCoroutine(SetGUI());
        instance = this;
        usePropCold = maxPropCold;
    }
    private void Update()
    {
        if (!isReadyToUseProp)
        {
            if (usePropCold <= 0) { isReadyToUseProp = true; usePropCold = maxPropCold; }
            usePropCold -= Time.deltaTime;
        }
    }

    private IEnumerator SetGUI()
    {
        //如果是空（在道具栏上），就返回
        if(bigWCWater == null) { yield break; }

        //道具1
        while (true)
        {
            yield return null;
            #region 物品UI设置
            if (PlayerCollect.instance.Item1Conut > 0)
            {
                bigWCWater.interactable = true;
            }
            else
            {
                bigWCWater.interactable = false;
            }

            if (PlayerCollect.instance.Item2Conut > 0)
            {
                recycleBin.interactable = true;
            }
            else
            {
                recycleBin.interactable = false;
            }
            #endregion

            #region 道具UI设置
            if (isReadyToUseProp)
            {
                for (int i = 0; i < propMenu.Length; i++)
                {
                    propMenu[i].interactable = true;
                }
            }
            else
            {
                for (int i = 0; i < propMenu.Length; i++)
                {
                    propMenu[i].interactable = false;
                }
            }
            #endregion
        }
    }
    
}
