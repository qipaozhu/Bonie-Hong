using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public static PlayerCollect instance { get; private set; }
    Rigidbody2D ridy;
    public float speed;
    public GameObject deadMenu;
    //====设置血量====
    private int maxHealth = 20;
    private int nowHealth = 20;
    //====传出血量====
    public int PnowHealth { get { return nowHealth; } }
    public int PmaxHealth { get { return maxHealth; } }
    //====无敌时间====
    private bool noDamage = false;
    private float timeNoDamage;
    private float timeNoDamageMax = 1f;
    //====默认朝向====
    private Vector2 lookWhere = new Vector2(1, 0);
    //====道具数量====
    int prop1Conut = 0; //道具1：遗照
    public int Prop1Conut { get { return prop1Conut; } }

    //======函数=======
    void Start()
    {
        nowHealth = maxHealth;
        ridy = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        //====死亡====
        if(nowHealth <= 0)
        {
            SoundHelper.Dead();
            deadMenu.SetActive(true);
            Destroy(this);
        }
        //====看的方向和移动方向====
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveWhere = new Vector2(moveX, moveY);
        if(moveWhere.x != 0 || moveWhere.y != 0) lookWhere = moveWhere;

        //====移动====
        Vector2 pos = ridy.position;
        pos.x += moveX * speed * Time.deltaTime;
        pos.y += moveY * speed * Time.deltaTime;
        ridy.MovePosition(pos);
        //====血量条====
        UIhealthyManaer.instance.UpdateHealthBar(maxHealth, nowHealth);

        //====交互====
        Echicken();

        //====无敌时间减去====
        if (noDamage)
        {
            if (timeNoDamage <= 0) noDamage = false;
            else if (timeNoDamage > 0) timeNoDamage = timeNoDamage - Time.deltaTime;
        }
        //====道具====
        Prop();

    }

    //血量变化
    public void ChangeHealth(int changeHealth)
    {
        if (noDamage && changeHealth <= 1)
        {
            Debug.Log("无敌");
            return;
        }
        timeNoDamage = timeNoDamageMax;
        noDamage = true;
        nowHealth = Mathf.Clamp(changeHealth + nowHealth, 0, maxHealth);
        Debug.Log(nowHealth + "和" + maxHealth);
    }

    //E交互
    void Echicken()
    {
        if (Input.GetButtonDown("jiaohu"))
        {
            RaycastHit2D hitnpc = Physics2D.Raycast(ridy.position, lookWhere, 10f, LayerMask.GetMask("CanNPC"));
            if (hitnpc.collider != null)
            {
                //厕所交互
                HiderPlace wc = hitnpc.collider.GetComponent<HiderPlace>();
                if (wc != null)
                {
                    PlayDisable.instance.playIsDisable = true;
                    Debug.Log("设置玩家已经禁用状态...");
                    SoundHelper.EnterToilet();
                    this.gameObject.SetActive(false);
                }

                //交互在此添加

            }
        }
    }
    
    public void SetProp(int whatToAdd,int howMuch)
    {
        if(whatToAdd == 1)
        {
            prop1Conut += howMuch;
        }
    }

    void Prop()
    {
        
    }

}
