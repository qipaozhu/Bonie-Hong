using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhealthyManaer : MonoBehaviour
{
    public Image healthBar;

    public static UIhealthyManaer instance { get; private set; }
    void Start()
    {
        instance = this;
    }
    //====¸üÐÂÑªÌõ====
    public void UpdateHealthBar(int maxH,int nowH)
    {
        healthBar.fillAmount = (float)nowH / (float)maxH;
    }
}
