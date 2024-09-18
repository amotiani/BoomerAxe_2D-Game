using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class weaponWheelButtonController : MonoBehaviour
{
    public int id;
    public Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedItem;
    private bool selected = false;
    public Sprite icon;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            selectedItem.sprite = icon;
            itemText.text = itemName;
        }
    }

    public void Selected()
    {
        selected = true;
        weaponWheelController.weaponId = id;
    }
    public void DeSelected()
    {
        selected = false;
        weaponWheelController.weaponId = 0;
    }

    public void HoverEnter()
    {
        anim.SetBool("hovered", true);
        itemText.text = itemName;
    }
    public void HoverExit()
    {
        anim.SetBool("hovered", false);
        itemText.text = "";
    }
}
