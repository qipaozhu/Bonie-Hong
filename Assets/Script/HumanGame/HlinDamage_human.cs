using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinDamage_human : MonoBehaviour
{
    public int damageHealth;

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        //Debug.Log("ÓÐÊµÌå");
        if (pc == null)
        {
            //Debug.Log("¿Õ");
            return;
        }
        if (HlinControl_human.instance.timeOver == true)
        {
            HlinControl_human.Found();
            HlinControl_human.instance.timeOver = false;
            Debug.Log("Hey");
        }
        PlayerCollect.instance.ChangeHealth(damageHealth);
    }
}
