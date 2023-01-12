using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager main { get; private set; }
    //====技能条====
    [Header("道具、物品栏")]
    public GameObject propBar;
    public GameObject itemBar;

    //====道具数量====
    [Header("道具数量显示")]
    public Text prop1;
    public Text prop2;
    public Text prop3;
    public Text prop4;

    [Header("迷你地图")]
    public GameObject miniMap;

    [Header("信息卡")]
    public GameObject infocard;
    public Slider healthBar;
    public Text title;
    public Text description;

    [Header("面板")]
    public Animator hurtPanel;
    public GameObject videoPlayerPanel;

    [Header("Boss战设置")]
    public GameObject bonieBar;
    public GameObject hlHealthBarParent;
    public Image hlHealth;

    private void Start()
    {
        main = this;
    }


    public void OnProp() //技能栏
    {
        SoundHelper.Click();
        if (propBar.GetComponent<Animator>().GetBool("isShow")) propBar.GetComponent<Animator>().SetBool("isShow", false);
        else propBar.GetComponent<Animator>().SetBool("isShow", true);
    }
    public void OnItem() //物品栏
    {
        SoundHelper.Click();
        if (itemBar.GetComponent<Animator>().GetBool("isShow")) itemBar.GetComponent<Animator>().SetBool("isShow", false);
        else itemBar.GetComponent<Animator>().SetBool("isShow", true);
    }
    public void OnMap() //地图显示
    {
        SoundHelper.Click();
        if (miniMap.GetComponent<Animator>().GetBool("isShow")) miniMap.GetComponent<Animator>().SetBool("isShow", false);
        else miniMap.GetComponent<Animator>().SetBool("isShow", true);
    }

    public void ShowWarnPanel()//警告时的ui特效
    {
        hurtPanel.SetTrigger("isDamage");
    }

    #region 泓大是否看见玩家躲进厕所摧毁部分
    public void SetHenrySaw()
    {
        hurtPanel.SetBool("isHenrySaw", true);
    }//泓大是否看见玩家
    public void HenrySawOut()
    {
        hurtPanel.SetBool("isHenrySaw", false);
    }
    #endregion

    public void PlaySkillAnimation()
    {
        videoPlayerPanel.SetActive(true);
        videoPlayerPanel.GetComponent<VideoPlayer>().Play();
        Invoke("ResetSkillAni", 1.7f);
    }//播放动画部分
    void ResetSkillAni()
    {
        videoPlayerPanel.SetActive(false);
        videoPlayerPanel.GetComponent<VideoPlayer>().Stop();
    }

    public void SetBossHealthBar()
    {
        bonieBar.SetActive(false);
        hlHealthBarParent.SetActive(true);
    }
    public void SetBossHealthValue(float v)
    {
        hlHealth.fillAmount = v;
    }

    void Update()
    {
        //====设置道具一数量文字====
        prop1.text = PlayerCollect.instance.Prop1Conut.ToString();
        prop2.text = PlayerCollect.instance.Prop2Conut.ToString();
        prop3.text = PlayerCollect.instance.Prop3Conut.ToString();
        prop4.text = PlayerCollect.instance.Prop4Conut.ToString();
        InfoCard();
    }

    void InfoCard()
    {
        RaycastHit2D[] hitlist = Physics2D.RaycastAll
            (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.up,.5f);
        if (hitlist.Length <= 0) { infocard.SetActive(false); return; }
        for (int i = 0; i < hitlist.Length; i++)
        {
            RaycastHit2D hit = hitlist[i];
         
            if (hit.collider != null)
            {
                InfoCard text = hit.collider.GetComponent<InfoCard>();
                if (text != null)
                {
                    Debug.Log(infocard.GetComponent<RectTransform>().position);
                    infocard.SetActive(true);
                    infocard.GetComponent<RectTransform>().position = Pointer.current.position.ReadValue();
                    title.text = text.objectName;
                    description.text = text.description;
                }
                else
                {
                    infocard.SetActive(false);
                }
            }
            
        }
    }
}
