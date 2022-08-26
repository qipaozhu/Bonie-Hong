using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollect : MonoBehaviour
{
    public static PlayerCollect instance { get; private set; }
    Rigidbody2D ridy;
    Animator anim;

    float agospeed;
    
    [Min(0)]
    public float speed;
    //====��Ϸ����
    public GameObject deadMenu;
    public GameObject gun;
    //====����Ѫ��====
    private int maxHealth = 20;
    private int nowHealth = 20;
    //====����Ѫ��====
    public int PnowHealth { get { return nowHealth; } }
    public int PmaxHealth { get { return maxHealth; } }
    //====�޵�ʱ��====
    private bool noDamage = false;
    private float timeNoDamage;
    private float timeNoDamageMax = 1f;
    //====Ĭ�ϳ���====
    private Vector2 lookWhere = new Vector2(1, 0);
    Vector2 moveWhere;


    //====��������====
    int prop1Conut = 0; //����1������
    public int Prop1Conut { get => prop1Conut; set => prop1Conut = value; }
    int prop2Conut = 0; //����2��������Ϣ
    public int Prop2Conut { get => prop2Conut; set => prop2Conut = value; }
    int prop3Conut = 0; //����3�����
    public int Prop3Conut { get => prop3Conut; set => prop3Conut = value; }
    int prop4Conut = 0; //����4������lbr
    public int Prop4Conut { get => prop4Conut; set => prop4Conut = value; }
    //====��Ʒ����====
    int item1Conut = 0; //��Ʒ1�����ͷˮ
    public int Item1Conut { get => item1Conut; set => item1Conut = value; }
    int item2Conut = 0; //��Ʒ2������վ
    public int Item2Conut { get => item2Conut; set => item2Conut = value; }

    //======����=======
    void Start()
    {
        nowHealth = maxHealth;
        ridy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agospeed = speed;
    }

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        //====����====
        if(nowHealth <= 0)
        {
            SoundHelper.Dead();
            deadMenu.SetActive(true);
            Destroy(gameObject);
        }
        //====���ķ�����ƶ�����====
       
        if (moveWhere.x != 0 || moveWhere.y != 0) 
        { 
            lookWhere = moveWhere;
            anim.SetFloat("moveX", lookWhere.x);
            anim.SetFloat("moveY", lookWhere.y);
        }

        //====�ƶ�====
        Vector2 pos = ridy.position;
        pos.x += moveWhere.x * speed * Time.deltaTime;
        pos.y += moveWhere.y * speed * Time.deltaTime;
        ridy.MovePosition(pos);

        //====Ѫ����====
        UIhealthyManaer.instance.UpdateHealthBar(maxHealth, nowHealth);

        //====�޵�ʱ���ȥ====
        if (noDamage)
        {
            if (timeNoDamage <= 0) noDamage = false;
            else if (timeNoDamage > 0) timeNoDamage = timeNoDamage - Time.deltaTime;
        }
    }

    //Ѫ���仯
    public void ChangeHealth(int changeHealth)
    {
        if (noDamage && changeHealth <= 1)
        {
            Debug.Log("�޵�");
            return;
        }
        timeNoDamage = timeNoDamageMax;
        noDamage = true;
        nowHealth = Mathf.Clamp(changeHealth + nowHealth, 0, maxHealth);
        Debug.Log(nowHealth + "��" + maxHealth);
    }

    //WASD�ƶ�
    void OnMove(InputValue value)
    {
        Debug.Log("WASD����");
        moveWhere = value.Get<Vector2>();
    }
    void OnJiaohu()
    {
        Echicken();
    }
    //E����
    public void Echicken()
    {
        RaycastHit2D hitnpc = Physics2D.Raycast(ridy.position, lookWhere, 2f, LayerMask.GetMask("CanNPC"));
        if (hitnpc.collider != null)
        {
            //��������
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
            //Add here
        }
    }
    
    public void SetProp(int whatToAdd,int howMuch)
    {
        Debug.LogWarning("���õĺ�����");
        if(whatToAdd == 1)
        {
            prop1Conut += howMuch;
        }
        if(whatToAdd == 2)
        {
            prop2Conut += howMuch;
        }
        if (whatToAdd == 3)
        {
            prop3Conut += howMuch;
        }
        if (whatToAdd == 4)
        {
            prop4Conut += howMuch;
        }
        if (whatToAdd == 21)
        {
            item1Conut += howMuch;
        }
        if (whatToAdd == 22)
        {
            item1Conut += howMuch;
        }
    }

    public void AddSpeed()
    {
        speed += 8;
        Invoke("ResetSpeed", 1);
    } void ResetSpeed() { speed = agospeed; }

    public void OpenGun()
    {
        gun.SetActive(true);
    } 
}
