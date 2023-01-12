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
    float health = 1000;
    [Min(20)]
    public float maxHenryHealth;

    //冷却时间
    float time = 6;
    [HideInInspector]
    public bool timeOver = false;

    [Header("基本参数设置")]
    //最大速度
    public int speed;
    public float distanceToDestroyHider;

    public Animator henryAni;
    Vector2 lastPos;

    //音频
    static AudioSource ads;
    AudioClip notFound;
    static AudioClip isFound;
    AudioClip cantGo;
    AudioClip hurt;

    Transform target;
	IAstarAI ai;
    ParticleSystem ps;

    //技能是否释放
    bool s_isTuiSkill = false;
    public bool isTuiSkill { get => s_isTuiSkill; set => s_isTuiSkill = value; }

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
        health = maxHenryHealth;
    }

    void Update()
    {
        if (health <= 0) //当没血时
        {
            SoundHelper.July5();
            Destroy(gameObject);
        }
        if (CenterCtrl.instance.isBossWar)
        {
            UIManager.main.SetBossHealthValue(health / maxHenryHealth);
        }
        if (target != null) { ai.destination = target.position; }//设置AI目标为设置的目标位置

        //====设置声音大小====
        ads.volume = AllSceneSetting.instance.EffectSound;

        //音效冷却功能
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

        //先配置泓第一个找的
        firstToFind = GameObject.FindGameObjectsWithTag("HFirstFind");

        //目标自动选择
        FindTarget();

        //判断向右走还是左
        Vector2 nowPos = transform.position;
        if(nowPos.x - lastPos.x != 0)
        {
            if(nowPos.x < lastPos.x) henryAni.SetFloat("x",0);
            else henryAni.SetFloat("x", 1);
        }
        lastPos = nowPos;
    }

    //设置目标
    bool isSawPlayer = false;
    public bool isSawPlayerToHide { get => isSawPlayer; }
    
    public void WasDestroyHider()
    {
        isSawPlayer = false;
        UIManager.main.HenrySawOut();
        Toast.instance.HaveNotice("厕所耐久-1");
    }
    void FindTarget()
    {
        if (CenterCtrl.instance.isHCM)
        {
            bool isDisalbe = PlayDisable.instance.playIsDisable; //检测玩家隐藏状态
            if (isDisalbe)
            {
                if (isSawPlayer)
                {
                    target = PlayDisable.instance.lastPlayerHider.transform;
                }
                else { target = hlNotFound; }
            }
            else
            {
                if (firstToFind.Length >= 1) { target = firstToFind[0].transform; }//如果有泓第一个先找的
                else
                {
                    target = player;
                    if (player != null)
                    {
                        float offset = (player.position - transform.position).sqrMagnitude;
                        if (offset <= distanceToDestroyHider * distanceToDestroyHider)
                        {
                            isSawPlayer = true;
                            UIManager.main.SetHenrySaw();
                        }
                        else
                        {
                            isSawPlayer = false;
                            UIManager.main.HenrySawOut();
                        }
                    }
                }
            }
        }//如果出没了

        else if (CenterCtrl.instance.isBossWar)
        {
            if (firstToFind.Length >= 1) { target = firstToFind[0].transform; }//如果有泓第一个先找的
            else
            {
                target = player;
            }
        }
        else
        {
            isSawPlayer = false;
            if (s_isTuiSkill)
            {
                if (PlayDisable.instance.playIsDisable) { target = hlHome; }
                else { target = player; }
            }
            else
            {
                target = hlHome;
            }
        }
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

    //伤害血量
    public void DamageHL(float healthy)
    {
        health -= healthy;
        hlTexture.GetComponent<SpriteRenderer>().color = Color.red;
        ps.Play();
        if (!ads.isPlaying)
        {
            ads.PlayOneShot(hurt);
        }
        Invoke("ResetColor", .05f);
    }
    void ResetColor()
    {
        ps.Stop();
        hlTexture.GetComponent<SpriteRenderer>().color = Color.white;
    }

    //进入boss战设置
    public void StartToBossWar()
    {
        health *= 0.8f;
        maxHenryHealth *= 0.8f;
        speed = 5;
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

