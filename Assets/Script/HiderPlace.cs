using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiderPlace : MonoBehaviour
{
    bool isToilet = false;
    bool isParkBike = false;
    public bool IsT { get { return isToilet; } }
    public bool IsP { get { return isParkBike; } }

    public GameObject enterNotice;
    
    public Color defColor;
    Color nowColor;
    

    void Start()
    {
        if (gameObject.tag == "Toitet") isToilet = true;
        else if (gameObject.tag == "Parkbike") isParkBike = true;
        else Debug.LogError("HiderPlaceû��Tag��");

        nowColor = GetComponent<Renderer>().material.color;
        StartCoroutine(TestEnd());
    }

    IEnumerator TestEnd()
    {
        while (true)
        {
            yield return null;
            if (EnderSky.instance.WillOver)
            {
                Destroy(gameObject);
            }
        }
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

    void OnMouseEnter()
    {
        Debug.Log("������");
        GetComponent<SpriteRenderer>().color = defColor;
        enterNotice.SetActive(true);
    }

    void OnMouseExit()
    {
        Debug.Log("����˳�");
        GetComponent<SpriteRenderer>().color = nowColor;
        enterNotice.SetActive(false);
    }
}
