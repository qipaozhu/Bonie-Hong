using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinDamage : MonoBehaviour
{
    Rigidbody2D rb;
    public int damageHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        //Debug.Log("ÓÐÊµÌå");
        if (pc == null)
        {
            //Debug.Log("¿Õ");
            return;
        }
        if (HlinControl.instance.timeOver == true)
        {
            HlinControl.Found();
            HlinControl.instance.timeOver = false;
        }
        PlayerCollect.instance.ChangeHealth(damageHealth);
    }
}
