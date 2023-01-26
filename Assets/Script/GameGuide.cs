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
            guideText.text = "�������Boss��������";
        }
        else if(CenterCtrl.instance.isHCM) 
        {
            guideText.text = "���������ȴ���û��ȥ";
        }
        else if(CenterCtrl.instance.lastTreeConut <= 0)
        {
            guideText.text = "ǰ����ͼ���Ϸ�����(��ɫ)����bossս";
        }
        else
        {
            guideText.text = "������ͼ�ϵ���";
        }
    }
}
