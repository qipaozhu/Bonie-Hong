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
    //====BOSS战====
    bool inBossWar = false;
    public bool isBossWar { get => inBossWar; set => inBossWar = value; }


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
        //====记时重置====
        nextHBonie = Random.Range(60, maxTimeToCM);
        fullHBonie = nextHBonie;
        IsHCM = false;
        hlText.text = "等待复活";
    }

    void Update()
    {
        playTime += Time.deltaTime; //游戏时间加


        if (!c_isTeleColdDone) //如果传送门没有冷却好
        {
            c_teleCold -= Time.deltaTime;
            if (c_teleCold <= 0) c_isTeleColdDone = true; //小于0设置true
        }

        if (nextHBonie > 0 && isHlLife && !inBossWar) 
            // 如果下一次出没大于0 和 不是boss战 和 泓在，时间减
        {
            hlBar.fillAmount = nextHBonie / fullHBonie; //boss条
            hlText.text = Mathf.Floor(nextHBonie).ToString();
            nextHBonie = nextHBonie - Time.deltaTime;
        }

        //当树<=0时
        if (TreeCount <= 0)
        {
            EnderSky.instance.TreeOver();
            IsHCM = false;
        }
        //树的显示
        float fillamount = (float)TreeCount / (float)maxTreeCount;
        treeFullBar.fillAmount = 1 - fillamount;
        
        if ( nextHBonie <= 0 && !isHCM)// 如果下一次出没小于0，HCM函数
            HCMfuncion();
    }

    void Start()
    {
        maxTreeCount = AllSceneSetting.instance.TreeCount;//最大树数目

        instance = this;
        ResetHL();
        TreeCount = maxTreeCount;
        c_teleCold = maxTeleCold;
        c_isTeleColdDone = true;
        SpawnTree(AllSceneSetting.instance.TreeCount);
    }

    //开始时生成树代码（开始时执行一次）
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

    //摧毁生成点表示（在暂停菜单）
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

    //====调整树数量====
    public void SetTree(int i)
    {
        TreeCount += i;
    }

    //====已经传送完了并进入冷却====
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
        //===========
        Invoke("ResetHL", Random.Range(60,240));
        hlText.text = "出没!隐蔽";
    }
}
