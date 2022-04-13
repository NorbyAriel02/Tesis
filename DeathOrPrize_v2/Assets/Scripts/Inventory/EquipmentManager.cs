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
    void Start()
    {
        GetSlots();
        items = InventoryHelper.GetListItemsFromFile(PathHelper.EquipmentDataFile);
    }
    void GetSlots()
    {
        Slots = ChildrenController.GetChildren(Equipment);
        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x].GetComponent<Slot>().ID = x; //le asigno un ID manual
            //Slots[x].GetComponent<Slot>().empty = true;
        }
    }    
    public void Save()
    {
        items = InventoryHelper.GetListItemsFromPanel(Slots);
        InventoryHelper.Save(items, PathHelper.EquipmentDataFile);        
    }
    public void ShowEquipment()
    {
        InventoryHelper.ShowItems(Slots, prefabItemTemplate, PathHelper.EquipmentDataFile);        
    }
}
