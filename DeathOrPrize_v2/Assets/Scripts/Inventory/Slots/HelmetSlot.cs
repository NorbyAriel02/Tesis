using UnityEngine.EventSystems;
using UnityEngine;

public class HelmetSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemHelmet))
            return true;

        return false;
    }
}
