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
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null || CenterCtrl.instance.lastTreeConut > 0 || CenterCtrl.instance.isBossWar) return;

        pc.transform.position += new Vector3(0, 8);

        CenterCtrl.instance.isBossWar= true;
        //SoundHelper.CompeleGame();
        //End();
    }

    public void End()
    {
        SoundHelper.CompeleGame();
        endNotice.SetActive(true);
        playTimeNotice.text = "你游玩了：" + Mathf.Floor(CenterCtrl.instance.GamePlayTime).ToString() + "秒";
        Time.timeScale = 0;
    }
}
