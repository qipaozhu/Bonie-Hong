using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANTUMN : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        Debug.Log("玩家进入道具");
        ClickThis();
    }
    public void ClickThis()
    {
        PlayerCollect.instance.Prop3Conut++;
        SoundHelper.Get();
        Destroy(gameObject);
    }
}
