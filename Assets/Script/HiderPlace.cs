using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderPlace : MonoBehaviour
{
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }

    void Start()
    {
        if (this.gameObject.tag == "Toitet") isToilet = true;
        else if (this.gameObject.tag == "Parkbike") isParkBike = true;
        else Debug.LogError("HiderPlace没有Tag！");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isToilet) InWC();
        //else if (isParkBike) InParkLot();
    }

    void InWC()
    {

    }

    void InParkLot() 
    {
        if (Input.GetButtonDown("jiaohu"))
        {
            SoundHelper.Beep();
            CenterCtrl.instance.HaveNotice("没有实装该功能，请关注进展");
        }
    }
}
