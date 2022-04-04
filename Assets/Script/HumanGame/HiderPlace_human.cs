using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderPlace_human : MonoBehaviour
{
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }

    void Start()
    {
        if (gameObject.tag == "Toitet") isToilet = true;
        else if (gameObject.tag == "Parkbike") isParkBike = true;
        else Debug.LogError("HiderPlace√ª”–Tag£°");
    }

    private void Update()
    {
        if (isToilet) InWC();
        else if (isParkBike) InParkLot();
    }

    void InWC()
    {
        if (EnderSky_human.instance.WillOver) Destroy(gameObject);
    }

    void InParkLot() 
    {
        if (EnderSky_human.instance.WillOver) Destroy(gameObject);
    }
}
