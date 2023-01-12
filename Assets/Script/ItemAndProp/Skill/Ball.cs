using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Min(10)]
    public int damageHl;

    void Start()
    {
        GetComponent<AIPath>().destination = HlinControl.instance.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HlinControl hl;
        if (other.gameObject.TryGetComponent<HlinControl>(out hl))
        {
            hl.DamageHL(damageHl);
            Destroy(gameObject);
        }
    }
}
