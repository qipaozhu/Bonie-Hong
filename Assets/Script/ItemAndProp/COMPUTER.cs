using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class COMPUTER : MonoBehaviour
{
    public Sprite afterImage;

    int health;
    [Min(1)]
    [Header("ÑªÁ¿")]
    public int maxhealth;

    ParticleSystem ps;
    //ÀäÈ´
    bool Cold = true;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        health = maxhealth;
    }

    public void ChangeHealth(int chheal)
    {
        ps.Play();
        if (Cold)
        {
            health += chheal;
        }
        Cold = false;

        if(health <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = afterImage;
            HlinSkill.main.NoSkill();

            gameObject.tag = "Untagged";
            Destroy(gameObject, 120);    
        }
        Invoke("ResetCold", 1);
    }

    void ResetCold()
    {
        ps.Stop();
        Cold = true;
    }
}
