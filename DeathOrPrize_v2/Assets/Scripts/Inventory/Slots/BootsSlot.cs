using UnityEngine.EventSystems;
using UnityEngine;

public class BootsSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemBoots))
            return true;

        return false;
    }
}
