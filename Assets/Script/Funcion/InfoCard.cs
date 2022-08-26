using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[AddComponentMenu("BrTool/InfoCard")]
public class InfoCard : MonoBehaviour
{
    [Header("�����������Ϣ")]
    public string[] info;

    public void ShowInformation()
    {
        InfoCardManager.main.ShowInfomation(info);
    }

    
}
