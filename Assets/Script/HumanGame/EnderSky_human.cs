using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnderSky_human : MonoBehaviour
{
    public static EnderSky_human instance { get; private set; }
    bool e_treeIsNone = false;
    public bool WillOver { get { return e_treeIsNone; } }

    public GameObject endNotice; //Ω· ¯±Í”Ô
    public Text playTimeNotice;

    private void Awake()
    {
        instance = this;
    }

    public void TreeOver()
    {
        if (e_treeIsNone) return;
        e_treeIsNone = true;
        CenterCtrl_human.instance.TiShiEnd();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null || !e_treeIsNone) return;

        SoundHelper.CompeleGame();
        End();
    }

    void End()
    {
        endNotice.SetActive(true);
        playTimeNotice.text = Mathf.Floor(CenterCtrl_human.instance.GamePlayTime).ToString();
        Time.timeScale = 0;
    }
}
