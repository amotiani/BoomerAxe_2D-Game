using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Rendering;

public class movt : MonoBehaviour
{
    public static bool canAxeThrow;
    public float fallTime = 0.2f;
    public float FallTimer = 0.2f;
    public float maxFallSpeed;
    public PlatformEffector2D effector;
    [SerializeField] float Speed;
    public Rigidbody2D body;
    public static bool isGrounded = false;
    public static bool isOnPlatform;
    public bool justjumped=false;
    public bool canjump = true;
    public float jumpTime;
    public float jumpTimer;
    public Animator anim;
    public Transform groundCheck;
    public Transform groundCheck2;
    public float radius;
    public float distance;
    public LayerMask ladder;
    public LayerMask groundlayer;
    private RaycastHit2D hitInfo;
    public bool isClimbing;
    public PolygonCollider2D polyPlayer;
    public BoxCollider2D boxPlayer;
    [SerializeField]
    public float topSpeed;
    public float movtForce;
    public float movtForceOld;
    private Vector2 velocity;
    [Range(0,1)]public float friction;
    public float defaultScale;
    public float fallScale;
    [SerializeField] public float runSpeedTimer;
    public float jumpForce;
    public float jumpForceOld;
    public float slidespeed;
    public float slideTimer;
    public float slideTime;
    public GameObject slopeCheck;
    public float slopeCheckDistance;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;
    public static bool isOnSlope;
    [SerializeField]
    private PhysicsMaterial2D nofriction;
    [SerializeField]
    private PhysicsMaterial2D fullfriction;
    private Vector2 newvelocity;
    public LayerMask slopelayer;
    public float leftSlopeForce;
    public float rightSlopeForce;
    private float oldGravity;
    public float ladderForce;
    void Awake()
    {
        oldGravity = body.gravityScale;
        boxPlayer = GetComponent<BoxCollider2D>();
        polyPlayer = GetComponent<PolygonCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpForce = jumpForceOld;
        justjumped = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //movt physics:

        //add force
        if (weaponWheelController.weaponWheelSelected == false)
        {
            if (isOnSlope && transform.rotation == Quaternion.Euler(0f, 0f, 0f))
            {
                movtForce *= rightSlopeForce;
            }
            else if(isOnSlope && transform.rotation == Quaternion.Euler(0f, 180f, 0f))
            {
                movtForce *= leftSlopeForce;
            }
            else
            {
                movtForce = movtForceOld;
            }
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Vector2 forceToAdd = new Vector2(movtForce * horizontalInput, 0);
            body.AddForce(forceToAdd * (Time.deltaTime * 5), ForceMode2D.Impulse);
            anim.SetFloat("speed", Mathf.Abs(horizontalInput));

            //IF RUN, NO AXE.
            if (horizontalInput == 0)
            {
                canAxeThrow = true;
            }
            else
            {
                canAxeThrow = false;
            }
            
            //clamp velocity
            if (body.velocity.x > topSpeed)// && anim.GetBool("preslide")==false)
            {
                body.velocity = new Vector2(topSpeed, body.velocity.y);
            }
            else if (body.velocity.x < -topSpeed)//&& anim.GetBool("preslide") == false)
            {
                body.velocity = new Vector2(-topSpeed, body.velocity.y);
            }
            

            //add friction
            if (horizontalInput == 0)
            {
                velocity = body.velocity;
                velocity.x *= friction;
                body.velocity = velocity;
            }

            //flip player
            if (horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (horizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }

    public void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //jumpForce = (Mathf.Sqrt(jumpHeight*(Physics2D.gravity.y*body.gravityScale)*-2))*body.mass; // ------> to fix the jump height, formula.
        if (weaponWheelController.weaponWheelSelected == false)
        {
            //jump:
            isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, radius, groundlayer) || Physics2D.OverlapCircle(groundCheck2.transform.position, radius, groundlayer);

            if (Input.GetKeyDown(KeyCode.Space) && canjump && !justjumped)
            {
                body.AddForce(Vector2.up * jumpForce * 14, ForceMode2D.Impulse);

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                velocity = body.velocity;
                velocity.y *= 0.5f;
                body.velocity = velocity;
            }
            if (isGrounded || isOnSlope || isOnPlatform)
            {
                jumpTimer = jumpTime;
                canjump = true;
                justjumped = false;
            }

            if (!isGrounded && !justjumped)
            {
                jumpTimer-=Time.deltaTime;
            }
            if(!isGrounded && !isOnPlatform && justjumped)
            {
                canjump = false;
            }
            if (jumpTimer<=0 || justjumped)
            {
                justjumped = true;
                canjump = false;
            }

            //jumpTimer-=Time.deltaTime;

            //jump buffer and ledge detection.


            //clamp fall velocity
            if (body.velocity.y > maxFallSpeed)
            {
                body.velocity = new Vector2(body.velocity.x, maxFallSpeed);
            }
            else if (body.velocity.y < -maxFallSpeed)
            {
                body.velocity = new Vector2(body.velocity.x, -maxFallSpeed);
            }

            //gravity change with jump and fall:
            if (body.velocity.y > 0.2 && !isGrounded && !isOnSlope)
            {
                body.gravityScale = defaultScale;
                anim.SetBool("isjumping", true);
                anim.SetBool("isfalling", false);
                anim.SetBool("isgrounded",false);
            }
            else if (body.velocity.y < 0.1 && !isGrounded)
            {
                body.gravityScale = fallScale;
                anim.SetBool("isjumping", false);
                anim.SetBool("isfalling", true);
                anim.SetBool("isgrounded", false);
                movtForce *= 0.5f;
            }
            if (isGrounded || isOnSlope)
            {
                anim.SetBool("isjumping", false);
                anim.SetBool("isfalling", false);
                anim.SetBool("isgrounded", true);
            }
            else
            {
                anim.SetBool("isgrounded", false);
            }

            /*//slide:
            Vector2 slideForce = transform.right * slidespeed;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("preslide", true);
            }
            if ( anim.GetBool("preslide") == true)
            {
                body.AddForce(slideForce, ForceMode2D.Impulse);
                friction = 0.15f;
            }


            if (anim.GetBool("preslide") == true)
            {
                slideTimer -= Time.deltaTime*5;
            }
            if (slideTimer <= 0)
            {
                anim.SetBool("preslide", false);
                slideTimer = slideTime;
            }
            if (anim.GetBool("preslide") == false)
            {
                friction = 0f;
            }*/


            //ladder:
            if (isClimbing)
            {
                anim.SetBool("isOnLadder", true);
                body.gravityScale = 0f;
                Vector2 verticalLadderForce = new Vector2(0, ladderForce);
                body.AddForce(verticalLadderForce * (Time.deltaTime *5), ForceMode2D.Impulse);
                movtForce = 0;
                jumpForce = 0;
            }
            else
            {
                movtForce = movtForceOld;
                jumpForce = jumpForceOld;
            }

            //slopeCheck:
            RaycastHit2D hit = Physics2D.Raycast(slopeCheck.transform.position, Vector2.down, slopeCheckDistance, groundlayer);
            if (hit)
            {
                slopeNormalPerp = Vector2.Perpendicular(hit.normal);
                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
                Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }

            isOnSlope = Physics2D.OverlapCircle(groundCheck.transform.position, radius, slopelayer) || Physics2D.OverlapCircle(groundCheck2.transform.position, radius, slopelayer);


            if (isGrounded && isOnSlope)
            {
                newvelocity.Set(movtForce*slopeNormalPerp.x*-horizontalInput, movtForce*slopeNormalPerp.y*-horizontalInput);
                body.velocity = newvelocity;
            }

            if(isOnSlope && horizontalInput == 0)
            {
                body.sharedMaterial = fullfriction;
            }
            else
            {
                body.sharedMaterial = nofriction;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (weaponWheelController.weaponWheelSelected == false)
        {
            if (collision.gameObject.CompareTag("ladder") && Input.GetKeyDown(KeyCode.S))
            {
                effector.surfaceArc = 0;
            }
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            isOnPlatform = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (weaponWheelController.weaponWheelSelected == false)
        {
            if (collision.gameObject.CompareTag("ladder") && Input.GetKeyDown(KeyCode.S))
            {
                effector.surfaceArc = 0;
            }
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (weaponWheelController.weaponWheelSelected == false)
        {
            if (collision.gameObject.CompareTag("ladder"))
            {
                effector.surfaceArc = 180;
            }
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            isOnPlatform = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladder"))
        {
            body.gravityScale = 0f;
            isClimbing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ladder"))
        {
            body.gravityScale = oldGravity;
            isClimbing = false;
            anim.SetBool("isOnLadder", false);
        }
    }
}
