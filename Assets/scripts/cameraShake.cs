using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    public float shakeIntensity;
    public float shakeTime;
    public float shakeTimer;
    private CinemachineBasicMultiChannelPerlin _cmbmcp;
    public GameObject axethrowfrom;
    public GameObject player;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        stopShake();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && pistolWeapon.pistolClick || (axeMaaro.cancallback))
        {
            Debug.Log("Shakeee");
            shake();
        }
        if(shakeTimer> 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                stopShake();
            }
        }
    }

    private void shake()
    {
        CinemachineBasicMultiChannelPerlin _cmbmcp = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cmbmcp.m_AmplitudeGain = shakeIntensity;
        shakeTimer = shakeTime;
    }

    private void stopShake()
    {
        CinemachineBasicMultiChannelPerlin _cmbmcp = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cmbmcp.m_AmplitudeGain = 0;
        shakeTimer = 0;
    }
}
