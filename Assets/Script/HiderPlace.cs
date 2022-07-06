using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiderPlace : MonoBehaviour
{
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }

    public GameObject enterNotice;
    
    public Color defColor;
    Color nowColor;
    

    void Start()
    {
        if (gameObject.tag == "Toitet") isToilet = true;
        else if (gameObject.tag == "Parkbike") isParkBike = true;
        else Debug.LogError("HiderPlace没有Tag！");

        nowColor = GetComponent<Renderer>().material.color;
        StartCoroutine(TestEnd());
    }

    IEnumerator TestEnd()
    {
        while (true)
        {
            yield return null;
            if (EnderSky.instance.WillOver)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 尝试躲进厕所
    /// </summary>
    public void TryToHideWC()
    {
        PlayDisable.instance.playIsDisable = true;
        Debug.Log("设置玩家已经禁用状态...");
        SoundHelper.EnterToilet();
        PlayerCollect.instance.gameObject.SetActive(false);
    }

    public void TryToHideParkLot() 
    {
        if (Random.Range(1, 10) != 2)
        {
            Toast.instance.HaveNotice("进入失败！");
            return;
        }
        PlayDisable.instance.playIsDisable = true;
        SoundHelper.EnterToilet();
        PlayerCollect.instance.gameObject.SetActive(false);
    }

    void OnMouseEnter()
    {
        Debug.Log("鼠标进入");
        GetComponent<SpriteRenderer>().color = defColor;
        enterNotice.SetActive(true);
    }

    void OnMouseExit()
    {
        Debug.Log("鼠标退出");
        GetComponent<SpriteRenderer>().color = nowColor;
        enterNotice.SetActive(false);
    }
}
