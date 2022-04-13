using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public KeyCode keyInventory;
    public GameObject inventory;
    public GameObject prefabItemTemplate;
    public GameObject inventoryPanel;
    public GameObject[] Slots;
    public EquipmentManager equipment;
    public List<ItemProperties> items;

    void Start()
    {        
        GetSlots();
        items = InventoryHelper.GetListItemsFromFile(PathHelper.InventoryDataFile);
        inventory.SetActive(false);
    }
    void GetSlots()
    {
        Slots = ChildrenController.GetChildren(inventoryPanel); 
        for(int x = 0; x < Slots.Length; x++)
        {
            Slots[x].GetComponent<Slot>().ID = x;
            Slots[x].GetComponent<Slot>().empty = true;
        }
    }    
    void Update()
    {
        if (Input.GetKeyDown(keyInventory) && !inventory.activeSelf)
            OpenInventory();
        else if (Input.GetKeyDown(keyInventory) && inventory.activeSelf)
            CloseInventory();
    }
    public void CloseInventory()
    {
        List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(Slots);
        InventoryHelper.Save(items, PathHelper.InventoryDataFile);        
        equipment.Save();
        inventory.SetActive(false);
    }
    public void OpenInventory()
    {
        inventory.SetActive(true);
        InventoryHelper.ShowItems(Slots, prefabItemTemplate, PathHelper.InventoryDataFile);        
        equipment.ShowEquipment();
    }    
    public bool AddItem(ItemProperties newItem)
    {        
        return InventoryHelper.AddItem(newItem, Slots, PathHelper.InventoryDataFile); 
    }
}
