using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHelper 
{
    public static List<ItemModel> GetListItemsFromFile(string path)
    {
        DataFileController fileController = new DataFileController();
        List<ItemModel> items = fileController.GetEncryptedData<List<ItemModel>>(path);
        if (items == null)
            items = new List<ItemModel>();

        return items;
    }
    public static List<ItemModel> GetListItemsFromPanel(GameObject[] _Slots)
    {
        List<ItemModel> items = new List<ItemModel>();
        foreach (GameObject slot in _Slots)
        {
            if (slot.GetComponent<Slot>() == null)
                continue;

            if (!slot.GetComponent<Slot>().empty)
            {
                GameObject item = ChildrenController.GetChild(slot);
                //ItemModel data = item.GetComponent<Item>().properties;
                //items.Add(data);
            }
        }
        return items;
    }
    public static void ShowItems(GameObject[] _Slots, GameObject prefabItem, string path)
    {
        EmptySlots(_Slots);
        List<ItemModel> items = GetListItemsFromFile(path);
        
        if (items == null)
            return;

        foreach (ItemModel item in items)
        {
            int index = -1;
            if (item.IndexSlot != -1 && IsIndexSlotEmpty(item.IndexSlot, _Slots))
                index = item.IndexSlot;
            else
                index = GetIndexSlotEmpty(_Slots, item);

            GameObject gItem = GameObject.Instantiate(prefabItem, _Slots[index].transform);
            _Slots[index].GetComponent<Slot>().empty = false;
            _Slots[index].GetComponent<Slot>().Item = gItem;
            item.IndexSlot = index;
            Item scriptItem = gItem.GetComponent<Item>();
            //scriptItem.SetItem(item);
        }
    }    
    public static bool AddItem(ItemModel newItem, GameObject[] Slots, string path)
    {
        List<ItemModel> items = GetListItemsFromFile(path);
        if (items.Count >= Slots.Length)
            return false;

        newItem.IndexSlot = GetIndexSlotEmpty(Slots, newItem);
        items.Add(newItem);
        Save(items, path);
        
        return true;
    }
    public static void RemoveItemFromFile(ItemModel item, string path)
    {
        List<ItemModel> items = GetListItemsFromFile(path);
        items.Remove(item);
        Save(items, path);
    }
    public static void EmptySlots(GameObject[] _Slots)
    {
        foreach (GameObject slot in _Slots)
        {
            if (slot.GetComponent<Slot>() == null)
                continue;

            slot.GetComponent<Slot>().empty = true;
            ChildrenController.RemoveAllChildren(slot);
        }
    }    
    public static void Save(List<ItemModel> items, string path)
    {
        DataFileController fileController = new DataFileController();
        fileController.SaveEncrypted<List<ItemModel>>(items, path);
    }
    private static int GetIndexSlotEmpty(GameObject[] _Slots, ItemModel item)
    {
        int index = 0;
        foreach (GameObject slot in _Slots)
        {
            Slot sslot = slot.GetComponent<Slot>();
            if (sslot.empty)// && sslot.tSlot == item.tItem)
                return index;

            index++;
        }
        return -1;
    }
    private static bool IsIndexSlotEmpty(int index, GameObject[] _Slots)
    {
        if (index < _Slots.Length && _Slots[index].GetComponent<Slot>().empty)
            return true;

        return false;
    }
    public static void StartInventoryAndEquipmentFile(string pathInventory, string pathEquipment)
    {
        DataFileController fileController = new DataFileController();
        List<ItemModel> items = new List<ItemModel>();
        fileController.SaveEncrypted<List<ItemModel>>(items, pathInventory);
        fileController.SaveEncrypted<List<ItemModel>>(items, pathEquipment);
    }
}
