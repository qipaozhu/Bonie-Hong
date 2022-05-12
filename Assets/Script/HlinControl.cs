using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HlinControl : MonoBehaviour
{
	public static HlinControl instance { get; private set; }

	Transform player;
	Transform hlHome;
    Transform hlNotFound;
    GameObject[] firstToFind;
    float health = 50;
    public TextMesh hlHealthText;

    float time = 6;
    public bool timeOver = false;

    public int speed;
    static AudioSource ads;
    static AudioClip notFound;
    static AudioClip isFound;
    static AudioClip cantGo;

    Transform target;
	IAstarAI ai;

    private void Awake()
    {
        instance = this;
        ads = GetComponent<AudioSource>();
        notFound = Resources.Load<AudioClip>("WherePeople");
        isFound = Resources.Load<AudioClip>("TooEasy");
        cantGo = Resources.Load<AudioClip>("BuyWH");
        ai = GetComponent<IAstarAI>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hlHome = GameObject.FindGameObjectWithTag("LHome").transform;
        
        hlNotFound = GameObject.Find("CenterCtrl").transform;

        target = player;
        ai.maxSpeed = speed;
    }

    void Update()
    {
        if (health <= 0) //当没血时
        {
            SoundHelper.July5();
            Destroy(gameObject);
        }
        hlHealthText.text = health.ToString();

        if (target != null) { ai.destination = target.position; }//设置AI目标为设置的目标位置

        //====声音====
        if (!timeOver) time = time - Time.deltaTime;
        if (time < 0)
        {
            timeOver = true;
            time = 3;
        }

        //如果没有找到玩家和冷却到了
        if (target == hlNotFound && timeOver)
        {
            ads.PlayOneShot(notFound);
            timeOver = false;
        }
        firstToFind = GameObject.FindGameObjectsWithTag("HFirstFind");
        //如果出没
        if (CenterCtrl.instance.isHCM)
        {
            bool isDisalbe = PlayDisable.instance.playIsDisable;
            if (isDisalbe) SetTarget(3);
            if (!isDisalbe) SetTarget(1);
        }
        if (!CenterCtrl.instance.isHCM)
        {
            SetTarget(2);
        }
    }

    //设置目标
    void SetTarget(int v)
    {
        if (v == 1)
        {
            if (firstToFind.Length >= 1)
            {
                target = firstToFind[0].transform;
            }
            else
            {
                target = player;
            }
        }
        if (v == 2) target = hlHome;
        if (v == 3) target = hlNotFound;
    }
    
    
    public static void Found() 
    {
        ads.PlayOneShot(isFound);
    }

    //设置速度
    public void SetSpeed()
    {
        ai.maxSpeed = 1;
        ads.PlayOneShot(cantGo);
        Invoke("ResetSpeed", 5);
    }
    void ResetSpeed() { ai.maxSpeed = speed; }

    //血量
    public void DamageHL(float healthy)
    {
        health -= healthy;
    }

    /// <summary>
    /// ====================AI部分==========================
    /// </summary>
    void OnEnable()
	{
		if (ai != null) ai.onSearchPath += Update;
	}

	void OnDisable()
	{
		if (ai != null) ai.onSearchPath -= Update;
	}
}

