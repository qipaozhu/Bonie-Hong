using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinSkill : MonoBehaviour
{
    public static HlinSkill main { get; private set; }
    AudioSource ads;

    //������ȴ
    float NoSkilltime;
    bool isLastSkillDone = true;

    bool s_isTui = false;
    public bool isTuiSkill { get => s_isTui; set => s_isTui = value; }

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
    void Update()
    {
        if(NoSkilltime > 0)
        {
            NoSkilltime -= Time.deltaTime;
            if(NoSkilltime <= 0) { isLastSkillDone = true; }
        }
    }
    

    //��ʱ������ȴ
    void SkillDone()
    {
        isLastSkillDone = false;
        NoSkilltime = maxTimeNoSkill;
    }

    //ʹ�õ��Ժ󲻻ᷢ������
    public void NoSkill()
    {
        isLastSkillDone = false;
    }
    //����1���ݻ�ͣ����
    public void DestroyHider()
    {
        if (!isLastSkillDone) { return; }
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
        if (!isLastSkillDone) { return; }
        ads.clip = Resources.Load<AudioClip>("tuituitui");
        ads.Play();
        s_isTui = true;

        StartCoroutine(OutEnd());
    }

    IEnumerator OutEnd()
    {
        yield return new WaitForSeconds(20);

        s_isTui = false;
        ads.Stop();
        ads.clip = null;
        isLastSkillDone = true;
    }
}
