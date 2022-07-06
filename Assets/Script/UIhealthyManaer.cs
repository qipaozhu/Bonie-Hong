using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhealthyManaer : MonoBehaviour
{
    public Image healthBar;
    public TMPro.TextMeshProUGUI healthText;
    public static UIhealthyManaer instance { get; private set; }
    void Start()
    {
        instance = this;
    }

    //====¸üÐÂÑªÌõ====
    public void UpdateHealthBar(int maxH,int nowH)
    {
        healthText.text = nowH + "/" + maxH;
        healthBar.fillAmount = (float)nowH / (float)maxH;
    }
}
