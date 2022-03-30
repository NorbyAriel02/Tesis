using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuyController : MonoBehaviour, IDropHandler
{
    public bool MarketOpen;
    private MarketController market;
    private Slot slot;

    void Start()
    {
        slot = GetComponent<Slot>();
        market = GameObject.FindGameObjectWithTag("Market").GetComponent<MarketController>();
        if (market == null)
            Debug.Log("Script buy dice Market script null");
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (market.IsClose)
            return;

        if (eventData.pointerDrag != null && slot.empty)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();
            if(!market.Buy(i.properties))
                slot.cancelar = true;
        }
    }
}
