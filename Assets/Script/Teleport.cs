using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform whereToGo;
    public ParticleSystem telePart;
    bool isPlayerIn;
    Transform player;
    public GameObject howTeleNotice;
 
    void OnJiaohu()
    {
        if (!isPlayerIn) return;

        if (!CenterCtrl.instance.isTeleDone)
        {
            Toast.instance.HaveNotice("传送门太热了！");
            return;
        }
        player.position = whereToGo.position;
        SoundHelper.telePort();
        CenterCtrl.instance.TeleDone();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        telePart.Stop();
        isPlayerIn = false;
        howTeleNotice.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        telePart.Play();
        isPlayerIn = true;
        howTeleNotice.SetActive(true);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        telePart.Stop();
        isPlayerIn = false;
    }
}
