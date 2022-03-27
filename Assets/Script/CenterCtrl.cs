using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterCtrl : MonoBehaviour
{
    public static CenterCtrl instance { get; private set; }
    public static bool isHFindPlayer;
    public float nextHBonie;
    float fullHBonie;
    public Image hlBar;
    public Text hlText;
    public bool isHCM = false;
    

    //重新开始记时
    void ResetHL() 
    {
        nextHBonie = Random.Range(60, 260);
        fullHBonie = nextHBonie;
        isHCM = false;
        HlinControl.instance.SetTarget(2);
    }

    void Update()
    {
        if (nextHBonie > 0)
        {
            hlBar.fillAmount = nextHBonie / fullHBonie;
            hlText.text = Mathf.Floor(nextHBonie).ToString();
            nextHBonie = nextHBonie - Time.deltaTime;
        }

        if ( nextHBonie <= 0 && !isHCM )
        {
            //SoundHelper.hCome();
            Debug.Log("开始出没！");
            HCMfuncion();
        }
    }

    void Start()
    {
        instance = this;
        ResetHL();
    }

    void HCMfuncion()
    {
        isHCM = true;

        //功能：
        SoundHelper.hCome();
        if(!PlayDisable.instance.playIsDisable) HlinControl.instance.SetTarget(1);

        //====
        Invoke("ResetHL", 30f);
        
    }
}
