using UnityEngine;
using UnityEngine.UI;

public class weaponWheelController : MonoBehaviour
{
    public Animator anim;
    public static bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponId;
    public GameObject displayText;
    public GameObject player;
    public GameObject pivot;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            weaponWheelSelected = true;
        }
        else if(Input.GetKeyUp(KeyCode.Tab) || !Input.GetKey(KeyCode.Tab))
        {
            weaponWheelSelected = false;
        }
        if(weaponWheelSelected == true)
        {
            anim.SetBool("openweaponwheel", true);
            player.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
        }
        else
        {
            anim.SetBool("openweaponwheel", false);
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        switch (weaponId)
        {
            case 0: //nothing selected
                selectedItem.sprite = noImage;
                break;
            case 1: //pistol
                pistolWeapon.pistolClick = true;
                pivot.GetComponent<axeWeapon>().axeClick = false;

                break;
            case 2: //axe
                pivot.GetComponent<axeWeapon>().axeClick = true;
                pistolWeapon.pistolClick = false;
                break;

            case 3: //katana
                Debug.Log("thor's thunder");
                break;
            case 4: //nothing selected
                Debug.Log("pistol");
                break;
            case 5: //nothing selected
                Debug.Log("pistol");
                break;
            case 6: //nothing selected
                Debug.Log("pistol");
                break;
        }

        if (weaponWheelSelected)
        {
            displayText.SetActive(true);
        }
        else if(weaponWheelSelected==false)
        {
            displayText.SetActive(false);
        }
    }
}
