using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum TypeSlot { SlotInventory, SlotWeapon, SlotArmor, NoDropSlot }
public class Slot : MonoBehaviour, IDropHandler
{
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
    void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();
            if (ValidateTypeSlot(i.properties.tItem) && this.empty && !cancelar)
            {
                i.properties.IndexSlot = this.ID;
                eventData.pointerDrag.gameObject.transform.SetParent(transform);
                eventData.pointerDrag.gameObject.transform.position = transform.position;
                this.empty = false;
            }
        }
    }

    bool ValidateTypeSlot(TypeItemInventory tItem)
    {
        if (tSlot == TypeSlot.NoDropSlot)
            return false;

        if (tSlot == TypeSlot.SlotArmor && tItem == TypeItemInventory.Armor)
        {
            AkSoundEngine.PostEvent("Item_Equip", this.gameObject);
            return true;
        }
            

        if (tSlot == TypeSlot.SlotWeapon && tItem == TypeItemInventory.Weapon)
        {
            AkSoundEngine.PostEvent("Item_Equip", this.gameObject);
            return true;
        }

        if (tSlot == TypeSlot.SlotInventory)
            return true;

        return false;
    }
}
