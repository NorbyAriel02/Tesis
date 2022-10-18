using UnityEngine.EventSystems;
using UnityEngine;

public class RingSlot : EquipSlot, IDropHandler
{    
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemRing))
            return true;

        return false;
    }
}
