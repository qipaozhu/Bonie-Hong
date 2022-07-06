using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(ParticleSystem))]
public class HlinControl : MonoBehaviour
{
	public static HlinControl instance { get; private set; }

    //����
    public GameObject hlTexture; //��ʾ��
    //λ��
	Transform player;
	Transform hlHome;
    Transform hlNotFound;
    GameObject[] firstToFind;

    //Ѫ��
    float health = 50;
    public TextMesh hlHealthText;

    //��ȴʱ��
    float time = 6;
    [HideInInspector]
    public bool timeOver = false;

    //����ٶ�
    public int speed;

    //��Ƶ
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
        //������ҪѰ�ҵ����壨��ʼ����
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hlHome = GameObject.FindGameObjectWithTag("LHome").transform;
        
        hlNotFound = GameObject.FindGameObjectWithTag("GameController").transform;

        target = player;
        ai.maxSpeed = speed;
    }

    void Update()
    {
        if (health <= 0) //��ûѪʱ
        {
            SoundHelper.July5();
            Destroy(gameObject);
        }
        //Ѫ����
        string hetext = "";
        while (hetext.Length - 1 < health)
        {
            hetext += "*";
        }
        hlHealthText.text = hetext;

        if (target != null) { ai.destination = target.position; }//����AIĿ��Ϊ���õ�Ŀ��λ��

        //====����������С====
        ads.volume = AllSceneSetting.instance.EffectSound;

        //��ȴ
        if (!timeOver) time = time - Time.deltaTime;
        if (time < 0)
        {
            timeOver = true;
            time = 3;
        }

        //���û���ҵ���Һ���ȴ����
        if (target == hlNotFound && timeOver)
        {
            ads.PlayOneShot(notFound);
            timeOver = false;
        }
        firstToFind = GameObject.FindGameObjectsWithTag("HFirstFind");
        //�����û
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

    //����Ŀ��
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

    //�����ٶ�
    public void SetSpeed()
    {
        ai.maxSpeed = 1;
        ads.PlayOneShot(cantGo);
        Invoke("ResetSpeed", 5);
    }
    void ResetSpeed() { ai.maxSpeed = speed; }

    //Ѫ��
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
    /// ====================AI����==========================
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

