using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(ParticleSystem))]
public class HlinControl : MonoBehaviour
{
	public static HlinControl instance { get; private set; }

    //对象
    public GameObject hlTexture; //显示的
    //位置
	Transform player;
	Transform hlHome;
    Transform hlNotFound;
    GameObject[] firstToFind;

    //血量
    float health = 50;
    public TextMesh hlHealthText;

    //冷却时间
    float time = 6;
    [HideInInspector]
    public bool timeOver = false;

    //最大速度
    public int speed;

    //音频
    static AudioSource ads;
    AudioClip notFound;
    static AudioClip isFound;
    AudioClip cantGo;
    AudioClip hurt;

    Transform target;
	IAstarAI ai;
    ParticleSystem ps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ads = GetComponent<AudioSource>();
        notFound = Resources.Load<AudioClip>("WherePeople");
        isFound = Resources.Load<AudioClip>("TooEasy");
        cantGo = Resources.Load<AudioClip>("BuyWH");
        hurt = Resources.Load<AudioClip>("Please");
        ai = GetComponent<IAstarAI>();
        ps = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        //设置需要寻找的物体（初始化）
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hlHome = GameObject.FindGameObjectWithTag("LHome").transform;
        
        hlNotFound = GameObject.FindGameObjectWithTag("GameController").transform;

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
        //血量的
        string hetext = "";
        while (hetext.Length - 1 < health)
        {
            hetext += "*";
        }
        hlHealthText.text = hetext;

        if (target != null) { ai.destination = target.position; }//设置AI目标为设置的目标位置

        //====设置声音大小====
        ads.volume = AllSceneSetting.instance.EffectSound;

        //冷却
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
        else
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
        if (v == 2)
        {
            if (HlinSkill.main.isTuiSkill)
            {
                target = player;
            }
            else
            {
                target = hlHome;
            }
        }
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
        hlTexture.GetComponent<SpriteRenderer>().color = Color.red;
        ps.Play();
        ads.PlayOneShot(hurt);
        Invoke("ResetColor", .2f);
    }
    void ResetColor()
    {
        ps.Stop();
        hlTexture.GetComponent<SpriteRenderer>().color = Color.white;
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

