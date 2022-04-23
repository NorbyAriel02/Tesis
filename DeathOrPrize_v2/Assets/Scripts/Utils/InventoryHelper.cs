using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelper 
{
    public static List<ItemProperties> GetListItemsFromFile(string path)
    {
        DataFileController fileController = new DataFileController();
        List<ItemProperties> items = fileController.GetEncryptedData<List<ItemProperties>>(path);

        return items;
    }
    public static List<ItemProperties> GetListItemsFromPanel(GameObject[] _Slots)
    {
        List<ItemProperties> items = new List<ItemProperties>();
        foreach (GameObject slot in _Slots)
        {
            if (!slot.GetComponent<Slot>().empty)
            {
                GameObject item = ChildrenController.GetChild(slot);
                ItemProperties data = item.GetComponent<Item>().properties;
                items.Add(data);
            }
        }
        return items;
    }
    public static void ShowItems(GameObject[] _Slots, GameObject prefabItem, string path)
    {
        EmptySlots(_Slots);
        List<ItemProperties> items = GetListItemsFromFile(path);
        
        if (items == null)
            return;

        foreach (ItemProperties item in items)
        {
            int index = -1;
            if (item.IndexSlot != -1 && IsIndexSlotEmpty(item.IndexSlot, _Slots))
                index = item.IndexSlot;
            else
                index = GetIndexSlotEmpty(_Slots);

            GameObject gItem = GameObject.Instantiate(prefabItem, _Slots[index].transform);
            _Slots[index].GetComponent<Slot>().empty = false;
            _Slots[index].GetComponent<Slot>().Item = gItem;
            item.IndexSlot = index;
            Item scriptItem = gItem.GetComponent<Item>();
            scriptItem.SetItem(item);
        }
    }    
    public static bool AddItem(ItemProperties newItem, GameObject[] Slots, string path)
    {
        List<ItemProperties> items = GetListItemsFromFile(path);
        if (items.Count >= Slots.Length)
            return false;

        newItem.IndexSlot = GetIndexSlotEmpty(Slots);
        items.Add(newItem);
        Save(items, path);
        
        return true;
    }
    public static void RemoveItemFromFile(ItemProperties item, string path)
    {
        List<ItemProperties> items = GetListItemsFromFile(path);
        items.Remove(item);
        Save(items, path);
    }
    public static void EmptySlots(GameObject[] _Slots)
    {
        foreach (GameObject slot in _Slots)
        {
            slot.GetComponent<Slot>().empty = true;
            ChildrenController.RemoveAllChildren(slot);
        }
    }    
    public static void Save(List<ItemProperties> items, string path)
    {
        DataFileController fileController = new DataFileController();
        fileController.SaveEncrypted<List<ItemProperties>>(items, path);
    }
    private static int GetIndexSlotEmpty(GameObject[] _Slots)
    {
        int index = 0;
        foreach (GameObject slot in _Slots)
        {
            if (slot.GetComponent<Slot>().empty)
                return index;

            index++;
        }
        return -1;
    }
    private static bool IsIndexSlotEmpty(int index, GameObject[] _Slots)
    {
        if (_Slots[index].GetComponent<Slot>().empty)
            return true;

        return false;
    }
    public static void StartInventoryAndEquipmentFile(string pathInventory, string pathEquipment)
    {
        DataFileController fileController = new DataFileController();
        List<ItemProperties> items = new List<ItemProperties>();
        fileController.SaveEncrypted<List<ItemProperties>>(items, pathInventory);
        fileController.SaveEncrypted<List<ItemProperties>>(items, pathEquipment);
    }
}
