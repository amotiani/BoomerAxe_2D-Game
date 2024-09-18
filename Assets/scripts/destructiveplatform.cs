using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructiveplatform : MonoBehaviour
{
    BoxCollider2D bc;
    SpriteRenderer sp;
    public bool touch = false;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (touch)
        {
            Invoke("destroy", 0.8f);
            playerRespawn.respawned = false;
        }
        if(playerRespawn.respawned==true && !touch )
        {
            bc.enabled = true;
            sp.enabled = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touch = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touch = false;
        }
    }

    void destroy()
    {
        bc.enabled = false;
        sp.enabled = false;
    }
}
