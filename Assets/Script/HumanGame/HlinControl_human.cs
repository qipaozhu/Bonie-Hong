using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HlinControl_human : MonoBehaviour
{
	public static HlinControl_human instance { get; private set; }

	public Transform player;
	public Transform hlHome;

    float health;

    float time = 6;
    public bool timeOver = false;

    public int speed;
    public int exitSpeed;
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
        target = player.transform;
        ai.maxSpeed = speed;
    }

    public void WillEnd()
    {
        ai.maxSpeed = exitSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
		ai.destination = target.position;

        //====声音====
        if (!timeOver) time = time - Time.deltaTime;
        if (time < 0)
        {
            timeOver = true;
            time = 3;
        }

        //如果没有找到玩家和冷却到了
        if (target == hlHome && timeOver)
        {
            ads.PlayOneShot(notFound);
            timeOver = false;
        }

        bool isDisalbe = PlayDisable.instance.playIsDisable;
        if (isDisalbe) SetTarget(2);
        if (!isDisalbe) SetTarget(1);
    }

    //设置目标
    void SetTarget(int v)
    {
        if (v == 1) target = player;
        if (v == 2) target = hlHome;
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
    /// =================AI部分=============================
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

