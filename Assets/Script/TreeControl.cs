using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TreeControl : MonoBehaviour
{
    //====血量====
    int treeHealth;
    public int maxTreeHealth;
    public ParticleSystem treePart;
    Animator anm;

    //====时间冷却====
    float time;
    float maxTime = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc != null && time <= 0 && treeHealth > -100)
        {
            //如果是玩家且冷却到了
            SoundHelper.DestroyTree();
            treeHealth--;
            time = maxTime;
            treePart.Play();

            if (treeHealth <= 0 && treeHealth > -100)
            {
                treeHealth = -999;
                if (GameObject.Find("CenterCtrl").GetComponent<CenterCtrl_human>() != null)
                {
                    CenterCtrl_human.instance.SetTree(-1);
                }//如果是猎杀模式
                else
                {
                    CenterCtrl.instance.SetTree(-1);
                }
                anm.SetBool("isDest", true);
                SoundHelper.Get();
                
                Destroy(gameObject,5);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        treePart.Stop();
    }

    private void Update()
    {
        time = time - Time.deltaTime;
    }

    private void Start()
    {
        time = maxTime;
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
