using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public static int damage = 2;
    private Animator anim;
    BoxCollider2D bc;
    public GameObject pivot;
    Rigidbody2D rb;
    public GameObject blood;
    public float speed;
    public float chaseSpeed;
    public GameObject player;
    public static bool isChasing;
    public float chaseDistance;
    public GameObject hitRay;
    public LayerMask playerLayerOnly;
    public bool inbetweenTriggers;
    public GameObject trigger1;
    public GameObject trigger2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //in btw triggers:
        if (transform.position.x >= trigger1.transform.position.x && transform.position.x <= trigger2.transform.position.x)
        {
            inbetweenTriggers=true;
        }
        else
        {
            inbetweenTriggers = false;
        }

        //reaches trigger, then flips..
        rb.velocity = transform.right*speed;
        if (health < 2 && health > 0) 
        {
            anim.SetBool("isHit", true);
        }
        else
        {
            anim.SetBool("isHit", false);
        }
        if (health <= 0)
        {
            anim.SetBool("isHit", false);
            anim.SetBool("isDead", true);
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0) && weaponWheelController.weaponWheelSelected == false && pivot.GetComponent<axeWeapon>().axeClick == true && axeMaaro.canInstantiate == true)
        {
            bc.isTrigger = true;
            rb.gravityScale = 0;
            rb.isKinematic = true;
        }

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, transform.right, chaseDistance, playerLayerOnly);
        

        if (isChasing)
        {
            Debug.Log(rb.velocity);
            Debug.Log("Chase.");
            anim.SetBool("isAttacking", true);
            if(transform.position.x > player.transform.position.x && movt.isGrounded && transform.position.y <= player.transform.position.y)        //enemy is on right.
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                rb.AddForce(Vector3.left*chaseSpeed*(Time.deltaTime*5), ForceMode2D.Impulse);
                chaseSpeed += Time.deltaTime * 100;
            }
            if (transform.position.x < player.transform.position.x && movt.isGrounded && transform.position.y <= player.transform.position.y)        //enemy is on left.
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.AddForce(Vector3.right * chaseSpeed * (Time.deltaTime * 5), ForceMode2D.Impulse);
                chaseSpeed += Time.deltaTime * 100;
            }
        }
        if (transform.position.y > player.transform.position.y)
        {
            if (!inbetweenTriggers)
            {
                if (transform.position.x < trigger1.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    Vector3.MoveTowards(transform.position, trigger1.transform.position, speed * Time.deltaTime);
                }
                if (transform.position.x > trigger2.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    Vector3.MoveTowards(transform.position, trigger2.transform.position, speed * Time.deltaTime);
                }
            }
        }

        if (!isChasing)
        {
            anim.SetBool("isAttacking", false);
        }

        if (hitPlayer.collider.name == "player")
        {
            isChasing = true;
        }

        if (playerhealth.playerHealth <= 0)
        {
            rb.velocity = Vector3.zero;
            isChasing = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy patrol trigger") && !isChasing)
        {
            flip();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("kill");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("kill");
        }
    }

    void flip()
    {
        if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
