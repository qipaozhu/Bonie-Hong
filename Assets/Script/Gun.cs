using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;

    [Header("枪口")]
    public Transform muzzle;
    [Header("枪口冷却时间")]
    public int waitTimeNoGun;

    Vector3 mousePos;
    Vector2 gunDireon;

    void Start()
    {
        StopCoroutine(GunAngle());
    }

    //计算枪的角度
    IEnumerator GunAngle()
    {
        while (true)
        {
            yield return null;
            mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            gunDireon = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(gunDireon.y, gunDireon.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, -0.3f, angle);
        }
    }

    /// <summary>
    /// 特定时间内没收枪
    /// </summary>
    /// <returns></returns>
    IEnumerator GunDisable()
    {
        yield return new WaitForSeconds(waitTimeNoGun);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(GunAngle());
    }
    private void OnEnable()
    {
        StartCoroutine(GunAngle());
        StartCoroutine(GunDisable());
    }

    //当发射时
    void OnFire()
    {
        if (PlayDisable.instance.playIsDisable) { return; }
        SoundHelper.Fire();
        Debug.Log("按下");
        Instantiate(bullet, muzzle.position, Quaternion.Euler(transform.eulerAngles));
        PlayerCollect.instance.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(gunDireon * -4000, mousePos); //后坐力
    }
}
