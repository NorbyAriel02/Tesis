using UnityEngine.EventSystems;
using UnityEngine;

public class EquipSlot : BaseSlot, IDropHandler
{
    public delegate void Equiped();
    public static Equiped OnEquiped;
    public override bool ActionSlot(ItemModel item)
    {
        if (NotIsTypeItemRigth(item))
            return false;
        
        if (LevelPlayer() < item.level)
            return false;

        if (item.owner == Owner.seller)
        {
            if(Buy(item))
            {
                OnEquiped?.Invoke();
                return true;
            }
            else
            {
                return false;
            }            
        }

        OnEquiped?.Invoke();
        return true;
    }
    public virtual bool NotIsTypeItemRigth(ItemModel item)
    {
        if (item.GetType() != typeof(ItemModel))
            return true;
        
        return false;
    }
    int LevelPlayer()
    {
        return DataHelper.GetStats().level;
    }
}
