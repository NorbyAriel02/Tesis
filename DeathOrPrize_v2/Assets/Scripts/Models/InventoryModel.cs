using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryModel 
{
    public List<ItemModel> items;
    public int SlotsNumber { get; set; }
    public InventoryModel()
    {
        items = new List<ItemModel>();        
    }
}
