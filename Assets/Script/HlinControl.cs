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

    Transform target;
	IAstarAI ai;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
		ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
		ai.destination = target.position;

        if (CenterCtrl.instance.isHCM)
        {
            bool isDisalbe = PlayDisable.instance.playIsDisable;
            if (isDisalbe) SetTarget(3);
            if (!isDisalbe) SetTarget(1);
        }
    }

    public void SetTarget(int v)
    {
        if (v == 1) target = player;
        if (v == 2) target = hlHome;
        if (v == 3) target = hlNotFound;
        Debug.Log("目标人物:" + v);
    }

    /// <summary>
    /// AI部分
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

