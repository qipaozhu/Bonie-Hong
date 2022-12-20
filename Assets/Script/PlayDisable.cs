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

    //InputSystem����Exit
    void OnExitHider()
    {
        Debug.Log("�˳�����");
        if (playIsDisable)
        {
            PlayerSetActive();
        }
    }

    void Update()
    {
        if (playIsDisable){ exitWCNotice.SetActive(true); }//������ʾΪ��ʾ
        else { exitWCNotice.SetActive(false); }
    }

    public void PlayerSetActive()
    {
        if (playIsDisable == false)
        {
            Debug.LogWarning("����ڻ���ٴ����Active");
            return;
        }//�ظ������û

        playerLastHider = null;
        SoundHelper.EnterToilet();
        player.SetActive(true);
        playIsDisable = false;
    }

    /// <summary>
    /// �����ϴ���Ҷ�صص�
    /// </summary>
    /// <param name="hp">��ص�����</param>
    public void SetPlayerLastHider(HiderPlace hp)
    {
        playerLastHider = hp;
    }
}
