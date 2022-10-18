using UnityEngine.EventSystems;
using UnityEngine;

public class ArmorSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemArmor))
            return true;

        return false;
    }
}
