using UnityEngine.EventSystems;
using UnityEngine;

public class CitySlot : BaseSlot, IDropHandler
{
    
    public override bool ActionSlot(ItemModel item)
    {
        if (item.owner == Owner.seller)
        {
            return Buy(item);
        }
        return true;
    }
    
}
