using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinDamage : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(-100,-1)]
    public int damageHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        //ÕÊº“…À∫¶
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc != null)
        {
            if (HlinControl.instance.timeOver == true)
            {
                HlinControl.Found();
                HlinControl.instance.timeOver = false;
            }
            pc.ChangeHealth(damageHealth);
        }

        //“Œ◊”…À∫¶
        CHAIR ch = other.gameObject.GetComponent<CHAIR>();
        if(ch != null)
        {
            ch.ChangeHealth(damageHealth);
        }

        //µÁƒ‘…À∫¶
        COMPUTER com = other.GetComponent<COMPUTER>();
        if(com != null)
        {
            com.ChangeHealth(damageHealth);
        }

        //∂„≤ÿµÿ…À∫¶
        HiderPlace hp = other.GetComponent<HiderPlace>();
        if (hp != null)
        {
            if (HlinControl.instance.isSawPlayerToHide)
            {
                hp.DamageHider(1);
                HlinControl.instance.WasDestroyHider();
            }
        }
    }
}
