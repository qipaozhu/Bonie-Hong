using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHspawn : MonoBehaviour
{
    public static LHspawn instance;

    float maxSpwanTime;
    float time;
    bool isHlIn;

    public GameObject hl;
    public Transform bosswarPosition;
    public TextMesh nextHSpawnNotice;

    void Start()
    {
        maxSpwanTime = AllSceneSetting.instance.lhNextSpawn;
        time = maxSpwanTime;
        instance = this;
    }

    void FixedUpdate()
    {
        CenterCtrl.instance.isHllife = isHlIn;

        if (FindObjectOfType<HlinControl>() == null)
        {
            if (CenterCtrl.instance.isBossWar && isHlIn)
            {
                EnderSky.instance.End();
            }
            isHlIn = false;
            time -= Time.deltaTime;
            nextHSpawnNotice.text = "��һ������LH��" + Mathf.Floor(time).ToString();
            if (time <= 0)
            {
                Instantiate(hl, gameObject.transform.position, Quaternion.identity);
                time = maxSpwanTime;
                nextHSpawnNotice.text = "�Ѿ��������µ�һֻ";
                isHlIn = true;
            }
        }
        else
        {
            nextHSpawnNotice.text = "�����";
            isHlIn = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHlIn || CenterCtrl.instance.isBossWar) { return; }
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if(pc != null)
        {
            HlinSkill.main.GetOutOutOut();
        }
    }

    public void SpawnHenryInBosswar()
    {
        if (!isHlIn)
        {
            Instantiate(hl, transform.position, Quaternion.identity);
            time = maxSpwanTime;
            nextHSpawnNotice.text = "�Ѿ��������µ�һֻ";
            isHlIn = true;
        }
        
        transform.position = bosswarPosition.position;
        HlinControl.instance.transform.position = transform.position;
    }
}
