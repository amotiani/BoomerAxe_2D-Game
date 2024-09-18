using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class pistolAim : MonoBehaviour
{
    public bool axeClick = false;
    public Vector3 difference;
    public GameObject myPlayer;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Update()
    {
        if (weaponWheelController.weaponWheelSelected == false && pistolWeapon.pistolClick == true)
        {
            GetComponent<SpriteRenderer>().enabled = true;

            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();// makes value bw 0 and 1

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            if (myPlayer.transform.rotation == Quaternion.Euler(0f, -180f, 0f))
            {
                GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipY = false;
            }

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
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

