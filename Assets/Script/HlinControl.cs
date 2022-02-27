using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class HlinControl : MonoBehaviour
{
	public static HlinControl instance { get; private set; }
	public Transform player;
	public Transform hlHome;
	Transform target;
	IAstarAI ai;

	public void setTarget(int v)
    {
        if (v == 1) target = player;
        if (v == 2) target = hlHome;
		Debug.Log(v);
    }
	
	void Start()
    {
		instance = this;
		ai = GetComponent<IAstarAI>();
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
		ai.destination = target.position;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        PlayerCollect pc = other.gameObject.GetComponent<PlayerCollect>();
        if (pc == null) return;
        
    }

    /// <summary>
    /// AI²¿·Ö
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

