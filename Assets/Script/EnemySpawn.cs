using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int coldToSpawn;

    float time;
    bool isColdOK = true;

    [Header("敌人预制体")]
    public GameObject[] enemyPrefab;

    void Start()
    {
        time = coldToSpawn;
    }

    void FixedUpdate()
    {
        //如果没有冷却完毕
        if (!isColdOK)
        {
            time -= Time.deltaTime;
            if(time <= 0) { isColdOK = true; }
        }

        if (isColdOK)
        {
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length - 1)], transform);
            isColdOK = false;
            time = coldToSpawn;
        }

    }
}
