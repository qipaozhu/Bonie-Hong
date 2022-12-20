using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiderPlace : MonoBehaviour
{
    //是否是停车棚或厕所
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }

    //躲藏地血量
    int health;
    public int maxToiletHeath;
    public int maxParklotHeath;

    void Start()
    {
        if (gameObject.tag == "Toitet")
        {
            isToilet = true;
            health = maxToiletHeath;
        }
        else if (gameObject.tag == "Parkbike")
        {
            isParkBike = true;
            health = maxParklotHeath;
        }
        else Debug.LogError("HiderPlace没有Tag！");
    }

    /// <summary>
    /// 尝试躲进厕所
    /// </summary>
    public void TryToHideWC()
    {
        PlayDisable.instance.playIsDisable = true;
        Debug.Log("设置玩家已经禁用状态...");
        SoundHelper.EnterToilet();
        PlayerCollect.instance.gameObject.SetActive(false);
    }

    public void TryToHideParkLot()
    {
        if (Random.Range(1, 10) != 2)
        {
            Toast.instance.HaveNotice("进入失败！");
            return;
        }
        PlayDisable.instance.playIsDisable = true;
        SoundHelper.EnterToilet();
        PlayerCollect.instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// 扣除躲藏地血量
    /// </summary>
    /// <param name="damage">正数的，扣多少</param>
    public void DamageHider(int damage)
    {
        if(damage < 0) { Debug.LogWarning("错的参数！");return; }
        health -= damage;
        if(health <=0)
        {
            Destroy(gameObject);
        }
    }
}
