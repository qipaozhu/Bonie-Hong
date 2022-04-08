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

    void Start()
    {
        maxSpwanTime = AllSceneSetting.instance.lhNextSpawn;
        time = maxSpwanTime;
    }

    void FixedUpdate()
    {
        if (GameObject.Find("CenterCtrl").GetComponent<CenterCtrl>() != null)
        {
            CenterCtrl.instance.isHllife = isHlIn;
        }
        else
        {
            CenterCtrl_human.instance.isHllife = isHlIn;
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Debug.Log("没有找到LH");

            isHlIn = false;
            time -= Time.deltaTime;
            nextHSpawnNotice.text = "下一次生成LH：" + Mathf.Floor(time).ToString();
            if (time <= 0)
            {
                Instantiate(hl, gameObject.transform.position, Quaternion.identity);
                time = maxSpwanTime;
                nextHSpawnNotice.text = "已经生成了新的一只";
                isHlIn = true;
            }
        }
        else
        {
            nextHSpawnNotice.text = "已经生成";
            isHlIn = true;
        }
    }
}
