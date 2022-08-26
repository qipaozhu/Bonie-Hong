using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CenterCtrl : MonoBehaviour
{
    public static CenterCtrl instance { get; private set; }
    public static bool isHFindPlayer;
    //====��Ϸʱ��====
    [SerializeField]
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

    [Header("��Ϸ��ʾ")]
    //С֥ʿ(��Ϸ��ʾ��
    public Image tishiIcon;
    public Text tishiText;

    [Header("��ʾ��ͼ��")]
    public Sprite endtishiIcon;
    public Sprite deffIcon;
    public Sprite wcIcon;


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

    

    [Header("�����ɵ�")]
    public GameObject[] treeSpawn;

    //====��������ȴ====
    [Header("��������ȴ")]
    [Min(0)]
    public float maxTeleCold;
    float c_teleCold;
    bool c_isTeleColdDone = true;
    public bool isTeleDone { get { return c_isTeleColdDone; } }
    
    //���¿�ʼ��ʱ
    void ResetHL() 
    {
        if (EnderSky.instance.WillOver) return;
        tishiIcon.sprite = deffIcon;
        tishiText.text = "����ײ�����ϵ�������ɫԲ�Σ�";
        //====��ʱ����====
        nextHBonie = Random.Range(60, maxTimeToCM);
        fullHBonie = nextHBonie;
        IsHCM = false;
        hlText.text = "�ȴ�����";
    }

    void Update()
    {
        playTime += Time.deltaTime; //��Ϸʱ���

        //���õ�ʱ��
#if !UNITY_EDITOR
        if(playTime > 180 && !AllSceneSetting.instance.isRealPlayer)
        {
            Toast.instance.InfoBox("���ý�����");
            MenuControl.instance.BackMenu();
        }
#endif
        if (!c_isTeleColdDone) //���������û����ȴ��
        {
            c_teleCold -= Time.deltaTime;
            if (c_teleCold <= 0) c_isTeleColdDone = true; //С��0����true
        }

        if (nextHBonie > 0 && !EnderSky.instance.WillOver && isHlLife) // �����һ�γ�û����0 �� ��û���� �� ���ڣ�ʱ���
        {
            hlBar.fillAmount = nextHBonie / fullHBonie; //boss��
            hlText.text = Mathf.Floor(nextHBonie).ToString();
            nextHBonie = nextHBonie - Time.deltaTime;
        }

        //����<=0ʱ
        if (TreeCount <= 0) EnderSky.instance.TreeOver();
        if (EnderSky.instance.WillOver)
        {
            IsHCM = true;
            hlText.text = "������ڣ�";
        }

        //������ʾ
        float fillamount = (float)TreeCount / (float)maxTreeCount;
        treeFullBar.fillAmount = 1 - fillamount;
        
        if ( nextHBonie <= 0 && !isHCM)// �����һ�γ�ûС��0��HCM����
            HCMfuncion();
    }

    void Start()
    {
#if !UNITY_EDITOR
        if(PlayerPrefs.GetInt("IsPlayedAgo") == 1 && !AllSceneSetting.instance.isRealPlayer)
        {
            Toast.instance.InfoBox("���ý�����");
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
    //====������ʾ������====
    public void TiShiEnd()
    {
        tishiIcon.sprite = endtishiIcon;
        tishiText.text = "�ҵ�����̫��ͼ�����룬��������û\n����ɫͼ�꣩";
    }
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

    //====��������====
    public void SetTree(int i)
    {
        TreeCount += i;
    }
    //====Debug������û====
    public void Bonie()
    {
        if(!IsHCM) nextHBonie = 5;
    }
    //====�����Ѿ���������====
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
        tishiIcon.sprite = wcIcon;
        tishiText.text = "�ҵ�����/ͣ�����ɫ�����Σ�������(E)���ȥ";
        //===========
        Invoke("ResetHL", Random.Range(60,240));
        hlText.text = "��û!����";
    }
}
