using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TreeControl : MonoBehaviour
{
    //====ÑªÁ¿====
    int treeHealth;
    public int maxTreeHealth;
    public ParticleSystem treePart;
    Animator anm;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();

        SoundHelper.DestroyTree();
        treeHealth--;
        treePart.Play();

        if (treeHealth <= 0 && treeHealth > -100)
        {
            treeHealth = -999;
            CenterCtrl.instance.SetTree(-1);
            anm.SetBool("isDest", true);
            SoundHelper.Get();

            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(transform.GetChild(0).gameObject);

            Destroy(gameObject, 60);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        treePart.Stop();
    }

    private void Start()
    {
        treeHealth = maxTreeHealth;
        treePart.Stop();
        anm = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        GetComponentInChildren<Light2D>().intensity = 2;
    }
    private void OnMouseExit()
    {
        GetComponentInChildren<Light2D>().intensity = 0;
    }
}
