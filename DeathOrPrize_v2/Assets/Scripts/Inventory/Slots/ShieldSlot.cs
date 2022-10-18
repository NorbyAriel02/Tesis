using UnityEngine.EventSystems;
using UnityEngine;

public class ShieldSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemShield))
            return true;

        return false;
    }
}
