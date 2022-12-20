using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiderPlace : MonoBehaviour
{
    //�Ƿ���ͣ��������
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }

    //��ص�Ѫ��
    int health;
    public int maxToiletHeath;
    public int maxParklotHeath;

    void Start()
    {
        if (gameObject.tag == "Toitet")
        {
            isToilet = true;
            health = maxToiletHeath;
        }
        else if (gameObject.tag == "Parkbike")
        {
            isParkBike = true;
            health = maxParklotHeath;
        }
        else Debug.LogError("HiderPlaceû��Tag��");
    }

    /// <summary>
    /// ���Զ������
    /// </summary>
    public void TryToHideWC()
    {
        PlayDisable.instance.playIsDisable = true;
        Debug.Log("��������Ѿ�����״̬...");
        SoundHelper.EnterToilet();
        PlayerCollect.instance.gameObject.SetActive(false);
    }

    public void TryToHideParkLot()
    {
        if (Random.Range(1, 10) != 2)
        {
            Toast.instance.HaveNotice("����ʧ�ܣ�");
            return;
        }
        PlayDisable.instance.playIsDisable = true;
        SoundHelper.EnterToilet();
        PlayerCollect.instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// �۳���ص�Ѫ��
    /// </summary>
    /// <param name="damage">�����ģ��۶���</param>
    public void DamageHider(int damage)
    {
        if(damage < 0) { Debug.LogWarning("��Ĳ�����");return; }
        health -= damage;
        if(health <=0)
        {
            Destroy(gameObject);
        }
    }
}
