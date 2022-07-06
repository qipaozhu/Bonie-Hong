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

    //结束时再生成几只
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
            nextHSpawnNotice.text = "泓大家";
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
