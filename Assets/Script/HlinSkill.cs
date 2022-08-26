using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinSkill : MonoBehaviour
{
    public static HlinSkill main { get; private set; }
    AudioSource ads;

    const int OutOutSkillTime = 20;

    //技能冷却
    float NoSkilltime;
    bool isLastSkillDone = true;

    bool s_isTuiSkill = false;
    public bool isTuiSkill { get => s_isTuiSkill; set => s_isTuiSkill = value; }

    public float maxTimeNoSkill;
    [Header("游戏对象")]
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


    //定时技能冷却
    void SkillDone()
    {
        isLastSkillDone = false;
        NoSkilltime = maxTimeNoSkill;
    }

    //使用电脑后不会发动技能一段时间
    public void NoSkill()
    {
        isLastSkillDone = false;
    }

    /// <summary>
    /// 这个函数是个模板，新增加的时候按照这里来
    /// </summary>
    void Template()
    {
        if (!isLastSkillDone) { return; }//冷却是否完成，不能删

        Toast.instance.NoticeSkill("自行替换内容");
        //操作
    }

    //技能1：摧毁停车棚
    public void DestroyHider()
    {
        if (!isLastSkillDone) { return; }//冷却是否完成
        Toast.instance.NoticeSkill("厕所摧毁停车棚");
        if (GameObject.FindGameObjectsWithTag("Parkbike").Length != 0)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Parkbike")[Random.Range(0, GameObject.FindGameObjectsWithTag("Parkbike").Length - 1)]);
        }
        Invoke("SkillDone", 20);
    }

    //2：退退退
    public void GetOutOutOut()
    {
        if (!isLastSkillDone) { return; }//冷却是否完成
        Toast.instance.NoticeSkill("退！退！退");
        ads.clip = Resources.Load<AudioClip>("tuituitui");
        ads.Play();
        s_isTuiSkill = true;

        StartCoroutine(OutEnd());
    }

    IEnumerator OutEnd() //善后工作
    {
        yield return new WaitForSeconds(OutOutSkillTime);

        s_isTuiSkill = false;
        ads.Stop();
        ads.clip = null;
        isLastSkillDone = true;
    }
}
