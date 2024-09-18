using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{
    Camera maincam;
    public CinemachineVirtualCamera cam;
    public GameObject camPos;
    public float increment;
    public float moveForce;
    public bool ZoomIn;
    public GameObject player;
    Vector3 target; 
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        maincam = Camera.main;
        target = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
    private void Update()
    {
        if (ZoomIn)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, 154f, Time.deltaTime*3f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, Time.deltaTime * 3f);
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY, 0.77f, Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trigger1"))
        {
            ZoomIn=false;
            //if (cam.m_Lens.OrthographicSize >= 200f) { cam.Follow = null; }
            if (movt.isOnPlatform)
            {
                cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY, 0.45f, Time.deltaTime);
            }

            if (cam.m_Lens.OrthographicSize < 234.6f)
            {
                cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, 234.6f, Time.deltaTime*3f);
            }
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPos.transform.position, Time.deltaTime*3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trigger1"))
        {
            if (cam.m_Lens.OrthographicSize > 154f)
            {
                ZoomIn = true;
            }
            if (cam.m_Lens.OrthographicSize < 234.6f)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, target, Time.deltaTime*20f);
                //Invoke("camfollow", 5f);
            }
        }
    }

    void camfollow()
    {
        cam.Follow = transform;
        Debug.Log("camfollow true");
    }
}
