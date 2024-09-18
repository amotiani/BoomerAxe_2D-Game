using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class onewayplatform : MonoBehaviour
{
    public PlatformEffector2D effector;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            effector.surfaceArc = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.S))
        {
            effector.surfaceArc = 0;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.S))
        {
            effector.surfaceArc = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            effector.surfaceArc = 180;
        }
    }
}
