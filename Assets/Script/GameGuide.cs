using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGuide : MonoBehaviour
{
    public Text guideText;

    void FixedUpdate()
    {
        if (CenterCtrl.instance.isBossWar)
        {
            guideText.text = "打败最终Boss——林泓";
        }
        else if(CenterCtrl.instance.isHCM) 
        {
            guideText.text = "躲进厕所里等待出没过去";
        }
        else if(CenterCtrl.instance.lastTreeConut <= 0)
        {
            guideText.text = "前往地图正上方区域(紫色)进行boss战";
        }
        else
        {
            guideText.text = "砍伐地图上的树";
        }
    }
}
