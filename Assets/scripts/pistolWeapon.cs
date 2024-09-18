using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class pistolWeapon : MonoBehaviour
{
    public Animator anim;
    public bool canShoot;
    public float timeBtwShots;
    public float resetTime;
    public float speed;
    public Transform bulletPos;
    public GameObject bulletPrefab;
    public float recoil;
    public static bool pistolClick = false;
    public GameObject pistolAim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponWheelController.weaponWheelSelected == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && pistolClick)
            {
                bulletPrefab.GetComponent<TrailRenderer>().endColor = Color.black;
                anim.SetBool("isshooting", true);
                GameObject banayiHuiBullet = Instantiate(bulletPrefab, bulletPos.position, transform.rotation);
                banayiHuiBullet.GetComponent<Rigidbody2D>().AddForce(pistolAim.transform.right * speed);
                if(movt.isGrounded || movt.isOnSlope || movt.isOnPlatform)
                {
                    if (gameObject.transform.rotation.y < 0f)
                    {
                        gameObject.GetComponent<movt>().body.AddForce(-pistolAim.transform.right * recoil);
                    }
                    else if (gameObject.transform.rotation.y == 0f)
                    {
                        gameObject.GetComponent<movt>().body.AddForce(-pistolAim.transform.right * recoil);
                    }
                }
                canShoot = false;
            }
            else
            {
                anim.SetBool("isshooting", false);
            }
            if (canShoot == false)
            {
                timeBtwShots -= Time.deltaTime;
            }
            if (timeBtwShots <= 0)
            {
                canShoot = true;
                timeBtwShots = resetTime;
            }
        }
    }
}
