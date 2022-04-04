using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform whereToGo;
    public ParticleSystem telePart;
    bool isPlayerIn;
    Transform player;

    void Update()
    {
        if (!isPlayerIn) return;

        if (Input.GetButtonDown("jiaohu"))
        {
            if (GameObject.Find("CenterCtrl").GetComponent<CenterCtrl>() != null)
            {
                if (!CenterCtrl.instance.isTeleDone)
                {
                    CenterCtrl.instance.HaveNotice("«Îµ»¥˝¿‰»¥£°");
                    return;
                }
                player.position = whereToGo.position;
                SoundHelper.telePort();
                CenterCtrl.instance.TeleDone();
            }
            else if (GameObject.Find("CenterCtrl").GetComponent<CenterCtrl_human>() != null)
            {
                if (!CenterCtrl_human.instance.isTeleDone)
                {
                    CenterCtrl_human.instance.HaveNotice("«Îµ»¥˝¿‰»¥£°");
                    return;
                }
                player.position = whereToGo.position;
                SoundHelper.telePort();
                CenterCtrl_human.instance.TeleDone();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        telePart.Stop();
        isPlayerIn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        telePart.Play();
        isPlayerIn = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        telePart.Stop();
        isPlayerIn = false;
    }
}
