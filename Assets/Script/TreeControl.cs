using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControl : MonoBehaviour
{
    //====血量====
    int treeHealth;
    public int maxTreeHealth;

    //====时间冷却====
    float time;
    float maxTime = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null || time > 0) return;
        //如果是玩家且冷却到了
        SoundHelper.DestroyTree();
        treeHealth--;
        time = maxTime;

        if(treeHealth <= 0)
        {
            CenterCtrl.instance.SetTree(-1);
            SoundHelper.Get();
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        time = time - Time.deltaTime;
    }

    private void Start()
    {
        time = maxTime;
        treeHealth = maxTreeHealth;
    }
}
