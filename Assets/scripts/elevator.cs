using UnityEngine;
using UnityEngine.Timeline;

public class ElevatorMovement : MonoBehaviour
{
    public Transform topPoint;
    public Transform bottomPoint;
    public GameObject player;
    public float moveSpeed = 20f;
    private bool movingUp = false;
    private bool playerInLift = false;
    public float Timer=1f;
    public float TimeValue=1f;

    private void Update()
    {
        // Move the elevator up or down
        if (movingUp)
        {
            moveSpeed = 0;
            /*player.transform.SetParent(transform);
            transform.position = Vector3.MoveTowards(transform.position, topPoint.position, moveSpeed * Time.deltaTime);

            if (transform.position == topPoint.position)
                movingUp = false;*/
        }
        else if (!movingUp && playerInLift)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0)
            {
                player.transform.SetParent(transform);
                transform.position = Vector3.MoveTowards(transform.position, bottomPoint.position, moveSpeed * Time.deltaTime);

                if (transform.position == bottomPoint.position)
                {
                    movingUp = true;
                }
            }
        }
        else if (!movingUp && !playerInLift)
        {
            player.transform.SetParent(null);
        }
        else if (movingUp && !playerInLift)
        {
            player.transform.SetParent(null);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInLift = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInLift = false;
        }
    }
}