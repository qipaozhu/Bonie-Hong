using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager main { get; private set; }
    //====������====
    [Header("���ߡ���Ʒ��")]
    public GameObject propBar;
    public GameObject itemBar;

    //====��������====
    [Header("����������ʾ")]
    public Text prop1;
    public Text prop2;
    public Text prop3;
    public Text prop4;

    [Header("�����ͼ")]
    public GameObject miniMap;

    private void Start()
    {
        main = this;
    }


    public void OnProp() //������
    {
        SoundHelper.Click();
        if (propBar.GetComponent<Animator>().GetBool("isShow")) propBar.GetComponent<Animator>().SetBool("isShow", false);
        else propBar.GetComponent<Animator>().SetBool("isShow", true);
    }
    public void OnItem() //��Ʒ��
    {
        SoundHelper.Click();
        if (itemBar.GetComponent<Animator>().GetBool("isShow")) itemBar.GetComponent<Animator>().SetBool("isShow", false);
        else itemBar.GetComponent<Animator>().SetBool("isShow", true);
    }
    public void OnMap() //��ͼ��ʾ
    {
        SoundHelper.Click();
        if (miniMap.GetComponent<Animator>().GetBool("isShow")) miniMap.GetComponent<Animator>().SetBool("isShow", false);
        else miniMap.GetComponent<Animator>().SetBool("isShow", true);
    }

    void Update()
    {
        //====���õ���һ��������====
        prop1.text = PlayerCollect.instance.Prop1Conut.ToString();
        prop2.text = PlayerCollect.instance.Prop2Conut.ToString();
        prop3.text = PlayerCollect.instance.Prop3Conut.ToString();
        prop4.text = PlayerCollect.instance.Prop4Conut.ToString();
    }
}