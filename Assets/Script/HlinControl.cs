using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HlinControl : MonoBehaviour
{
	public static HlinControl instance { get; private set; }

	public Transform player;
	public Transform hlHome;
    public Transform hlNotFound;

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
        target = player.transform;
        ai.maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
		ai.destination = target.position;

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
        if (v == 1) target = player;
        if (v == 2) target = hlHome;
        if (v == 3) target = hlNotFound;
    }

    //出没设置
    /*public void CMControl(int what)
    {
        if(what == 1)
        {
            SetTarget(1);
        }
        if(what == 2)
        {
            SetTarget(2);
        }
    }*/

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

