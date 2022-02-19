using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{    
    public GameObject PanelMaster;    
    public GameObject prefabItemTemplate;
    public GameObject Equipment;
    public GameObject[] Slots;
    public List<ItemProperties> items;
    DataFileController fileController = new DataFileController();
    // Start is called before the first frame update
    void Start()
    {
        GetSlots();
        Load();
    }
    void GetSlots()
    {
        Slots = ChildrenController.GetChildren(Equipment);
        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x].GetComponent<Slot>().ID = x; //le asigno un ID manual
            Slots[x].GetComponent<Slot>().empty = true;
        }
    }
    void Load()
    {
        items = fileController.GetData<List<ItemProperties>>(PathHelper.EquipmentDataFile);
        if (items == null)
            return;

        AddItemsToSlots();
    }
    void AddItemsToSlots()
    {
        int index = -1;
        for (int x = 0; x < items.Count; x++)
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
    public void Save()
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
        fileController.Save<List<ItemProperties>>(items, PathHelper.EquipmentDataFile);
    }
    public void UpdateData()
    {
        AddItemsToSlots();
    }
}
