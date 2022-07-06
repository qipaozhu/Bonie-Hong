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
        if(other.gameObject.GetComponent<HlinControl>() != null)
        {
            other.gameObject.GetComponent<HlinControl>().DamageHL(4);
            Destroy(gameObject);
        }
    }
}
