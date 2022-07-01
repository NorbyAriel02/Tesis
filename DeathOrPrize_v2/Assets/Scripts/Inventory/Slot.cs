using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum TypeSlot { SlotInventory, SlotWeapon, SlotArmor, NoDropSlot }
public class Slot : MonoBehaviour, IDropHandler
{
    public delegate void EquipedArmor(ItemProperties item);
    public static EquipedArmor OnEquipedArmor;
    public delegate void EquipedWeapon(ItemProperties item);
    public static EquipedWeapon OnEquipedWeapon;
    public delegate void MoveItem(ItemProperties item);
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
            if (this.empty && !cancelar && ValidateTypeSlot(i.properties))
            {
                i.properties.IndexSlot = this.ID;
                eventData.pointerDrag.gameObject.transform.SetParent(transform);
                eventData.pointerDrag.gameObject.transform.position = transform.position;
                this.empty = false;
            }
        }
        
    }
    
    bool ValidateTypeSlot(ItemProperties item)
    {
        if (tSlot == TypeSlot.NoDropSlot)
            return false;

        if (tSlot == TypeSlot.SlotArmor && item.tItem == TypeItemInventory.Armor)
        {
            OnEquipedArmor?.Invoke(item);
            return true;
        }            

        if (tSlot == TypeSlot.SlotWeapon && item.tItem == TypeItemInventory.Weapon)
        {
            OnEquipedWeapon?.Invoke(item);
            return true;
        }

        if (tSlot == TypeSlot.SlotInventory)
        {
            OnMoveItem?.Invoke(item);
            return true;
        }

        return false;
    }
}
