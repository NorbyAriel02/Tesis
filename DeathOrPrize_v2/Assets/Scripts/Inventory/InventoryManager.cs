using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{    
    [Header("Panel que contiene los Slots del Inventario")]
    [Tooltip("Agrega el panel contenedor del inventario")]
    public GameObject viewInventory;    
    [Header("Prefeb del modelo item para inventario")]
    public GameObject prefabItemTemplate;
    private GameObject[] SlotsInventory;
    
    
    private void OnEnable()
    {
        DataHelper.OnItemPickup += ItemPickup;
        InventoryUI.OnOpen += UpdateView;
        DragAndDrop.OnMoveItem += UpdateView;
        CityController.OnEnterCity += UpdateView;
    }
    private void OnDisable()
    {
        DataHelper.OnItemPickup -= ItemPickup;
        InventoryUI.OnOpen -= UpdateView;
        DragAndDrop.OnMoveItem -= UpdateView;
        CityController.OnEnterCity -= UpdateView;
    }    
     
    public void UpdateView()
    {
        if (SlotsInventory == null)
        {
            SlotsInventory = ViewHelper.GetSlots(viewInventory);
            DataHelper.UpdateSlotsNumberInventory(SlotsInventory.Length);
        }            
                        
        ViewHelper.ShowItemsInventory(SlotsInventory, prefabItemTemplate);        
    }
    private void ItemPickup(ItemProperties item)
    {
        if (item.IndexSlot == -1 && SlotsInventory != null)
        {
            item.IndexSlot = ViewHelper.GetIndexSlotEmpty(SlotsInventory);
            DataHelper.UpdateItemInventory(item);
        }

        UpdateView();
    }
    //void InventoryToInventory(ItemProperties item)
    //{
    //    //cambiar idex item        
    //    DataHelper.UpdateItemInventory(item);
    //    UpdateView();
    //}
    //void OnMoveItem(ItemProperties item)
    //{
    //    if (DataHelper.ItemExistsInInventory(item))
    //        InventoryToInventory(item);
    //    else
    //        EquipToInventory(item);
    //}
    //void InventoryToEquip(ItemProperties item)
    //{                
    //    UpdateView();
    //}
    //void EquipToInventory(ItemProperties item)
    //{
    //    DataHelper.Unequip(item);
    //    DataHelper.AddItemInventory(item);
    //    UpdateView();
    //}
    //void InventoryToTrash(ItemProperties item)
    //{
    //    //Actualizar vista
    //}
    //void EquipToTrash(ItemProperties item)
    //{
    //    //Actualizar vista
    //}
}
