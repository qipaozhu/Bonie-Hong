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
        CenterCtrl.instance.isHllife = isHlIn;

        if (FindObjectOfType<HlinControl>() == null)
        {
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
        if (!isHlIn) { return; }
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if(pc != null)
        {
            HlinSkill.main.GetOutOutOut();
        }
    }
}
