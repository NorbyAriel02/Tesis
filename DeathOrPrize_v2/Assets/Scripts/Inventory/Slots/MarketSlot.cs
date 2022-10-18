using UnityEngine.EventSystems;
using UnityEngine;

public class MarketSlot : BaseSlot, IDropHandler
{
    public delegate void EventSell();
    public static EventSell OnSell;
    public override bool ActionSlot(ItemModel item)
    {  
        if(item.owner == Owner.player)
        {
            Sell(item);
            OnSell.Invoke();
        }        
        return true;
    }
}
