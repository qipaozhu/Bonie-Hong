using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INFORMATION : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        Debug.Log("��ҽ������");
        PlayerCollect.instance.Prop2Conut++;
        SoundHelper.Get();
        Destroy(this.gameObject);
    }
}
