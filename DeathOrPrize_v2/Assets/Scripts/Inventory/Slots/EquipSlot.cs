using UnityEngine.EventSystems;
using UnityEngine;

public class EquipSlot : BaseSlot, IDropHandler
{    
    public override bool ActionSlot(ItemProperties item)
    {        
        if (item.typeSlot != this.tSlot)
            return false;

        if (item.owner == Owner.seller)
        {
            return Buy(item);
        }
        
        if (LevelPlayer() < item.level)
            return false;

        return true;
    }

    int LevelPlayer()
    {
        return DataHelper.GetStats().level;
    }
}
