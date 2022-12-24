using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayDisable : MonoBehaviour
{
    public static PlayDisable instance { get; private set; }

    [HideInInspector]
    bool playerIsDisable;
    public bool playIsDisable { get => playerIsDisable; }
    public bool isPlayerDeath { get; set; } = false;
    GameObject player;
    HiderPlace playerLastHider;
    public HiderPlace lastPlayerHider { get => playerLastHider; }

    public GameObject exitWCNotice;


    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //InputSystem接收Exit
    void OnExitHider()
    {
        Debug.Log("退出厕所");
        if (playIsDisable)
        {
            PlayerSetActive();
        }
    }

    void Update()
    {
        if (playIsDisable){ exitWCNotice.SetActive(true); }//设置提示为显示
        else { exitWCNotice.SetActive(false); }
    }

    public void PlayerSetActive()
    {
        if (playIsDisable == false)
        {
            Debug.LogWarning("玩家在活动是再次设成Active");
            return;
        }//重复的设置活动

        playerLastHider = null;
        SoundHelper.EnterToilet();
        player.SetActive(true);
        playerIsDisable = false;
    }

    /// <summary>
    /// 设置上次玩家躲藏地点
    /// </summary>
    /// <param name="hp">躲藏地属性</param>
    public void SetPlayerHide(HiderPlace hp)
    {
        playerIsDisable = true;
        playerLastHider = hp;
    }
}
