using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.;

public class Login : MonoBehaviour
{
    public static Login instance { get; private set; }

    private void Start()
    {
        //��֤�Ƿ�Ϊ����
        GameObject peVer = GameObject.Find("PeopleFromNotice");
#if UNITY_ANDROID

        if (peVer != null)
        {
            peVer.GetComponent<Text>().text = "�ֻ���";
        }
        AllSceneSetting.instance.isRealPlayer = true;
        gameObject.SetActive(false);
        return;
        
#else
        

#if UNITY_EDITOR
        string[] people = { "aaa", "����Ϸ�����", "VhOA91uIA" };
#else
        string[] people = Environment.GetCommandLineArgs();
#endif
        //1.���Ȳ���3
        if (people.Length != 3)
        {
            AllSceneSetting.instance.isRealPlayer = false;
            if (peVer != null)
            {
                peVer.GetComponent<Text>().text = "���ð�";
            }
            return;
        }
        //2.����λ��������
        if (people[2] != "VhOA91uIA")
        {
            AllSceneSetting.instance.isRealPlayer = false;
            if(peVer != null)
            {
                peVer.GetComponent<Text>().text = "���ð�";
            }
            return;
        }

        //�ǵĻ�
        AllSceneSetting.instance.isRealPlayer = true;

        //��ʾ
        if (peVer != null)
        {
            peVer.GetComponent<Text>().text = people[1] + "�� - ����";
        }
        gameObject.SetActive(false);
#endif
    }
}
