using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Slot : MonoBehaviour, IDropHandler
{
    public delegate void EquipedArmor(ItemModel item);
    public static EquipedArmor OnEquipedArmor;
    public delegate void EquipedWeapon(ItemModel item);
    public static EquipedWeapon OnEquipedWeapon;
    public delegate void MoveItem(ItemModel item);
    public static MoveItem OnMoveItem;
    
    public GameObject Item;
    public TypeSlot tSlot;
    public int ID;
    public string description;
    public Sprite icon;
    public bool empty;
    public bool cancelar;    
    private RenderTexture rt;

    private void OnEnable()
    {        
        GetComponent<Image>().raycastTarget = true;
    }
    void OnDisable()
    {
        GetComponent<Image>().raycastTarget = false;
    }
    
    private void Start()
    {
        rt = GetComponent<RenderTexture>();
    }
    public void UpdateSlot()
    {
        //SlotIconGameObject.GetComponent<Image>().sprite = icon;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();     
            //if (this.empty && !cancelar && ValidateTypeSlot(i.properties))
            //{
            //    this.empty = false;
            //    Destroy(eventData.pointerDrag.gameObject);
            //}
        }        
    }
    
    bool ValidateTypeSlot(ItemModel item)
    {
        //if (tSlot == TypeSlot.NoDropSlot)
        //    return false;

        //if (tSlot == TypeSlot.SlotArmor && item.typeItem == TypeItemInventory.Armor)
        //{            
        //    item.IndexSlot = this.ID;
        //    OnEquipedArmor?.Invoke(item);
        //    return true;
        //}      

        //if (tSlot == TypeSlot.SlotWeapon && item.typeItem == TypeItemInventory.Weapon)
        //{
        //    item.IndexSlot = this.ID;
        //    OnEquipedWeapon?.Invoke(item);
        //    return true;
        //}

        //if (tSlot == TypeSlot.SlotInventory)
        //{
        //    item.IndexSlot = this.ID;            
        //    OnMoveItem?.Invoke(item);
        //    return true;
        //}

        return false;
    }
}
