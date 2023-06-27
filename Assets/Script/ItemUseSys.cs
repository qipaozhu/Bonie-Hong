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

    [Header("��Ϸ����")]
    public GameObject chair;
    public GameObject computer;
    public GameObject GunFire;
    public GameObject ball;
    [Header("������")]
    public int prop1AddHealth;
    public Button[] propMenu;
    [Header("��Ʒ��")]
    public Animator itemBar;
    public Button bigWCWater;
    public Button recycleBin;

    #region InputSystem�Ŀ�ݼ�
    //InputSystem��ݼ���Ӧ
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
    //������
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

    //�ø�����Ϣ
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

    //�����
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
    //�ñ���
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

    //���ͷˮ��Ʒ
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

    //����վ��Ʒ
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
        //����ǿգ��ڵ������ϣ����ͷ���
        if(bigWCWater == null) { yield break; }

        //����1
        while (true)
        {
            yield return null;
            #region ��ƷUI����
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

            #region ����UI����
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
