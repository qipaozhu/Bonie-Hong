using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnPoint : MonoBehaviour
{
    public TextMesh lastSpawnTime;
    public GameObject[] propTool;

    float maxWaitSpawnTime;
    float time;
    bool isPlayerPickUpProp;

    void Start()
    {
        isPlayerPickUpProp = true;
        maxWaitSpawnTime = AllSceneSetting.instance.PropSpawnCold;
    }

    void FixedUpdate()
    {
        if (time <= 0)
        {
            Instantiate(propTool[Random.Range(0, propTool.Length)], gameObject.transform.position, Quaternion.identity);
            isPlayerPickUpProp = false;
            lastSpawnTime.text = "已经生成";

            time = maxWaitSpawnTime;
        }
        else if(isPlayerPickUpProp)
        {
            lastSpawnTime.text = Mathf.Floor(time).ToString();
            time -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //====检测玩家是否在碰撞体内====
        isPlayerPickUpProp = true;
    }
}
