using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public int IndexSlot = -1;
    public int CurrentSlot = -1;
    public ItemProperties properties;
    public Image image;
    public Text txtValue;
    public Text txtPeso;
    private SpriteList weaponList;
    private SpriteList armorList;    
    
    private void Awake()
    {
        ReferenceComponents();
    }
    void Start()
    {
        
    }
    void ReferenceComponents()
    {        
        weaponList = GameObject.FindGameObjectWithTag("Weapons").GetComponent<SpriteList>();
        armorList = GameObject.FindGameObjectWithTag("Armors").GetComponent<SpriteList>();
    }
    void SetTexts()
    {
        if (txtValue == null)
            return;

        txtPeso.text = properties.weight.ToString();
    }
    
    public void SetItem(ItemProperties value)
    {
        properties = value;
        if (properties.tItem == TypeItemInventory.Weapon)
            AssignWeapon();
        else if (properties.tItem == TypeItemInventory.Armor)
            AssignArmor();
    }    
    
    void Update()
    {
        
    }

    
    void AssignWeapon()
    {
        if(weaponList == null)
            weaponList = GameObject.FindGameObjectWithTag("Weapons").GetComponent<SpriteList>();

        if (weaponList != null)
            image.sprite = weaponList.GetSprite(properties.SubType);
    }
    void AssignArmor()
    {
        if(armorList == null)
            armorList = GameObject.FindGameObjectWithTag("Armors").GetComponent<SpriteList>();

        if (armorList != null)
            image.sprite = armorList.GetSprite(properties.SubType);
    }
    
}
