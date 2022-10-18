using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHelper
{
    public static void ShowItemsMarket(GameObject[] _Slots, GameObject[] prefabItem)
    {
        List<ItemModel> items = DataHelper.GetListMarket();
        ShowItems(_Slots, prefabItem, items);
    }
    public static void ShowItemsEquip(GameObject[] _Slots, GameObject[] prefabItem)
    {
        List<ItemModel> items = DataHelper.GetListEquip();
        ShowItems(_Slots, prefabItem, items);
    }
    public static void ShowItemsInventory(GameObject[] _Slots, GameObject[] prefabItem)
    {
        try
        {            
            List<ItemModel> items = DataHelper.GetListInventory();
            ShowItems(_Slots, prefabItem, items);
        }
        catch (System.Exception ex)
        {
            Logger.WriteLog(ex.Message);
        }
    }
    public static void ShowItems(GameObject[] _Slots, GameObject[] prefabItem, string dataFile)
    {
        List<ItemModel> items = DataHelper.GetItems(dataFile);
        ShowItems(_Slots, prefabItem, items);
    }
    private static void ShowItems(GameObject[] _Slots, GameObject[] prefabItem, List<ItemModel> items)
    {
        
            EmptySlots(_Slots);

            if (items == null)
                return;

            foreach (ItemModel item in items)
            {
                int index = -1;
                if (item.IndexSlot != -1 && IsIndexSlotEmpty(item.IndexSlot, _Slots))
                    index = item.IndexSlot;
                else
                    index = GetIndexSlotEmpty(_Slots);

                GameObject gItem = null;
                if(item.IsStackable)
                {
                    gItem = GameObject.Instantiate(prefabItem[1], _Slots[index].transform);
                }
                else
                {
                    gItem = GameObject.Instantiate(prefabItem[0], _Slots[index].transform);
                }                
                _Slots[index].GetComponent<BaseSlot>().empty = false;
                _Slots[index].GetComponent<BaseSlot>().Item = gItem;
                item.IndexSlot = index;
                Item scriptItem = gItem.GetComponent<Item>();
                scriptItem.SetItem(item);
            }
        
    }    
    public static GameObject[] GetSlots(GameObject content)
    {
        GameObject[] slots =  ChildrenController.GetChildren(content);
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<BaseSlot>() != null)
            {
                slots[i].GetComponent<BaseSlot>().empty = true;
                slots[i].GetComponent<BaseSlot>().ID = i;
            }
        }        
        return slots;
    }
    public static void EmptySlots(GameObject[] _Slots)
    {
        foreach (GameObject slot in _Slots)
        {
            if (slot.GetComponent<BaseSlot>() == null)
                continue;

            slot.GetComponent<BaseSlot>().empty = true;
            ChildrenController.RemoveAllChildren(slot);
        }
    }
    private static bool IsIndexSlotEmpty(int index, GameObject[] _Slots)
    {
        
        if (index < _Slots.Length)
        {
            BaseSlot slot = _Slots[index].GetComponent<BaseSlot>();
            if(slot.empty)
                return true;
        }           

        return false;
    }
    public static int GetIndexSlotEmpty(GameObject[] _Slots)
    {        
        foreach (GameObject slot in _Slots)
        {
            BaseSlot sslot = slot.GetComponent<BaseSlot>();
            if (sslot.empty)
                return sslot.ID;
        }
        return -1;
    }
}
