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
    float health = 1000;

    //��ȴʱ��
    float time = 6;
    [HideInInspector]
    public bool timeOver = false;

    [Header("������������")]
    //����ٶ�
    public int speed;
    public float distanceToDestroyHider;

    //��Ƶ
    static AudioSource ads;
    AudioClip notFound;
    static AudioClip isFound;
    AudioClip cantGo;
    AudioClip hurt;

    Transform target;
	IAstarAI ai;
    ParticleSystem ps;

    //�����Ƿ��ͷ�
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

        if (target != null) { ai.destination = target.position; }//����AIĿ��Ϊ���õ�Ŀ��λ��

        //====����������С====
        ads.volume = AllSceneSetting.instance.EffectSound;

        //��Ч��ȴ����
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

        //����������һ���ҵ�
        firstToFind = GameObject.FindGameObjectsWithTag("HFirstFind");

        //Ŀ���Զ�ѡ��
        FindTarget();
    }

    //����Ŀ��
    bool isSawPlayer = false;
    public bool isSawPlayerToHide { get => isSawPlayer; }
    
    public void WasDestroyHider()
    {
        isSawPlayer = false;
        UIManager.main.HenrySawOut();
    }
    void FindTarget()
    {
        if (CenterCtrl.instance.isHCM)
        {
            bool isDisalbe = PlayDisable.instance.playIsDisable; //����������״̬
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
                if (firstToFind.Length >= 1) { target = firstToFind[0].transform; }//���������һ�����ҵ�
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
        }//�����û��

        else if (CenterCtrl.instance.isBossWar)
        {

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

    //�����ٶ�
    public void SetSpeed()
    {
        ai.maxSpeed = 1;
        ads.PlayOneShot(cantGo);
        Invoke("ResetSpeed", 5);
    }
    void ResetSpeed() { ai.maxSpeed = speed; }

    //�˺�Ѫ��
    public void DamageHL(float healthy)
    {
        health -= healthy;
        hlTexture.GetComponent<SpriteRenderer>().color = Color.red;
        ps.Play();
        ads.PlayOneShot(hurt);
        Invoke("ResetColor", .05f);
    }
    void ResetColor()
    {
        ps.Stop();
        hlTexture.GetComponent<SpriteRenderer>().color = Color.white;
    }


    public void StartToBossWar()
    {
        health *= 0.4f;
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

