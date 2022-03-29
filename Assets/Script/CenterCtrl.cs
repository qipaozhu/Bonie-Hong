using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterCtrl : MonoBehaviour
{
    public static CenterCtrl instance { get; private set; }
    public static bool isHFindPlayer;

    //====出没时间====
    public int maxTimeToCM;
    float nextHBonie;
    float fullHBonie;
    public GameObject endNotice;
    //====条====
    public Image hlBar;
    public Text hlText;
    public Image treeFullBar;

    //====是否出没====
    bool IsHCM = false;
    public bool isHCM { get { return IsHCM; } }
    //====树木====
    public int lastTreeConut { get { return TreeCount; } }
    int TreeCount;
    public int maxTreeCount;

    //====道具====
    public Text prop1;

    //====技能条====
    public GameObject propBar;
    
    //重新开始记时
    void ResetHL() 
    {
        //====记时重置====
        nextHBonie = Random.Range(30, maxTimeToCM);
        fullHBonie = nextHBonie;
        IsHCM = false;
    }

    void Update()
    {
        if (nextHBonie > 0) // 如果下一次出没大于0，时间
        {
            hlBar.fillAmount = nextHBonie / fullHBonie; //boss条
            hlText.text = Mathf.Floor(nextHBonie).ToString();
            nextHBonie = nextHBonie - Time.deltaTime;
        }
        //当树=0时
        if(TreeCount <= 0)
        {
            endNotice.SetActive(true);
            Time.timeScale = 0;
        }
        //树的显示
        float fillamount = (float)TreeCount / (float)maxTreeCount;
        treeFullBar.fillAmount = 1 - fillamount;

        if (Input.GetButtonDown("Cancel")) MenuControl.instance.Pause(); // 按下绑定为Cancel的按键时,暂停
        if (Input.GetButtonDown("Prop")) //技能栏
        {
            if (propBar.activeSelf) propBar.SetActive(false);
            else propBar.SetActive(true);
        }

        if ( nextHBonie <= 0 && !isHCM)// 如果下一次出没小于0，HCM函数
            HCMfuncion();
        prop1.text =PlayerCollect.instance.Prop1Conut.ToString(); // 设置道具一数量文字
    }

    void Start()
    {
        instance = this;
        ResetHL();
        TreeCount = maxTreeCount;
    }


    //====调整数量====
    public void SetTree(int i)
    {
        TreeCount = TreeCount + i;
    }


    //========出没功能========
    void HCMfuncion()
    {
        IsHCM = true;
        //====功能====
        SoundHelper.hCome();
        //===========
        Invoke("ResetHL", 40f);
        hlText.text = "出没!隐蔽";
    }
}
