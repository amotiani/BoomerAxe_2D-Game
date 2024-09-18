using UnityEngine;

public class axeMaaro : MonoBehaviour
{
    public GameObject Axe;
    public float launchForce;
    public float returnForce;
    public GameObject pivot;
    public GameObject player;
    public static bool canInstantiate = true;
    public static bool cancallback;
    public static bool cangoback;
    public static bool axefired;
    public static bool ShakeCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //THROW THE AXE
        if (Input.GetMouseButtonDown(0) && weaponWheelController.weaponWheelSelected == false && pivot.GetComponent<axeWeapon>().axeClick == true && canInstantiate)// && movt.canAxeThrow==true)
        {
            if (movt.canAxeThrow)
            {
                GameObject banayaHuaAxe = Instantiate(Axe, transform.position, Axe.transform.rotation);
                canInstantiate = false;
                banayaHuaAxe.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce);
                banayaHuaAxe.GetComponent<axeMovt>().animAxe.SetBool("isrotating", true);
                axefired = true;
            }
            else if (!movt.canAxeThrow)
            {
                GameObject banayaHuaAxe = Instantiate(Axe, transform.position, Axe.transform.rotation);
                canInstantiate = false;
                banayaHuaAxe.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce*1.3f);
                banayaHuaAxe.GetComponent<axeMovt>().animAxe.SetBool("isrotating", true);
                axefired = true;
            }
            
            
        }

        //CALL IT BACK
        else if(Input.GetMouseButtonDown(0) && weaponWheelController.weaponWheelSelected == false && axefired==true && !canInstantiate)
        {
            cancallback = true;
        }
        else if (Input.GetMouseButtonDown(1) && weaponWheelController.weaponWheelSelected == false && axefired == true && !canInstantiate)
        {
            cangoback = true;
        }
        if (cancallback)
        {
            CallBack();
        }
        if(cangoback)
        {
            GoBack();
        }
    }

    void CallBack()
    {
        GameObject.FindWithTag("axe").transform.position = Vector3.MoveTowards(GameObject.FindWithTag("axe").transform.position, transform.position, returnForce * Time.deltaTime*3.5f);
        GameObject.FindWithTag("axe").GetComponent<axeMovt>().animAxe.SetBool("isrotating", true);

        if (Vector3.Distance(GameObject.FindWithTag("axe").transform.position, transform.position) <= 0.1f)
        {
            DestroyImmediate(GameObject.FindWithTag("axe"));
            canInstantiate = true;
            cancallback = false;
        }
    }

    void GoBack()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, GameObject.FindWithTag("axe").transform.position, returnForce * Time.deltaTime * 3.5f);

        if (Vector3.Distance(GameObject.FindWithTag("axe").transform.position, player.transform.position) <= 0.1f)
        {
            DestroyImmediate(GameObject.FindWithTag("axe"));
            canInstantiate = true;
            cangoback = false;
        }

    }
}
