using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHAIR : MonoBehaviour
{
    [Header("³õÊ¼ÑªÁ¿")]
    public int maxHealth;
    int health;

    bool ColdOver;

    private void Start()
    {
        health = maxHealth;
        ColdOver = true;
    }

    public void ChangeHealth(int healthDa)
    {
        if (ColdOver)
        {
            health += healthDa;
            ColdOver = false;
            Invoke("WaitCold", 1);
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void WaitCold()
    {
        ColdOver = true;
    }
}
