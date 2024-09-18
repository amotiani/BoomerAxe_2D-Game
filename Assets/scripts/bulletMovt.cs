using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletMovt : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int damage;
    public float bulletDie;
    public GameObject blood;
    public GameObject pistolPivot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pistolPivot = GameObject.FindWithTag("pistol pivot");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 forceToAdd = new Vector2(speed, 0);

        Invoke("DestroyBullet", bulletDie);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            DestroyBullet();
            Instantiate(blood, transform.position, Quaternion.identity);
            if (collision.gameObject.GetComponent<Enemy>())
            {
                collision.gameObject.GetComponent<Enemy>().health -= damage;
                if (collision.gameObject.GetComponent<Enemy>().health <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
        }

        if (collision.gameObject.CompareTag("brittle"))
        {
            collision.gameObject.GetComponent<brittleWall>().wallHealth -= damage;
            if (collision.gameObject.GetComponent<brittleWall>().wallHealth <= 0)
            {
                Destroy(collision.gameObject);
                DestroyBullet();
            }
        }

        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("enemy patrol trigger") && !collision.gameObject.CompareTag("trigger1")) 
        {
            Destroy(gameObject);
        }
    }
}
