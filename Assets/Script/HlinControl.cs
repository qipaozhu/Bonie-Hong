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

    public float time = 6;
    public bool timeOver = false;
    public static AudioSource ads;
    public static AudioClip notFound;
    public static AudioClip isFound;

    Transform target;
	IAstarAI ai;

    private void Awake()
    {
        instance = this;
        ads = GetComponent<AudioSource>();
        notFound = Resources.Load<AudioClip>("WherePeople");
        isFound = Resources.Load<AudioClip>("TooEasy");
        ai = GetComponent<IAstarAI>();
    }

    void Start()
    {
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
		ai.destination = target.position;

        if (!timeOver) time = time - Time.deltaTime;
        if (time < 0)
        {
            timeOver = true;
            time = 6;
        }
        Debug.Log(time);
        Debug.Log(timeOver);

        if (target == hlNotFound && timeOver)
        {
            ads.PlayOneShot(notFound);
            timeOver = false;
        }

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

    void SetTarget(int v)
    {
        if (v == 1) target = player;
        if (v == 2) target = hlHome;
        if (v == 3) target = hlNotFound;
        Debug.Log("目标人物:" + v);
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

