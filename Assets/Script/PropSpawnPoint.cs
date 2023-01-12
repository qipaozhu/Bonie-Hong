using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnPoint : MonoBehaviour
{
    public TextMesh lastSpawnTime;
    public GameObject[] propTool;

    float maxWaitSpawnTime;
    float time;

    void Start()
    {
        maxWaitSpawnTime = AllSceneSetting.instance.PropSpawnCold;
    }

    void FixedUpdate()
    {
        if (CenterCtrl.instance.isPropTooMuch && gameObject.tag == "PropSpawn")
        {
            lastSpawnTime.text = "µÀ¾ßÌ«¶à";
            return;
        }
        if(!CenterCtrl.instance.isBossWar && gameObject.tag != "PropSpawn")
        {
            return;
        }
        if (time <= 0)
        {
            Instantiate(propTool[Random.Range(0, propTool.Length)], gameObject.transform.position, Quaternion.identity);

            time = maxWaitSpawnTime;
        }
        else
        {
            lastSpawnTime.text = Mathf.Floor(time).ToString();
            time -= Time.deltaTime;
        }
    }

    public void SetBosswarSpawn()
    {
        time = 1;
        maxWaitSpawnTime = 30;
    }
}
