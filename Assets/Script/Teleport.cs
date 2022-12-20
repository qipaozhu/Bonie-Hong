using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform whereToGo;
    public ParticleSystem telePart;
    Transform player;
    public GameObject howTeleNotice;
 
    public void TeleprotTo()
    {
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
        howTeleNotice.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        telePart.Play();
        howTeleNotice.SetActive(true);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        telePart.Stop();
    }
}
