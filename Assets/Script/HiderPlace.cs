using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderPlace : MonoBehaviour
{
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }
    bool isHumanMode;

    public GameObject enterNotice;
    
    public Color defColor;
    Color nowColor;
    

    void Start()
    {
        if (gameObject.tag == "Toitet") isToilet = true;
        else if (gameObject.tag == "Parkbike") isParkBike = true;
        else Debug.LogError("HiderPlace没有Tag！");
        CenterCtrl_human ch = GetComponent<CenterCtrl_human>();
        if (ch != null) isHumanMode = true;

        nowColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (isToilet) InWC();
        else if (isParkBike) InParkLot();
    }

    void InWC()
    {
        if (isHumanMode) { if (EnderSky_human.instance.WillOver) Destroy(gameObject); }
        else if (EnderSky.instance.WillOver) Destroy(gameObject);
    }

    void InParkLot() 
    {
        if (isHumanMode) { if (EnderSky_human.instance.WillOver) Destroy(gameObject); }
        else if (EnderSky.instance.WillOver) Destroy(gameObject);
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
