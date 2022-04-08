using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMLBR : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        Debug.Log("玩家进入道具");
        PlayerCollect.instance.SetProp(4, 1);
        SoundHelper.Get();
        Destroy(this.gameObject);
    }
}
