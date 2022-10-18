using UnityEngine.EventSystems;
using UnityEngine;

public class GlovesSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemGloves))
            return true;

        return false;
    }
}
