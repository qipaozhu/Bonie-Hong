using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CenterCtrl : MonoBehaviour
{
    public static CenterCtrl instance { get; private set; }
    public static bool isHFindPlayer;
    //====游戏时间====
    [SerializeField]
    float playTime = 0;
    public float GamePlayTime { get { return playTime; } }
    public float GamePlayTimeS { get => playTime; set => playTime = value; }
    //====出没时间====
    [Header("下一次出没")]
    public int maxTimeToCM;
    float nextHBonie;
    float fullHBonie;

    //====条====
    [Header("下一次出没条")]
    public Image hlBar;
    public Text hlText;
    [Header("树的条")]
    public Image treeFullBar; //树条

    [Header("游戏提示")]
    //小芝士(游戏提示）
    public Image tishiIcon;
    public Text tishiText;

    [Header("提示的图标")]
    public Sprite endtishiIcon;
    public Sprite deffIcon;
    public Sprite wcIcon;


    //====是否出没====
    bool IsHCM = false;
    public bool isHCM { get { return IsHCM; } }
    //====林泓是否在====
    bool isHlLife;
    public bool isHllife { get => isHlLife; set => isHlLife = value; }
    //====树木====
    public int lastTreeConut { get { return TreeCount; } }
    int TreeCount;
    int maxTreeCount;

    

    [Header("树生成点")]
    public GameObject[] treeSpawn;

    //====传送门冷却====
    [Header("传送门冷却")]
    [Min(0)]
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
        hlText.text = "等待复活";
    }

    void Update()
    {
        playTime += Time.deltaTime; //游戏时间加

        //试用的时间
#if !UNITY_EDITOR
        if(playTime > 180 && !AllSceneSetting.instance.isRealPlayer)
        {
            Toast.instance.InfoBox("试用结束！");
            MenuControl.instance.BackMenu();
        }
#endif
        if (!c_isTeleColdDone) //如果传送门没有冷却好
        {
            c_teleCold -= Time.deltaTime;
            if (c_teleCold <= 0) c_isTeleColdDone = true; //小于0设置true
        }

        if (nextHBonie > 0 && !EnderSky.instance.WillOver && isHlLife) // 如果下一次出没大于0 和 树没砍完 和 泓在，时间减
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
        
        if ( nextHBonie <= 0 && !isHCM)// 如果下一次出没小于0，HCM函数
            HCMfuncion();
    }

    void Start()
    {
#if !UNITY_EDITOR
        if(PlayerPrefs.GetInt("IsPlayedAgo") == 1 && !AllSceneSetting.instance.isRealPlayer)
        {
            Toast.instance.InfoBox("试用结束！");
            MenuControl.instance.BackMenu();
            return;
        }
        if (!AllSceneSetting.instance.isRealPlayer)
        {
            PlayerPrefs.SetInt("IsPlayedAgo", 1);
        }
        else
        {
            PlayerPrefs.DeleteKey("IsPlayedAgo");
        }
#endif
        maxTreeCount = AllSceneSetting.instance.TreeCount;
        instance = this;
        ResetHL();
        TreeCount = maxTreeCount;
        c_teleCold = maxTeleCold;
        c_isTeleColdDone = true;
        SpawnTree(AllSceneSetting.instance.TreeCount);
    }

    public void TiShi(Sprite icon,string text)
    {
        tishiIcon.sprite = icon;
        tishiText.text = text;
    }
    //====游玩提示：逃脱====
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

    void OnESC()
    {
        if (Time.timeScale != 0)
        { MenuControl.instance.Pause(); }
        else { MenuControl.instance.NotPause(); }
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
        tishiIcon.sprite = wcIcon;
        tishiText.text = "找到厕所/停车棚（红色正方形）并交互(E)躲进去";
        //===========
        Invoke("ResetHL", Random.Range(60,240));
        hlText.text = "出没!隐蔽";
    }
}
