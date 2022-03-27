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
            Debug.Log("Íæ¼ÒÊÇ½ûÓÃ");
            exitWCNotice.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
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
