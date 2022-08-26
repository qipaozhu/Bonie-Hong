using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinSkill : MonoBehaviour
{
    public static HlinSkill main { get; private set; }
    AudioSource ads;

    const int OutOutSkillTime = 20;

    //������ȴ
    float NoSkilltime;
    bool isLastSkillDone = true;

    bool s_isTuiSkill = false;
    public bool isTuiSkill { get => s_isTuiSkill; set => s_isTuiSkill = value; }

    public float maxTimeNoSkill;
    [Header("��Ϸ����")]
    public GameObject xiaoZhuPrefab;

    void Start()
    {
        if (main == null)
        {
            main = this;
            ads = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(NoSkilltime > 0)
        {
            NoSkilltime -= Time.deltaTime;
            if(NoSkilltime <= 0) { isLastSkillDone = true; }
        }
        
    }

    void Update()
    {
        if (PlayDisable.instance.isPlayerDeath)
        {
            ads.Stop();
        }
    }


    //��ʱ������ȴ
    void SkillDone()
    {
        isLastSkillDone = false;
        NoSkilltime = maxTimeNoSkill;
    }

    //ʹ�õ��Ժ󲻻ᷢ������һ��ʱ��
    public void NoSkill()
    {
        isLastSkillDone = false;
    }

    /// <summary>
    /// ��������Ǹ�ģ�壬�����ӵ�ʱ����������
    /// </summary>
    void Template()
    {
        if (!isLastSkillDone) { return; }//��ȴ�Ƿ���ɣ�����ɾ

        Toast.instance.NoticeSkill("�����滻����");
        //����
    }

    //����1���ݻ�ͣ����
    public void DestroyHider()
    {
        if (!isLastSkillDone) { return; }//��ȴ�Ƿ����
        Toast.instance.NoticeSkill("�����ݻ�ͣ����");
        if (GameObject.FindGameObjectsWithTag("Parkbike").Length != 0)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Parkbike")[Random.Range(0, GameObject.FindGameObjectsWithTag("Parkbike").Length - 1)]);
        }
        Invoke("SkillDone", 20);
    }

    //2��������
    public void GetOutOutOut()
    {
        if (!isLastSkillDone) { return; }//��ȴ�Ƿ����
        Toast.instance.NoticeSkill("�ˣ��ˣ���");
        ads.clip = Resources.Load<AudioClip>("tuituitui");
        ads.Play();
        s_isTuiSkill = true;

        StartCoroutine(OutEnd());
    }

    IEnumerator OutEnd() //�ƺ���
    {
        yield return new WaitForSeconds(OutOutSkillTime);

        s_isTuiSkill = false;
        ads.Stop();
        ads.clip = null;
        isLastSkillDone = true;
    }
}
