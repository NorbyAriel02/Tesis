using UnityEngine.EventSystems;
using UnityEngine;

public class WeaponSlot : EquipSlot, IDropHandler
{
    public override bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemWeapon))
            return true;

        return false;
    }
}
