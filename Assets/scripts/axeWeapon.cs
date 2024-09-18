using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class axeWeapon : MonoBehaviour
{
    public GameObject axe;
    public Animator anim;
    public Transform axepos;
    public bool axeClick = false;
    public Vector3 difference;
    public GameObject throwaxefrom;
    public GameObject myPlayer;
    void Update()
    {
        if (weaponWheelController.weaponWheelSelected == false && axeClick == true && axeMaaro.cancallback == false)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();// makes value bw 0 and 1

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            if (rotationZ < -90 && axeMaaro.cancallback==false)
            {
                //facing right:
                if (myPlayer.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90f);
                }
            }

            if (rotationZ > 90 && axeMaaro.cancallback == false)
            {
                //facing right:
                if (myPlayer.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90f);
                }
            }

            if (rotationZ < 90 && rotationZ > 0 && axeMaaro.cancallback == false)
            {
                //facing left:
                if (myPlayer.transform.rotation == Quaternion.Euler(0f, -180f, 0f))
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90f);
                }
            }
            if (rotationZ > -90 && rotationZ < 0 && axeMaaro.cancallback == false)
            {
                //facing left:
                if (myPlayer.transform.rotation == Quaternion.Euler(0f, -180f, 0f))
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90f);
                }
            }
        }
    }
}

