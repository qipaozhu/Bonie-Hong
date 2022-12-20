using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnderSky : MonoBehaviour
{
    public static EnderSky instance { get; private set; }

    public GameObject endNotice; //结束标语
    public Text playTimeNotice;

    private void Awake()
    {
        instance = this;
    }

    public void TreeOver()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null || CenterCtrl.instance.lastTreeConut > 0) return;

        SoundHelper.CompeleGame();
        End();
    }

    void End()
    {
        endNotice.SetActive(true);
        playTimeNotice.text = "你游玩了：" + Mathf.Floor(CenterCtrl.instance.GamePlayTime).ToString() + "秒";
        Time.timeScale = 0;
    }
}
