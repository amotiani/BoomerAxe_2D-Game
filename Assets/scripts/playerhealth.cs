using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerhealth : MonoBehaviour
{
    public static float playerHealth=15;
    public ParticleSystem blood;
    
    public Animator anim;
    public static Vector3 startPos;


    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            gameObject.SetActive(false);
            Instantiate(blood, gameObject.transform.position, Quaternion.identity);
            playerHealth = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            playerHealth -= Enemy.damage;
            anim.SetBool("isHit", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            playerHealth -= Time.deltaTime;
            anim.SetBool("isHit", true);
            Invoke("animToFalse", 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            animToFalse();
        }
        
    }

    void animToFalse()
    {
        anim.SetBool("isHit", false);
    }

    
}
