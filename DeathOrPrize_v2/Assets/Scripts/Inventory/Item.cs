using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemProperties properties;
    public Image image;
    public Text txtValue;
    public Text txtPeso;
    private AudioSource audioSource;
    private SpriteList weaponList;
    private SpriteList armorList;
    private DataFileController fileController = new DataFileController();
    
    private void Awake()
    {
        ReferenceComponents();
    }
    void Start()
    {
        
    }
    void ReferenceComponents()
    {
        audioSource = GetComponent<AudioSource>();
        weaponList = GameObject.FindGameObjectWithTag("Weapons").GetComponent<SpriteList>();
        armorList = GameObject.FindGameObjectWithTag("Armors").GetComponent<SpriteList>();
    }
    void SetTexts()
    {
        if (txtValue == null)
            return;

        //if(type == TypeItem.ARMOR)
        //{
        //    txtValue.text = defending.ToString();
        //}
        //if (type == TypeItem.WEAPON)
        //{
        //    txtValue.text = damageBase.ToString();
        //}
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
        if(weaponList != null)
            image.sprite = weaponList.GetSprite(properties.SubType);
    }
    void AssignArmor()
    {
        if(armorList != null)
            image.sprite = armorList.GetSprite(properties.SubType);
    }
    
}
