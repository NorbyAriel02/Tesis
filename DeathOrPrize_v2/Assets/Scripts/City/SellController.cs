using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SellController : MonoBehaviour, IDropHandler
{
    private MarketController market;
    private Slot slot;
    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponent<Slot>();
        market = GetComponentInParent<MarketController>();
        if (market == null)
            Debug.Log("Market script null");
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && slot.empty)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();
            market.Sell(i.properties);
        }
    }
}
