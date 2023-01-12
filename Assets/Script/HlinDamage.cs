using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HlinDamage : MonoBehaviour
{
    Rigidbody2D rb;
    HlinControl hc;
    [Range(-1000,-1)]
    public int damageHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hc = GetComponent<HlinControl>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //ÕÊº“…À∫¶
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc != null)
        {
            if (hc.timeOver == true)
            {
                HlinControl.Found();
                hc.timeOver = false;
            }
            hc.henryAni.SetTrigger("Attack");
            pc.ChangeHealth(damageHealth);
        }

        //“Œ◊”…À∫¶
        CHAIR ch = other.gameObject.GetComponent<CHAIR>();
        if(ch != null)
        {
            hc.henryAni.SetTrigger("Attack");
            ch.ChangeHealth(-1);
        }

        //µÁƒ‘…À∫¶
        COMPUTER com = other.GetComponent<COMPUTER>();
        if(com != null)
        {
            hc.henryAni.SetTrigger("Attack");
            com.ChangeHealth(-1);
        }

        //∂„≤ÿµÿ…À∫¶
        HiderPlace hp = other.GetComponent<HiderPlace>();
        if (hp != null)
        {
            if (HlinControl.instance.isSawPlayerToHide)
            {
                hc.henryAni.SetTrigger("Attack");
                hp.DamageHider(1);
                SoundHelper.DestroyTree();
                hc.WasDestroyHider();
            }
        }
    }
}
