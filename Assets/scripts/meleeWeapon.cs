using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class meleeWeapon : MonoBehaviour
{
    public Animator anim;
    public Transform attackCirclePos;
    public float attackRadius;
    public LayerMask enemyMask;
    public int damage;
    public static bool canShoot;
    public float timeBtwShots;
    public float resetTime;
    public float moveStep;
    public GameObject wallPieces;
    public GameObject wall;
    public GameObject blood;
    public GameObject enemy;
    public static bool hit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 force = new Vector2(moveStep, 0f);
        //attack1:
        if (weaponWheelController.weaponWheelSelected == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && canShoot)
            {
                if (movt.isGrounded)
                {
                    if (transform.rotation == Quaternion.Euler(0f, -180f, 0f)|| transform.rotation == Quaternion.Euler(0f, 180f, 0f))
                    {
                        gameObject.GetComponent<movt>().body.AddForce(Vector2.left * moveStep);
                    }
                    else if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
                    {
                        gameObject.GetComponent<movt>().body.AddForce(Vector2.right * moveStep);
                    }
                }
                anim.SetBool("attack", true);
                canShoot = false;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCirclePos.position, attackRadius);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].GetComponent<Enemy>())
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().health -= damage;
                    }
                    if (enemiesToDamage[i].GetComponent<brittleWall>())
                    {
                        enemiesToDamage[i].GetComponent<brittleWall>().wallHealth -= damage;
                    }
                }
            }
            else
            {
                anim.SetBool("attack", false);
            }
            if (!canShoot)
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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackCirclePos.position, attackRadius);
    }

    
}
