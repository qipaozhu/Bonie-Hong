using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CenterCtrl : MonoBehaviour
{
    public static CenterCtrl instance { get; private set; }
    //====��Ϸʱ��====
    float playTime = 0;
    public float GamePlayTime { get { return playTime; } }
    public float GamePlayTimeS { get => playTime; set => playTime = value; }
    //====��ûʱ��====
    [Header("��һ�γ�û")]
    public int maxTimeToCM;
    float nextHBonie;
    float fullHBonie;

    //====��====
    [Header("��һ�γ�û��")]
    public Image hlBar;
    public Text hlText;
    [Header("������")]
    public Image treeFullBar; //����

    //====�Ƿ��û====
    bool IsHCM = false;
    public bool isHCM { get { return IsHCM; } }
    //====�����Ƿ���====
    bool isHlLife;
    public bool isHllife { get => isHlLife; set => isHlLife = value; }
    //====��ľ====
    public int lastTreeConut { get { return TreeCount; } }
    int TreeCount;
    int maxTreeCount;
    //====BOSSս====
    bool inBossWar = false;
    public bool isBossWar { get => inBossWar; set => inBossWar = value; }
    //====��������====
    bool isPropMax;
    public bool isPropTooMuch { get => isPropMax; }

    [Header("�����ɵ�")]
    public GameObject[] treeSpawn;

    //====��������ȴ====
    [Header("��������ȴ")]
    [Min(0)]
    public float maxTeleCold;
    float c_teleCold;
    bool c_isTeleColdDone = true;
    public bool isTeleDone { get { return c_isTeleColdDone; } }

    bool isRanBossFun = false;
    void Update()
    {
        playTime += Time.deltaTime; //��Ϸʱ���


        if (!c_isTeleColdDone) //���������û����ȴ��
        {
            c_teleCold -= Time.deltaTime;
            if (c_teleCold <= 0) c_isTeleColdDone = true; //С��0����true
        }

        if (nextHBonie > 0 && isHlLife && !inBossWar)
        // �����һ�γ�û����0 �� ����bossս �� ���ڣ�ʱ���
        {
            if (TreeCount <= 0)
            {
                hlText.text = "Bossս";
                return;
            }
            hlBar.fillAmount = nextHBonie / fullHBonie; //boss��
            hlText.text = Mathf.Floor(nextHBonie).ToString();
            nextHBonie = nextHBonie - Time.deltaTime;
        }

        //����<=0ʱ
        if (TreeCount <= 0)
        {
            ResetHL();
            hlText.text = "Bossս";
            EnderSky.instance.TreeOver();//û�£�end���������Ŀ
            IsHCM = false;
        }
        //������ʾ
        float fillamount = (float)TreeCount / (float)maxTreeCount;
        treeFullBar.fillAmount = 1 - fillamount;

        if (nextHBonie <= 0 && nextHBonie > -1000 && !isHCM)// �����һ�γ�ûС��0��HCM����
        {
            HCMfuncion();
            nextHBonie = -1999;
        }

        //�������bossս�Ļ�
        if (isBossWar & !isRanBossFun)
        {
            ResetHL();
            isRanBossFun = true;
            LHspawn.instance.SpawnHenryInBosswar();//��ֹû������׷ɱ,ͬʱ��ʼ��
            HlinControl.instance.StartToBossWar();
            PlayerCollect.instance.StartToBossWar();
            UIManager.main.SetBossHealthBar();

            PlayerCollect.instance.Prop1Conut += 2;
            PlayerCollect.instance.Prop2Conut += 2;
            PlayerCollect.instance.Prop3Conut += 2;
            PlayerCollect.instance.Prop4Conut += 2;

            PropSpawnPoint[] point = FindObjectsOfType<PropSpawnPoint>();
            for (int i = 0; i < point.Length; i++)
            {
                point[i].SetBosswarSpawn();
            }

            GameObject[] item = GameObject.FindGameObjectsWithTag("PropItem");
            for (int i = 0; i < item.Length; i++)
            {
                Destroy(item[i]);
            }

            GameObject[] hfirfind = GameObject.FindGameObjectsWithTag("HFirstFind");
            for (int i = 0; i < hfirfind.Length; i++)
            {
                Destroy(hfirfind[i]);
            }
        }

        if (GameObject.FindGameObjectsWithTag("PropItem").Length >= 20)
        {
            isPropMax = true;
        }
        else { isPropMax = false; }
    }

    void Start()
    {
        maxTreeCount = AllSceneSetting.instance.TreeCount;//�������Ŀ

        instance = this;
        ResetHL();
        TreeCount = maxTreeCount;
        c_teleCold = maxTeleCold;
        c_isTeleColdDone = true;
        SpawnTree(AllSceneSetting.instance.TreeCount);
    }

    //��ʼʱ���������루��ʼʱִ��һ�Σ�
    void SpawnTree(int many)
    {
        int i = 0;
        while (i < many)
        {
            treeSpawn[Random.Range(0, treeSpawn.Length)].GetComponent<TreeSpawn>().Spawn();
            i++;
            Debug.Log("���ɵڿ�����" + i);
        }
    }

    void OnESC()
    {
        if (Time.timeScale != 0)
        { MenuControl.instance.Pause(); }
        else { MenuControl.instance.NotPause(); }
    }

    //�ݻ����ɵ��ʾ������ͣ�˵���
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

    //====����������====
    public void SetTree(int i)
    {
        TreeCount += i;
    }

    //====�Ѿ��������˲�������ȴ====
    public void TeleDone()
    {
        c_isTeleColdDone = false;
        c_teleCold = maxTeleCold;
    }

    //========��û����========
    void HCMfuncion()
    {
        IsHCM = true;
        //====����====
        SoundHelper.hCome();
        //===========
        StartCoroutine(HlHiderTest());
        StartCoroutine(ResetHLIenu());
        hlText.text = "��û!����";
    }

    IEnumerator HlHiderTest()
    {
        yield return new WaitForSeconds(Random.Range(40, 70));
        if (PlayDisable.instance.playIsDisable)
        {
            ResetHL();
        }
        else
        {
            yield break;
        }
    }//�����һֱ���ʱ�򾡿�����û
    IEnumerator ResetHLIenu()
    {
        yield return new WaitForSeconds(AllSceneSetting.instance.hlBonieTime);
        ResetHL();
    }//��һֱ��������Ȼ���ûʱ�����ʱ����
    
    void ResetHL()
    {
        StopCoroutine(ResetHLIenu());
        StopCoroutine(HlHiderTest());
        //====��ʱ����====
        nextHBonie = Random.Range(90, maxTimeToCM);
        fullHBonie = nextHBonie;
        IsHCM = false;
        hlText.text = "���ڸ���";
    }//���¿�ʼ��ʱ

}
