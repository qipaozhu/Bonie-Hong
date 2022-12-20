using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public static Toast instance { get; private set; }
    [Header("��ʾ��")]
    public GameObject notice; //������ʾ
    public Text noticeText; //������ʾ
    [Header("������ʾ��")]
    public GameObject hlSkill;
    public Text hlSkillText;
    [Header("��ʾ��")]
    public GameObject messageBoxPrefab;
    
    void Awake()
    {
        instance = this;
    }

    //��ʾ����
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

    //HL�ͷż�����ʾ
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
