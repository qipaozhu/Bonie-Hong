using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HkillAI : MonoBehaviour
{
    bool canPlay;
    float time;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<SoundHelper>() != null)
        {

        } 
    }
    private void FixedUpdate()
    {
        if (!canPlay) { 
            time = time - Time.deltaTime;
            if(time ==)
        }
        
    }

}
