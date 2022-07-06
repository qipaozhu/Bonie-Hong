using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHspawn : MonoBehaviour
{
    float maxSpwanTime;
    float time;
    bool isHlIn;

    public GameObject hl;
    public TextMesh nextHSpawnNotice;

    //����ʱ�����ɼ�ֻ
    bool isSpawnOK = false;

    void Start()
    {
        maxSpwanTime = AllSceneSetting.instance.lhNextSpawn;
        time = maxSpwanTime;
    }

    void FixedUpdate()
    {
        CenterCtrl.instance.isHllife = isHlIn;

        if (EnderSky.instance.WillOver && !isSpawnOK)
        {
            Instantiate(hl);
            Instantiate(hl);
            Instantiate(hl);

            isSpawnOK = true;
        }

        if (FindObjectOfType<HlinControl>() == null)
        {
            Debug.Log("û���ҵ�LH");

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
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if(pc != null)
        {
            HlinSkill.main.GetOutOutOut();
        }
    }
}