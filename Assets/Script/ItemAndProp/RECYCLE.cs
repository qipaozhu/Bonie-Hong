using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RECYCLE : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        Debug.Log("��ҽ������");
        ClickThis();
    }
    public void ClickThis()
    {
        PlayerCollect.instance.Item2Conut++; // ���ӵ�������
        SoundHelper.Get();
        Destroy(gameObject);
    }
}
