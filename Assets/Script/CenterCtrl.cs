using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterCtrl : MonoBehaviour
{
    public static CenterCtrl instance { get; private set; }
    public static bool isHFindPlayer;
    //====游戏时间====
    float playTime = 0;
    public float GamePlayTime { get { return playTime; } }
    public float GamePlayTimeS { get => playTime; set => playTime = value; }
    //====出没时间====
    public int maxTimeToCM;
    float nextHBonie;
    float fullHBonie;
    
    //====条====
    //下一次出没条
    public Image hlBar;
    public Text hlText;
    public bool ooooooooooooooooooooooooooo;
    public Image treeFullBar; //树条

    public GameObject notice; //弹出提示
    public Text noticeText; //弹出提示
    public bool ooooooooooooooooooooooo;
    //小芝士(游戏提示）
    public Image tishiIcon;
    public Text tishiText;

    public Sprite endtishiIcon;
    public Sprite deffIcon;

    public GameObject miniMap;
    public bool oooooooooo0oooooooooooooo;
    //====是否出没====
    bool IsHCM = false;
    public bool isHCM { get { return IsHCM; } }
    //====树木====
    public int lastTreeConut { get { return TreeCount; } }
    int TreeCount;
    int maxTreeCount;
    public bool oooooooooooooooooooooooooo;
    //====道具====
    public Text prop1;
    public Text prop2;
    //====技能条====
    public GameObject propBar;
    public GameObject[] treeSpawn;

    public bool oooooooooooooooooooooooo;
    //====传送门冷却====
    public float maxTeleCold;
    float c_teleCold;
    bool c_isTeleColdDone = true;
    public bool isTeleDone { get { return c_isTeleColdDone; } }
    
    //重新开始记时
    void ResetHL() 
    {
        if (EnderSky.instance.WillOver) return;
        tishiIcon.sprite = deffIcon;
        tishiText.text = "砍（撞）场上的树（绿色圆形）";
        //====记时重置====
        nextHBonie = Random.Range(60, maxTimeToCM);
        fullHBonie = nextHBonie;
        IsHCM = false;
    }

    void Update()
    {
        playTime += Time.deltaTime; //游戏时间加
        if (!c_isTeleColdDone) //如果传送门没有冷却好
        {
            c_teleCold -= Time.deltaTime;
            if (c_teleCold <= 0) c_isTeleColdDone = true; //小于0设置true
        }

        if (nextHBonie > 0 && !EnderSky.instance.WillOver) // 如果下一次出没大于0且树没砍完，时间
        {
            hlBar.fillAmount = nextHBonie / fullHBonie; //boss条
            hlText.text = Mathf.Floor(nextHBonie).ToString();
            nextHBonie = nextHBonie - Time.deltaTime;
        }

        //当树<=0时
        if (TreeCount <= 0) EnderSky.instance.TreeOver();
        if (EnderSky.instance.WillOver)
        {
            IsHCM = true;
            hlText.text = "逃向出口！";
        }

        //树的显示
        float fillamount = (float)TreeCount / (float)maxTreeCount;
        treeFullBar.fillAmount = 1 - fillamount;

        if (Input.GetButtonDown("Cancel"))  // 按下绑定为Cancel的按键时,暂停
        {
            MenuControl.instance.Pause();
        }
        if (Input.GetButtonDown("Prop")) //技能栏
        {
            SoundHelper.Click();

            if (propBar.GetComponent<Animator>().GetBool("isShow")) propBar.GetComponent<Animator>().SetBool("isShow", false);
            else propBar.GetComponent<Animator>().SetBool("isShow", true);

            //if (propBar.activeSelf) propBar.SetActive(false);
            //else propBar.SetActive(true);
        }
        if (Input.GetButtonDown("Minimap")) //地图显示
        {
            SoundHelper.Click();
            if (miniMap.activeSelf) miniMap.SetActive(false);
            else miniMap.SetActive(true);
        }
        

        if ( nextHBonie <= 0 && !isHCM)// 如果下一次出没小于0，HCM函数
            HCMfuncion();
        //====设置道具一数量文字====
        prop1.text = PlayerCollect.instance.Prop1Conut.ToString();
        prop2.text = PlayerCollect.instance.Prop2Conut.ToString();
    }

    void Start()
    {
        maxTreeCount = AllSceneSetting.instance.TreeCount;
        instance = this;
        ResetHL();
        TreeCount = maxTreeCount;
        c_teleCold = maxTeleCold;
        c_isTeleColdDone = true;
        SpawnTree(AllSceneSetting.instance.TreeCount);
    }

    void TiShi(Sprite icon,string text)
    {
        tishiIcon.sprite = icon;
        tishiText.text = text;
    }
    //====提示结束====
    public void TiShiEnd()
    {
        tishiIcon.sprite = endtishiIcon;
        tishiText.text = "找到大大的太空图并进入，逃脱泓出没\n（紫色图标）";
    }
    void SpawnTree(int many)
    {
        int i = 0;
        while (i < many)
        {
            treeSpawn[Random.Range(0, treeSpawn.Length)].GetComponent<TreeSpawn>().Spawn();
            i++;
            Debug.Log("生成第课树：" + i);
        }
    }

    public void DelSpawn()
    {
        int i = 0;
        while (true) 
        {
            Destroy(treeSpawn[i]);
            i++;
            if (i >= treeSpawn.Length) break;
        }
        SoundHelper.OK();
     }

    //====调整数量====
    public void SetTree(int i)
    {
        TreeCount += i;
    }
    //====Debug立即出没====
    public void Bonie()
    {
        if(!IsHCM) nextHBonie = 5;
    }
    //====设置已经传送完了====
    public void TeleDone()
    {
        c_isTeleColdDone = false;
        c_teleCold = maxTeleCold;
    }

    //========出没功能========
    void HCMfuncion()
    {
        IsHCM = true;
        //====功能====
        SoundHelper.hCome();
        tishiIcon.sprite = deffIcon;
        tishiText.text = "找到厕所/停车棚（红色正方形）并“E”躲进去";
        //===========
        Invoke("ResetHL", Random.Range(30,100));
        hlText.text = "出没!隐蔽";
    }

    //提示功能
    public void HaveNotice(string whatToNotice)
    {
        Debug.Log("提示：" + whatToNotice);
        notice.SetActive(true);
        noticeText.text = whatToNotice;
        SoundHelper.Beep();
        Invoke("EndNotice", 5);
    }
    void EndNotice() { notice.SetActive(false); }
}
