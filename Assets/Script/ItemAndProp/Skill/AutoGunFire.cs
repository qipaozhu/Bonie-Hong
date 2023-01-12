using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoGunFire : MonoBehaviour
{
    Vector2 gunDireon;
    AudioSource ads;

    [Header("游戏对象")]
    public GameObject bullet;
    public Transform gunTexture;
    public Transform muzzle;
    [Header("变量")]
    public float maxFireTime;
    float fireTime;

    private void Start()
    {
        fireTime= maxFireTime;
        Invoke("DestroyThis", 30);

        ads = GetComponent<AudioSource>();
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        ads.volume = AllSceneSetting.instance.EffectSound * 0.6f;
        if(HlinControl.instance == null) { return; }
        gunDireon = (HlinControl.instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(gunDireon.y, gunDireon.x) * Mathf.Rad2Deg;
        gunTexture.transform.eulerAngles = new Vector3(0, -0.3f, angle);

        fireTime-= Time.deltaTime;
        if (fireTime <= 0)
        {
            ads.PlayOneShot(Resources.Load<AudioClip>("Fire"));
            fireTime = maxFireTime;
            Instantiate(bullet, muzzle.position, Quaternion.Euler(gunTexture.transform.eulerAngles));
        }
    }
}
