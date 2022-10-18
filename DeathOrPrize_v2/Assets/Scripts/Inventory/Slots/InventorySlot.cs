using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : BaseSlot
{
    public override bool ActionSlot(ItemModel item)
    {
        if (item.owner == Owner.seller)
        {
            return Buy(item);
        }
        return true;
    }
}
