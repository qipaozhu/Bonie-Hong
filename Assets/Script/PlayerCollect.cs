using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public static PlayerCollect instance { get; private set; }
    Rigidbody2D ridy;
    public float speed;
    //====设置血量====
    private int maxHealth = 20;
    private int nowHealth = 20;
    //====传出血量====
    public int PnowHealth { get { return nowHealth; } }
    public int PmaxHealth { get { return maxHealth; } }
    //====无敌时间====
    private bool noDamage = false;
    private int timeNoDamage;
    private int timeNoDamageMax = 300;
    //====默认朝向====
    private Vector2 lookWhere = new Vector2(1, 0);

    void Start()
    {
        instance = this;
        ridy = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        Vector2 moveWhere = new Vector2(moveX, moveY);
        if(moveWhere.x != 0 || moveWhere.y != 0)
        {
            lookWhere = moveWhere;
        }
        //====移动====
        Vector2 pos = ridy.position;
        pos.x += moveX * speed * Time.deltaTime;
        pos.y += moveY * speed * Time.deltaTime;
        ridy.MovePosition(pos);
        UIhealthyManaer.instance.UpdateHealthBar(maxHealth, nowHealth);
        //====交互====
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hitnpc = Physics2D.Raycast(ridy.position, lookWhere , 1f, LayerMask.GetMask("CanNPC"));
            if (hitnpc.collider != null)
            {
                HiderPlace wc = hitnpc.collider.GetComponent<HiderPlace>();
                if (wc != null)
                {
                    Debug.Log("wc done");
                }
            }
        }
        //====无敌时间减去====
        if (noDamage)
        {
            if (timeNoDamage == 0)
            {
                noDamage = false;
            }
            else if (timeNoDamage != 0)
            {
                timeNoDamage = timeNoDamage - 1;
            }
        }
        //====
    }
    public void ChangeHealth(int changeHealth)
    {
        if (noDamage & changeHealth <= 1)
        {
            Debug.Log("无敌");
            return;
        }
        timeNoDamage = timeNoDamageMax;
        noDamage = true;
        nowHealth = Mathf.Clamp(changeHealth + nowHealth, 0, maxHealth);
        Debug.Log(nowHealth + "和" + maxHealth);
    }
}
