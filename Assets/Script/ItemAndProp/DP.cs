using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DP : MonoBehaviour
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
        PlayerCollect.instance.Prop1Conut++;
        SoundHelper.Get();
        Destroy(this.gameObject);
    }
}
