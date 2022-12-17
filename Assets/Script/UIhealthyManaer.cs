using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhealthyManaer : MonoBehaviour
{
    Slider healthFill;
    public TMPro.TextMeshProUGUI healthText;
    public static UIhealthyManaer instance { get; private set; }
    void Start()
    {
        instance = this;
        healthFill = GetComponent<Slider>();
    }

    //====¸üÐÂÑªÌõ====
    public void UpdateHealthBar(int maxH,int nowH)
    {
        healthText.text = nowH + "/" + maxH;
        healthFill.value = (float)nowH / (float)maxH;
    }
}
