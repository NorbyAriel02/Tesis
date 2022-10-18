using UnityEngine.EventSystems;
using UnityEngine;

public class AmuletSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemAmulet))
            return true;

        return false;
    }
}
