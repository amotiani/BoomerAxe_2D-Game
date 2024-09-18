using UnityEngine;

public class axeMovt : MonoBehaviour
{
    public Rigidbody2D rb;
    public float degrees;
    public static bool isRotating = false;
    public GameObject blood;
    public Animator animAxe;
    public static int damage=10;
    private Transform axeBack;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(blood, 1f);
            if (collision.gameObject.GetComponent<Enemy>())
            {
                collision.gameObject.GetComponent<Enemy>().health -= damage;
            }
            if (collision.gameObject.GetComponent<Enemy>())
            {
                if (collision.gameObject.GetComponent<Enemy>().health <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("enemy") && !collision.gameObject.CompareTag("brittle") && !collision.gameObject.CompareTag("trigger1") && !collision.gameObject.CompareTag("trigger2"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.gravityScale = 0;
            animAxe.SetBool("isrotating", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("enemy"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.gravityScale = 0;
            animAxe.SetBool("isrotating", false);
        }   
    }
}
