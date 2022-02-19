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
    public List<ItemProperties> items;
    public EquipmentManager equipment;
    DataFileController fileController = new DataFileController();    
    void Start()
    {
        items = new List<ItemProperties>();
        
        GetSlots();
        Load();
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyInventory))
        {
            inventory.SetActive(!inventory.activeSelf);
            if (!inventory.activeSelf)
            {
                Save();
                equipment.Save();
            }
                
            if (inventory.activeSelf)
            {
                UpdateData();
                equipment.UpdateData();
            }
                
        }   
    }
    void Save()
    {
        items = new List<ItemProperties>();
        foreach (GameObject slot in Slots)
        {
            if (!slot.GetComponent<Slot>().empty)
            {
                GameObject item = ChildrenController.GetChild(slot);
                ItemProperties data = item.GetComponent<Item>().properties;
                items.Add(data);
            }
        }
        fileController.Save<List<ItemProperties>>(items, PathHelper.InventoryDataFile);
    }
    void Load()
    {
        LoadInventory();
    }
    int GetIndexSlotEmpty()
    {
        int index = 0;
        foreach (GameObject slot in Slots)
        {
            if (slot.GetComponent<Slot>().empty)
                return index;

            index++;
        }
        //No debe ir aca
        FullInventaryFeedback();

        return -1;
    }
    
    void LoadInventory()
    {
        items = fileController.GetData<List<ItemProperties>>(PathHelper.InventoryDataFile);
        if (items == null)
            return;
        
        AddItemsToSlots();        
    }
    void AddItemsToSlots()
    {
        int index = -1;
        for(int x = 0; x < items.Count; x++)
        {
            if (items[x].IndexSlot != -1)
                index = items[x].IndexSlot;

            if (index == -1)
                return;

            if (Slots[index].GetComponent<Slot>().empty)
            {
                GameObject gItem = Instantiate(prefabItemTemplate, Slots[index].transform);
                Slots[index].GetComponent<Slot>().empty = false;
                items[x].IndexSlot = index;
                Item scriptItem = gItem.GetComponent<Item>();
                scriptItem.SetItem(items[x]);
            }            
        }
    }
    public void AddItem(ItemProperties newItem)
    {
        newItem.IndexSlot = GetIndexSlotEmpty();
        items.Add(newItem);
    }
    void UpdateData()
    {
        AddItemsToSlots();
    }
    public void FullInventaryFeedback()
    {
        Debug.Log("Inventario llego");
    }

    public void DeleteItem(ItemProperties item)
    {
        GameObject gItem = ChildrenController.GetChild(Slots[item.IndexSlot]);
        Destroy(gItem);
        items.Remove(item);
    }
}
