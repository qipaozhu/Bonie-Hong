using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DP : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        Debug.Log("玩家进入道具");
        PlayerCollect.instance.SetProp(1, 1);
        SoundHelper.Get();
        Destroy(this.gameObject);
    }
}
