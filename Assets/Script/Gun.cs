using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public GameObject bullet;
    public Transform muzzle;

    Vector3 mousePos;
    Vector2 gunDireon;

    
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gunDireon = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(gunDireon.y, gunDireon.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, -0.3f, angle);

        if (Input.GetMouseButtonDown(1))
        {
            SoundHelper.Fire();
            Debug.Log("°´ÏÂ");
            Instantiate(bullet, muzzle.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
