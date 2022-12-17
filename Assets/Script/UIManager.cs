using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    void Update()
    {
        //====设置道具一数量文字====
        prop1.text = PlayerCollect.instance.Prop1Conut.ToString();
        prop2.text = PlayerCollect.instance.Prop2Conut.ToString();
        prop3.text = PlayerCollect.instance.Prop3Conut.ToString();
        prop4.text = PlayerCollect.instance.Prop4Conut.ToString();
    }
    IEnumerator InfoCard()
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast
                (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, .5f);
            if(hit.collider != null)
            {
                InfoCard text = hit.collider.GetComponent<InfoCard>();
                if(text != null)
                {
                    infocard.SetActive(true);
                    infocard.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector2(2,0);
                    title.text = text.objectName;
                    description.text = text.description;
                }
                else
                {
                    infocard.SetActive(false);
                }
            }
            yield return null;
        }
    }
}
