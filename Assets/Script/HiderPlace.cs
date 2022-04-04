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

    void Start()
    {
        if (this.gameObject.tag == "Toitet") isToilet = true;
        else if (this.gameObject.tag == "Parkbike") isParkBike = true;
        else Debug.LogError("HiderPlace√ª”–Tag£°");
        CenterCtrl_human ch = GetComponent<CenterCtrl_human>();
        if (ch != null) isHumanMode = true;
    }

    private void Update()
    {
        if (isToilet) InWC();
        else if (isParkBike) InParkLot();
    }

    void InWC()
    {
        if(isHumanMode) if (EnderSky_human.instance.WillOver) Destroy(gameObject);
        else if (EnderSky.instance.WillOver) Destroy(gameObject);
    }

    void InParkLot() 
    {
        if(isHumanMode) if(EnderSky_human.instance.WillOver) Destroy(gameObject);
        else if(EnderSky.instance.WillOver)Destroy(gameObject);
    }
}
