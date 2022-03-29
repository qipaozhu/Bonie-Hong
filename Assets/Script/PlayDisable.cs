using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDisable : MonoBehaviour
{
    public static PlayDisable instance { get; private set; }

    public bool playIsDisable;
    public GameObject player;
    public GameObject exitWCNotice;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
       
        if (playIsDisable == true)
        {
            Debug.Log("玩家是禁用");
            exitWCNotice.SetActive(true); //设置提示为显示
            if (Input.GetKeyDown(KeyCode.X))
            {
                SoundHelper.EnterToilet();
                player.SetActive(true);
                playIsDisable = false;
            }
        }
        if(playIsDisable == false)
        {
            exitWCNotice.SetActive(false);
        }
    }
}
