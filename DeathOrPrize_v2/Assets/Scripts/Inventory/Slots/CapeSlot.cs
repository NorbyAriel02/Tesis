using UnityEngine.EventSystems;
using UnityEngine;

public class CapeSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemCape))
            return true;

        return false;
    }
}
