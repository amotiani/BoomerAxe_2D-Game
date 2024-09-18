using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class brittleWall : MonoBehaviour
{
    public int wallHealth;
    public GameObject wallPieces;
    public bool canmake = true;
    BoxCollider2D bc;
    Rigidbody2D rb;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(wallHealth <= 0 && canmake && meleeWeapon.canShoot==true)
        {
            Destroy(gameObject);
            Instantiate(wallPieces, transform.position, Quaternion.identity);
            Destroy(wallPieces);
            canmake = false;
        }
        else if(wallHealth <= 0 && canmake && meleeWeapon.canShoot != true)
        {
            Destroy(gameObject, 0.23f);
            Invoke("makeparticles", 0.2f);
            canmake = false;
        }

        if (Input.GetMouseButtonDown(0) && weaponWheelController.weaponWheelSelected == false && GameObject.FindWithTag("pivot").GetComponent<axeWeapon>().axeClick == true && axeMaaro.canInstantiate == true)
        {
            Debug.Log("istrigger");
            bc.isTrigger = true;
            rb.gravityScale = 0;
            rb.isKinematic = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("axe"))
        {
            wallHealth -= axeMovt.damage;
        }
    }
    void makeparticles()
    {
        Instantiate(wallPieces, transform.position, Quaternion.identity);
    }
}

