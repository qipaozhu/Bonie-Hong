using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public static Toast instance { get; private set; }
    [Header("提示条")]
    public GameObject notice; //弹出提示
    public Text noticeText; //弹出提示
    [Header("技能提示条")]
    public GameObject hlSkill;
    public Text hlSkillText;
    [Header("提示框")]
    public GameObject messageBoxPrefab;
    
    void Awake()
    {
        instance = this;
    }

    //提示功能
    public void HaveNotice(string whatToNotice)
    {
        StopCoroutine(EndNotice());
        notice.SetActive(true);
        noticeText.text = whatToNotice;
        SoundHelper.Beep();
        StartCoroutine(EndNotice());
    }
    IEnumerator EndNotice()
    {
        yield return new WaitForSeconds(5);
        notice.SetActive(false);
    }

    //HL释放技能提示
    public void NoticeSkill(string skillname)
    {
        hlSkill.SetActive(true);
        hlSkillText.text = skillname;
        SoundHelper.July5();
        Invoke("EndSkillNotice", 5);
    }
    void EndSkillNotice() { hlSkill.SetActive(false); }

    public void InfoBox(string info)
    {
        GameObject box = Instantiate(messageBoxPrefab,transform,false);
        box.GetComponentInChildren<Text>().text = info;
    }
}
