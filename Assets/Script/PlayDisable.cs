using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayDisable : MonoBehaviour
{
    public static PlayDisable instance { get; private set; }

    [HideInInspector]
    public bool playIsDisable;
    public bool isPlayerDeath { get; set; } = false;
    public GameObject player;
    public GameObject exitWCNotice;


    void Awake()
    {
        instance = this;
    }

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
        if (playIsDisable == true)
        {
            exitWCNotice.SetActive(true); //设置提示为显示
        }
        if(playIsDisable == false)
        {
            exitWCNotice.SetActive(false);
        }
    }

    public void PlayerSetActive()
    {
        if (playIsDisable == false)
        {
            Debug.LogWarning("玩家在活动是再次设成Active");
            return;
        }
        SoundHelper.EnterToilet();
        player.SetActive(true);
        playIsDisable = false;
    }
}
