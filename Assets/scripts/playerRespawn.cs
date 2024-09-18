using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    public float respawnTimer;
    public float respawnTime;
    public GameObject player;
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;
    public GameObject platform4;
    public static bool respawned;
    public static bool platformWapasLao;

    // Update is called once per frame
    void Update()
    {
        if (playerhealth.playerHealth <= 0)
        {
            respawnTimer -= Time.deltaTime;
            axeMaaro.cangoback = false;
        }
        if (respawnTimer <= 0)
        {
            Respawn();
            if(Input.GetMouseButtonDown(1))
            {
                axeMaaro.cangoback = true;
            }
        }
    }

    void Respawn()
    {
        player.transform.position = playerhealth.startPos;
        player.SetActive(true);
        respawnTimer = respawnTime;
        playerhealth.playerHealth = 10;
        respawned = true;
        if (platform1.activeInHierarchy == false)
        {
            platformWapasLao = true;
            platform1.SetActive(true);
            Debug.Log("pt1 resp");
        }
        if (platform2.activeInHierarchy == false)
        {
            platformWapasLao = true;
            platform2.SetActive(true);
            Debug.Log("pt2 resp");
        }
        if (platform3.activeInHierarchy == false)
        {
            platformWapasLao = true;
            platform3.SetActive(true);
            Debug.Log("pt3 resp");
        }
        if (platform4.activeInHierarchy == false)
        {
            platformWapasLao = true;
            platform4.SetActive(true);
            Debug.Log("pt4 resp");
        }
    }
}
