using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;

    [Header("Ç¹¿Ú")]
    public Transform muzzle;

    Vector3 mousePos;
    Vector2 gunDireon;

    void Start()
    {
        StopCoroutine(GunAngle());
    }

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

    private void OnDisable()
    {
        StopCoroutine(GunAngle());
    }
    private void OnEnable()
    {
        StartCoroutine(GunAngle());
    }

    void OnFire()
    {
        if (PlayDisable.instance.playIsDisable) { return; }
        SoundHelper.Fire();
        Debug.Log("°´ÏÂ");
        Instantiate(bullet, muzzle.position, Quaternion.Euler(transform.eulerAngles));
        PlayerCollect.instance.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(gunDireon * -4000, mousePos);
    }
}
