using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float makeEnemyDamage;
    public float destoryDistance;

    Rigidbody2D rid;
    Vector3 startPos;

    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        rid.velocity = transform.right * speed;
        startPos = transform.position; 
        Destroy(gameObject,10);
    }

    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if(distance > destoryDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HlinControl hc = other.gameObject.GetComponent<HlinControl>();
        if (hc != null)
        {
            hc.DamageHL(4);
            Destroy(gameObject);
        }
    }
}
