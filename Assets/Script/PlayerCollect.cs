using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollect : MonoBehaviour
{
    public static PlayerCollect instance { get; private set; }
    Rigidbody2D ridy;
    Animator anim;

    float agospeed;
    
    [Min(0)]
    public float speed;
    //====游戏对象
    public GameObject deadMenu;
    public GameObject gun;
    public Slider tiliBar;
    //====设置血量====
    public int maxHealth = 20;
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
    Vector2 moveWhere;
    //====攻击冷却====
    float attackcold;
    public float maxattackcold;
    //====体力值====
    [Min(1)]
    public int maxTiliValue;
    int TiliValue;


    //====道具数量====
    int prop1Conut = 0; //道具1：遗照
    public int Prop1Conut { get => prop1Conut; set => prop1Conut = value; }
    int prop2Conut = 0; //道具2：个人信息
    public int Prop2Conut { get => prop2Conut; set => prop2Conut = value; }
    int prop3Conut = 0; //道具3：秋键
    public int Prop3Conut { get => prop3Conut; set => prop3Conut = value; }
    int prop4Conut = 0; //道具4：我是lbr
    public int Prop4Conut { get => prop4Conut; set => prop4Conut = value; }
    //====物品数量====
    int item1Conut = 0; //物品1：大厕头水
    public int Item1Conut { get => item1Conut; set => item1Conut = value; }
    int item2Conut = 0; //物品2：回收站
    public int Item2Conut { get => item2Conut; set => item2Conut = value; }

    //======函数=======
    void Start()
    {
        nowHealth = maxHealth;
        ridy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agospeed = speed;
        attackcold = maxattackcold;
        TiliValue = maxTiliValue;
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
            Destroy(gameObject);
        }
        //====看的方向和移动方向====
       
        if (moveWhere.x != 0 || moveWhere.y != 0) 
        { 
            lookWhere = moveWhere;
            anim.SetFloat("moveX", lookWhere.x);
            anim.SetFloat("moveY", lookWhere.y);
        }

        //====移动====
        Vector2 pos = ridy.position;
        pos.x += moveWhere.x * speed * Time.deltaTime;
        pos.y += moveWhere.y * speed * Time.deltaTime;
        ridy.MovePosition(pos);

        //====血量条====
        UIhealthyManaer.instance.UpdateHealthBar(maxHealth, nowHealth);
        //体力条
        tiliBar.value = (float)TiliValue / (float)maxTiliValue;

        //====无敌时间减去====
        if (noDamage)
        {
            if (timeNoDamage <= 0) noDamage = false;
            else if (timeNoDamage > 0) timeNoDamage = timeNoDamage - Time.deltaTime;
        }

        //====攻击冷却====
        if(attackcold > 0)
        {
            attackcold -= Time.deltaTime;  
        }
        
        //====点击q砍树====
        if (Keyboard.current.qKey.wasPressedThisFrame) 
        {
            Attack();
        }

        //====点击地图上的东西获取道具====
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, 0.4f, LayerMask.GetMask("PropItem"));
            if (hitinfo.collider != null)
            {
                hitinfo.transform.gameObject.SendMessage("ClickThis");
            }
        }
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
        if(changeHealth < 0)
        {
            UIManager.main.ShowWarnPanel();
        }
        Debug.Log(nowHealth + "和" + maxHealth);
    }
    //进入boss战设置
    public void StartToBossWar()
    {
        maxHealth *= 2;
        nowHealth *= 2;
    }
    //WASD移动
    void OnMove(InputValue value)
    {
        Debug.Log("WASD按下");
        moveWhere = value.Get<Vector2>();
    }

    void OnJiaohu()
    {
        Echicken();
    }

    public void Attack()
    {
        if (attackcold > 0) { return; }
        RaycastHit2D hit = Physics2D.Raycast(ridy.position, lookWhere, 3f, LayerMask.GetMask("CanNPC"));
        anim.SetTrigger("Attack");
        if (hit.collider != null)
        {
            TreeControl tc;
            if (tc = hit.collider.GetComponent<TreeControl>())
            {
                tc.DestroyTreeByClick();
            }
        }
        RaycastHit2D hitenemy = Physics2D.Raycast(ridy.position, lookWhere, 3f, LayerMask.GetMask("EnemyHL"));
        if (hitenemy.collider != null)
        {
            HlinControl hl;
            if (hl = hit.collider.GetComponent<HlinControl>())
            {
                hl.DamageHL(20);
                if (CenterCtrl.instance.isBossWar)
                {
                    hl.DamageHL(40);
                }
            }
        }
        attackcold = maxattackcold;
    }

    public void OnSprint()
    {
        if (TiliValue > 0)
        {
            speed += 7;
            TiliValue--;
            Invoke("ResetSpeed", .3f);
            Invoke("ReChanSprint", 6);
        }
        else
        {
            SoundHelper.Beep();
        }
    }
    void ReChanSprint()
    {
        if(TiliValue >= maxTiliValue) { return; }
        TiliValue++;
    }
    //E交互
    public void Echicken()
    {
        RaycastHit2D hitnpc = Physics2D.Raycast(ridy.position, lookWhere, 2f, LayerMask.GetMask("CanNPC"));
        if (hitnpc.collider != null)
        {
            //厕所交互
            HiderPlace wc = hitnpc.collider.GetComponent<HiderPlace>();
            if (wc != null)
            {
                if (wc.IsT)
                {
                    wc.TryToHideWC();
                }

                if (wc.IsP)
                {
                    wc.TryToHideParkLot();
                }
            }
            //传送门交互
            Teleport tp = hitnpc.collider.GetComponent<Teleport>();
            if(tp!=null)
            {
                tp.TeleprotTo();
            }
            //Add here
        }
    }

    //public void SetProp(int whatToAdd, int howMuch)
    //{
    //    Debug.LogWarning("弃用的函数！");
    //    if(whatToAdd == 1)
    //    {
    //        prop1Conut += howMuch;
    //    }
    //    if(whatToAdd == 2)
    //    {
    //        prop2Conut += howMuch;
    //    }
    //    if (whatToAdd == 3)
    //    {
    //        prop3Conut += howMuch;
    //    }
    //    if (whatToAdd == 4)
    //    {
    //        prop4Conut += howMuch;
    //    }
    //    if (whatToAdd == 21)
    //    {
    //        item1Conut += howMuch;
    //    }
    //    if (whatToAdd == 22)
    //    {
    //        item1Conut += howMuch;
    //    }
    //}

    public void AddSpeed()
    {
        speed += 10;
        Invoke("ResetSpeed", 1.4f);
    } 
    void ResetSpeed() { speed = agospeed; }

    public void OpenGun()
    {
        gun.SetActive(true);
    } 
}
